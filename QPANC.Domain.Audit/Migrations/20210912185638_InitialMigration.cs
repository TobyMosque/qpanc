using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace QPANC.Domain.Audit.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Revision = table.Column<Guid>(type: "uuid", nullable: false),
                    RevNumber = table.Column<int>(type: "integer", nullable: false),
                    Operation = table.Column<byte>(type: "smallint", nullable: false),
                    AuditedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UserSessionId = table.Column<Guid>(type: "uuid", nullable: true),
                    Id = table.Column<int>(type: "integer", nullable: false),
                    RoleId = table.Column<Guid>(type: "uuid", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Revision);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Revision = table.Column<Guid>(type: "uuid", nullable: false),
                    RevNumber = table.Column<int>(type: "integer", nullable: false),
                    Operation = table.Column<byte>(type: "smallint", nullable: false),
                    AuditedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UserSessionId = table.Column<Guid>(type: "uuid", nullable: true),
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Revision);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Revision = table.Column<Guid>(type: "uuid", nullable: false),
                    RevNumber = table.Column<int>(type: "integer", nullable: false),
                    Operation = table.Column<byte>(type: "smallint", nullable: false),
                    AuditedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UserSessionId = table.Column<Guid>(type: "uuid", nullable: true),
                    Id = table.Column<int>(type: "integer", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Revision);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    Revision = table.Column<Guid>(type: "uuid", nullable: false),
                    RevNumber = table.Column<int>(type: "integer", nullable: false),
                    Operation = table.Column<byte>(type: "smallint", nullable: false),
                    AuditedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UserSessionId = table.Column<Guid>(type: "uuid", nullable: true),
                    LoginProvider = table.Column<string>(type: "text", nullable: true),
                    ProviderKey = table.Column<string>(type: "text", nullable: true),
                    ProviderDisplayName = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => x.Revision);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    Revision = table.Column<Guid>(type: "uuid", nullable: false),
                    RevNumber = table.Column<int>(type: "integer", nullable: false),
                    Operation = table.Column<byte>(type: "smallint", nullable: false),
                    AuditedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UserSessionId = table.Column<Guid>(type: "uuid", nullable: true),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    RoleId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => x.Revision);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Revision = table.Column<Guid>(type: "uuid", nullable: false),
                    RevNumber = table.Column<int>(type: "integer", nullable: false),
                    Operation = table.Column<byte>(type: "smallint", nullable: false),
                    AuditedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UserSessionId = table.Column<Guid>(type: "uuid", nullable: true),
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: true),
                    SecurityStamp = table.Column<string>(type: "text", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "integer", nullable: false),
                    FirstName = table.Column<string>(type: "text", nullable: true),
                    LastName = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Revision);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    Revision = table.Column<Guid>(type: "uuid", nullable: false),
                    RevNumber = table.Column<int>(type: "integer", nullable: false),
                    Operation = table.Column<byte>(type: "smallint", nullable: false),
                    AuditedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UserSessionId = table.Column<Guid>(type: "uuid", nullable: true),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    LoginProvider = table.Column<string>(type: "text", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => x.Revision);
                });

            migrationBuilder.CreateTable(
                name: "Sessions",
                columns: table => new
                {
                    Revision = table.Column<Guid>(type: "uuid", nullable: false),
                    RevNumber = table.Column<int>(type: "integer", nullable: false),
                    Operation = table.Column<byte>(type: "smallint", nullable: false),
                    AuditedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UserSessionId = table.Column<Guid>(type: "uuid", nullable: true),
                    SessionId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    ExpireAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sessions", x => x.Revision);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_Id_RevNumber",
                table: "AspNetRoleClaims",
                columns: new[] { "Id", "RevNumber" });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoles_Id_RevNumber",
                table: "AspNetRoles",
                columns: new[] { "Id", "RevNumber" });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_Id_RevNumber",
                table: "AspNetUserClaims",
                columns: new[] { "Id", "RevNumber" });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_LoginProvider_ProviderKey_RevNumber",
                table: "AspNetUserLogins",
                columns: new[] { "LoginProvider", "ProviderKey", "RevNumber" });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_UserId_RoleId_RevNumber",
                table: "AspNetUserRoles",
                columns: new[] { "UserId", "RoleId", "RevNumber" });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_Id_RevNumber",
                table: "AspNetUsers",
                columns: new[] { "Id", "RevNumber" });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserTokens_UserId_LoginProvider_RevNumber",
                table: "AspNetUserTokens",
                columns: new[] { "UserId", "LoginProvider", "RevNumber" });

            migrationBuilder.CreateIndex(
                name: "IX_Sessions_SessionId_RevNumber",
                table: "Sessions",
                columns: new[] { "SessionId", "RevNumber" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Sessions");
        }
    }
}
