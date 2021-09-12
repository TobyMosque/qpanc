using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using QPANC.Domain;
using QPANC.Domain.Audit;
using QPANC.Services.Abstract;
using QPANC.Services.Abstract.Entities.Identity;
using System.Threading.Tasks;

namespace QPANC.Services
{
    public class Seeder : ISeeder
    {
        private RT.Comb.ICombProvider _comb;
        private QpancContext _context;
        private QpancAuditContext _auditContext;
        private UserManager<User> _userManager;
        private RoleManager<Role> _roleManager;
        public Seeder(RT.Comb.ICombProvider comb, QpancContext context, QpancAuditContext auditContext, UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            this._comb = comb;
            this._context = context;
            this._auditContext = auditContext;
            this._userManager = userManager;
            this._roleManager = roleManager;
        }

        public async Task Execute()
        {
            await this._context.Database.MigrateAsync();
            await this._auditContext.Database.MigrateAsync();
            await this.CreateRolesAndDevUser();
        }

        private async Task CreateRolesAndDevUser()
        {
            var roleNames = new string[] { "User", "Manager", "Admin", "Developer" };
            foreach (var roleName in roleNames)
            {
                var exists = await this._context.Roles.AnyAsync(role => role.Name == roleName);
                var roleExists = await this._roleManager.RoleExistsAsync(roleName);
                if (!roleExists)
                {
                    var role = new Role
                    {
                        Id = this._comb.Create(),
                        Name = roleName
                    };
                    await this._roleManager.CreateAsync(role);
                }
            }

            var developer = "developer@qpanc.app";
            var user = await this._userManager.FindByNameAsync(developer);
            if (user == default)
            {
                user = new User
                {
                    Id = this._comb.Create(),
                    UserName = developer,
                    Email = developer,
                    EmailConfirmed = true
                };
                var result = await this._userManager.CreateAsync(user, "KeepItSuperSecret$512");
            }

            roleNames = new string[] { "User", "Developer" };
            foreach (var roleName in roleNames)
            {
                var inInRole = await this._userManager.IsInRoleAsync(user, roleName);
                if (!inInRole)
                {
                    await this._userManager.AddToRoleAsync(user, roleName);
                }
            }
        }
    }
}
