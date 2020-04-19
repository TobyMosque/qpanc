using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using QPANC.Domain;
using QPANC.Domain.Identity;
using QPANC.Services.Abstract;
using System.Threading.Tasks;

namespace QPANC.Services
{
    public class Seeder : ISeeder
    {
        private ISGuid _sGuid;
        private QpancContext _context;
        private UserManager<User> _userManager;
        private RoleManager<Role> _roleManager;
        public Seeder(ISGuid sGuid, QpancContext context, UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            this._sGuid = sGuid;
            this._context = context;
            this._userManager = userManager;
            this._roleManager = roleManager;
        }

        public async Task Execute()
        {
            await this._context.Database.MigrateAsync();
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
                        Id = this._sGuid.NewGuid(),
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
                    Id = this._sGuid.NewGuid(),
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
