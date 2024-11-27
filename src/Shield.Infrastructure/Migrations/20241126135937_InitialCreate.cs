using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace XkliburSolutions.Shield.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    FirstName = table.Column<string>(type: "TEXT", nullable: true),
                    LastName = table.Column<string>(type: "TEXT", nullable: true),
                    DateOfBirth = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ProfilePictureUrl = table.Column<string>(type: "TEXT", nullable: true),
                    AlternateEmail = table.Column<string>(type: "TEXT", nullable: true),
                    Status = table.Column<int>(type: "INTEGER", nullable: false),
                    UserName = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "INTEGER", nullable: false),
                    PasswordHash = table.Column<string>(type: "TEXT", nullable: true),
                    SecurityStamp = table.Column<string>(type: "TEXT", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "TEXT", nullable: true),
                    PhoneNumber = table.Column<string>(type: "TEXT", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "INTEGER", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "INTEGER", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "TEXT", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "INTEGER", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RoleId = table.Column<Guid>(type: "TEXT", nullable: false),
                    ClaimType = table.Column<string>(type: "TEXT", nullable: true),
                    ClaimValue = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoleClaims_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Address",
                columns: table => new
                {
                    ApplicationUserId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Id = table.Column<int>(type: "INTEGER", nullable: false),
                    Type = table.Column<int>(type: "INTEGER", nullable: false),
                    AddressLine1 = table.Column<string>(type: "TEXT", nullable: true),
                    AddressLine2 = table.Column<string>(type: "TEXT", nullable: true),
                    AddressLine3 = table.Column<string>(type: "TEXT", nullable: true),
                    City = table.Column<string>(type: "TEXT", nullable: true),
                    Region = table.Column<string>(type: "TEXT", nullable: true),
                    Code = table.Column<string>(type: "TEXT", nullable: true),
                    Country = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Address", x => new { x.ApplicationUserId, x.Id });
                    table.ForeignKey(
                        name: "FK_Address_Users_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<Guid>(type: "TEXT", nullable: false),
                    ClaimType = table.Column<string>(type: "TEXT", nullable: true),
                    ClaimValue = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserClaims_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "TEXT", nullable: false),
                    ProviderKey = table.Column<string>(type: "TEXT", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "TEXT", nullable: true),
                    UserId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_UserLogins_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "TEXT", nullable: false),
                    RoleId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_UserRoles_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRoles_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserTokens",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "TEXT", nullable: false),
                    LoginProvider = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Value = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_UserTokens_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("1a16cfa9-2e08-49e4-b802-04cbf967e509"), null, "Administrator", "ADMINISTRATOR" },
                    { new Guid("ab6b5dbb-c72e-4c5c-ae29-ff03873d1eb8"), null, "User", "USER" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "AlternateEmail", "ConcurrencyStamp", "DateOfBirth", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "ProfilePictureUrl", "SecurityStamp", "Status", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("e0643abd-e50a-4683-b8a0-daa6cb9ea098"), 0, null, "02b42c44-ca84-42df-bb78-1a0d5b395d26", null, "opencode@xklibursolutions.io", true, null, null, false, null, "OPENCODE@XKLIBURSOLUTIONS.IO", "ADMIN", "AQAAAAIAAYagAAAAEK9rpIPyZfP9B9pcvQByMA5cycI0rvnNfvBEDjCKxM7UO0qzfYxVEKeRxfIdZQAwHQ==", null, false, null, "", 0, false, "admin" });

            migrationBuilder.InsertData(
                table: "RoleClaims",
                columns: new[] { "Id", "ClaimType", "ClaimValue", "RoleId" },
                values: new object[,]
                {
                    { 1, "Permission", "UserManagement.Create", new Guid("1a16cfa9-2e08-49e4-b802-04cbf967e509") },
                    { 2, "Permission", "UserManagement.Read", new Guid("1a16cfa9-2e08-49e4-b802-04cbf967e509") },
                    { 3, "Permission", "UserManagement.Update", new Guid("1a16cfa9-2e08-49e4-b802-04cbf967e509") },
                    { 4, "Permission", "UserManagement.Delete", new Guid("1a16cfa9-2e08-49e4-b802-04cbf967e509") },
                    { 5, "Permission", "UserManagement.ManageRoles", new Guid("1a16cfa9-2e08-49e4-b802-04cbf967e509") },
                    { 6, "Permission", "UserManagement.ManageClaims", new Guid("1a16cfa9-2e08-49e4-b802-04cbf967e509") },
                    { 7, "Permission", "UserManagement.Lock", new Guid("1a16cfa9-2e08-49e4-b802-04cbf967e509") },
                    { 8, "Permission", "UserManagement.Unlock", new Guid("1a16cfa9-2e08-49e4-b802-04cbf967e509") },
                    { 9, "Permission", "UserManagement.ResetPassword", new Guid("1a16cfa9-2e08-49e4-b802-04cbf967e509") },
                    { 10, "Permission", "RoleManagement.Create", new Guid("1a16cfa9-2e08-49e4-b802-04cbf967e509") },
                    { 11, "Permission", "RoleManagement.Read", new Guid("1a16cfa9-2e08-49e4-b802-04cbf967e509") },
                    { 12, "Permission", "RoleManagement.Update", new Guid("1a16cfa9-2e08-49e4-b802-04cbf967e509") },
                    { 13, "Permission", "RoleManagement.Delete", new Guid("1a16cfa9-2e08-49e4-b802-04cbf967e509") },
                    { 14, "Permission", "RoleManagement.ManageClaims", new Guid("1a16cfa9-2e08-49e4-b802-04cbf967e509") },
                    { 15, "Permission", "Security.EnableTwoFactorAuthentication", new Guid("1a16cfa9-2e08-49e4-b802-04cbf967e509") },
                    { 16, "Permission", "Security.DisableTwoFactorAuthentication", new Guid("1a16cfa9-2e08-49e4-b802-04cbf967e509") },
                    { 17, "Permission", "Security.ViewLoginHistory", new Guid("1a16cfa9-2e08-49e4-b802-04cbf967e509") },
                    { 19, "Permission", "AccessControl.Grant", new Guid("1a16cfa9-2e08-49e4-b802-04cbf967e509") },
                    { 20, "Permission", "AccessControl.Revoke", new Guid("1a16cfa9-2e08-49e4-b802-04cbf967e509") },
                    { 21, "Permission", "AccessControl.ViewLogs", new Guid("1a16cfa9-2e08-49e4-b802-04cbf967e509") },
                    { 22, "Permission", "ApplicationSettings.View", new Guid("1a16cfa9-2e08-49e4-b802-04cbf967e509") },
                    { 23, "Permission", "ApplicationSettings.Update", new Guid("1a16cfa9-2e08-49e4-b802-04cbf967e509") },
                    { 24, "Permission", "ApplicationSettings.ManageAPIKeys", new Guid("1a16cfa9-2e08-49e4-b802-04cbf967e509") },
                    { 31, "Permission", "Analytics.View", new Guid("1a16cfa9-2e08-49e4-b802-04cbf967e509") },
                    { 32, "Permission", "Analytics.Generate", new Guid("1a16cfa9-2e08-49e4-b802-04cbf967e509") },
                    { 33, "Permission", "Analytics.Export", new Guid("1a16cfa9-2e08-49e4-b802-04cbf967e509") },
                    { 37, "Permission", "UserManagement.Read", new Guid("ab6b5dbb-c72e-4c5c-ae29-ff03873d1eb8") },
                    { 38, "Permission", "UserManagement.Update", new Guid("ab6b5dbb-c72e-4c5c-ae29-ff03873d1eb8") }
                });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { new Guid("1a16cfa9-2e08-49e4-b802-04cbf967e509"), new Guid("e0643abd-e50a-4683-b8a0-daa6cb9ea098") });

            migrationBuilder.CreateIndex(
                name: "IX_RoleClaims_RoleId",
                table: "RoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "Roles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserClaims_UserId",
                table: "UserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserLogins_UserId",
                table: "UserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_RoleId",
                table: "UserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "Users",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "Users",
                column: "NormalizedUserName",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Address");

            migrationBuilder.DropTable(
                name: "RoleClaims");

            migrationBuilder.DropTable(
                name: "UserClaims");

            migrationBuilder.DropTable(
                name: "UserLogins");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "UserTokens");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
