using System.Reflection.Emit;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using XkliburSolutions.Shield.CrossCutting.Security;
using XkliburSolutions.Shield.Domain.Entities;

namespace XkliburSolutions.Shield.Infrastructure.Repositories;

/// <summary>
/// Represents the database context used by the solution for managing user data and identity information.
/// This class provides the necessary methods and properties to interact with the underlying database
/// and perform operations such as CRUD (Create, Read, Update, Delete) on user accounts, roles, and claims.
/// </summary>
/// <remarks>
/// Initializes a new instance of the ApplicationDbContext class using the specified options.
/// The options include configurations such as the database provider, connection string, and other settings
/// that are necessary for the context to interact with the database.
/// </remarks>
public class ApplicationDbContext(DbContextOptions options, IConfiguration configuration)
    : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>(options)
{
    /// <summary>
    /// Configures the schema needed for the identity system by overriding the OnModelCreating method.
    /// This method is called when the model for a derived context has been initialized, but
    /// before the model has been locked down and used to initialize the context.
    /// </summary>
    /// <param name="builder">Provides a simple API for configuring a model that defines the shape of your entities, 
    /// the relationships between them, and how they map to the database.</param>
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<ApplicationRole>().ToTable("Roles");
        builder.Entity<ApplicationUser>().ToTable("Users");
        builder.Entity<IdentityUserRole<Guid>>().ToTable("UserRoles");
        builder.Entity<IdentityUserClaim<Guid>>().ToTable("UserClaims");
        builder.Entity<IdentityRoleClaim<Guid>>().ToTable("RoleClaims");
        builder.Entity<IdentityUserLogin<Guid>>().ToTable("UserLogins");
        builder.Entity<IdentityUserToken<Guid>>().ToTable("UserTokens");

        builder.Entity<ApplicationUser>().OwnsMany(o => o.Addresses);

        // Seed roles
        Guid adminRoleId = Guid.NewGuid();
        Guid userRoleId = Guid.NewGuid();

        List<IdentityRole<Guid>> roles =
        [
            new IdentityRole<Guid> { Id = adminRoleId, Name = "Administrator", NormalizedName = "ADMINISTRATOR" },
            new IdentityRole<Guid> { Id = userRoleId, Name = "User", NormalizedName = "USER" },
        ];

        builder.Entity<ApplicationRole>()
            .HasData(roles);

        // Seed role claims
        List<IdentityRoleClaim<Guid>> roleClaims =
        [
            // Administrator role claims
            new IdentityRoleClaim<Guid> { Id = 1, RoleId = adminRoleId, ClaimType = Permissions.ClaimType, ClaimValue = Permissions.UserManagement.Create },
            new IdentityRoleClaim<Guid> { Id = 2, RoleId = adminRoleId, ClaimType = Permissions.ClaimType, ClaimValue = Permissions.UserManagement.Read },
            new IdentityRoleClaim<Guid> { Id = 3, RoleId = adminRoleId, ClaimType = Permissions.ClaimType, ClaimValue = Permissions.UserManagement.Update },
            new IdentityRoleClaim<Guid> { Id = 4, RoleId = adminRoleId, ClaimType = Permissions.ClaimType, ClaimValue = Permissions.UserManagement.Delete },
            new IdentityRoleClaim<Guid> { Id = 5, RoleId = adminRoleId, ClaimType = Permissions.ClaimType, ClaimValue = Permissions.UserManagement.ManageRoles },
            new IdentityRoleClaim<Guid> { Id = 6, RoleId = adminRoleId, ClaimType = Permissions.ClaimType, ClaimValue = Permissions.UserManagement.ManageClaims },
            new IdentityRoleClaim<Guid> { Id = 7, RoleId = adminRoleId, ClaimType = Permissions.ClaimType, ClaimValue = Permissions.UserManagement.Lock },
            new IdentityRoleClaim<Guid> { Id = 8, RoleId = adminRoleId, ClaimType = Permissions.ClaimType, ClaimValue = Permissions.UserManagement.Unlock },
            new IdentityRoleClaim<Guid> { Id = 9, RoleId = adminRoleId, ClaimType = Permissions.ClaimType, ClaimValue = Permissions.UserManagement.ResetPassword },
            new IdentityRoleClaim<Guid> { Id = 10, RoleId = adminRoleId, ClaimType = Permissions.ClaimType, ClaimValue = Permissions.RoleManagement.Create },
            new IdentityRoleClaim<Guid> { Id = 11, RoleId = adminRoleId, ClaimType = Permissions.ClaimType, ClaimValue = Permissions.RoleManagement.Read },
            new IdentityRoleClaim<Guid> { Id = 12, RoleId = adminRoleId, ClaimType = Permissions.ClaimType, ClaimValue = Permissions.RoleManagement.Update },
            new IdentityRoleClaim<Guid> { Id = 13, RoleId = adminRoleId, ClaimType = Permissions.ClaimType, ClaimValue = Permissions.RoleManagement.Delete },
            new IdentityRoleClaim<Guid> { Id = 14, RoleId = adminRoleId, ClaimType = Permissions.ClaimType, ClaimValue = Permissions.RoleManagement.ManageClaims },
            new IdentityRoleClaim<Guid> { Id = 15, RoleId = adminRoleId, ClaimType = Permissions.ClaimType, ClaimValue = Permissions.Security.EnableTwoFactorAuthentication },
            new IdentityRoleClaim<Guid> { Id = 16, RoleId = adminRoleId, ClaimType = Permissions.ClaimType, ClaimValue = Permissions.Security.DisableTwoFactorAuthentication },
            new IdentityRoleClaim<Guid> { Id = 17, RoleId = adminRoleId, ClaimType = Permissions.ClaimType, ClaimValue = Permissions.Security.ViewLoginHistory },
            new IdentityRoleClaim<Guid> { Id = 19, RoleId = adminRoleId, ClaimType = Permissions.ClaimType, ClaimValue = Permissions.AccessControl.Grant },
            new IdentityRoleClaim<Guid> { Id = 20, RoleId = adminRoleId, ClaimType = Permissions.ClaimType, ClaimValue = Permissions.AccessControl.Revoke },
            new IdentityRoleClaim<Guid> { Id = 21, RoleId = adminRoleId, ClaimType = Permissions.ClaimType, ClaimValue = Permissions.AccessControl.ViewLogs },
            new IdentityRoleClaim<Guid> { Id = 22, RoleId = adminRoleId, ClaimType = Permissions.ClaimType, ClaimValue = Permissions.ApplicationSettings.View },
            new IdentityRoleClaim<Guid> { Id = 23, RoleId = adminRoleId, ClaimType = Permissions.ClaimType, ClaimValue = Permissions.ApplicationSettings.Update },
            new IdentityRoleClaim<Guid> { Id = 24, RoleId = adminRoleId, ClaimType = Permissions.ClaimType, ClaimValue = Permissions.ApplicationSettings.ManageAPIKeys },
            new IdentityRoleClaim<Guid> { Id = 31, RoleId = adminRoleId, ClaimType = Permissions.ClaimType, ClaimValue = Permissions.Analytics.View },
            new IdentityRoleClaim<Guid> { Id = 32, RoleId = adminRoleId, ClaimType = Permissions.ClaimType, ClaimValue = Permissions.Analytics.Generate },
            new IdentityRoleClaim<Guid> { Id = 33, RoleId = adminRoleId, ClaimType = Permissions.ClaimType, ClaimValue = Permissions.Analytics.Export },

            // User role claims
            new IdentityRoleClaim<Guid> { Id = 37, RoleId = userRoleId, ClaimType = Permissions.ClaimType, ClaimValue = Permissions.UserManagement.Read },
            new IdentityRoleClaim<Guid> { Id = 38, RoleId = userRoleId, ClaimType = Permissions.ClaimType, ClaimValue = Permissions.UserManagement.Update },
        ];

        builder.Entity<IdentityRoleClaim<Guid>>()
            .HasData(roleClaims);

        // Seed administrator user
        Guid adminUserId = Guid.NewGuid();
        PasswordHasher<ApplicationUser> hasher = new();

        ApplicationUser adminUser = new()
        {
            Id = adminUserId,
            UserName = "admin",
            NormalizedUserName = "ADMIN",
            Email = "opencode@xklibursolutions.io",
            NormalizedEmail = "OPENCODE@XKLIBURSOLUTIONS.IO",
            EmailConfirmed = true,
            PasswordHash = hasher.HashPassword(null!, "admin"),
            SecurityStamp = string.Empty,
        };

        builder.Entity<ApplicationUser>()
            .HasData(adminUser);

        // Assign administrator role to admin user
        builder.Entity<IdentityUserRole<Guid>>()
            .HasData(new IdentityUserRole<Guid>
        {
            RoleId = adminRoleId,
            UserId = adminUserId
        });
    }

    /// <summary>
    /// Configures the database context to use a SQLite database with the connection string
    /// specified in the application configuration.
    /// </summary>
    /// <param name="optionsBuilder">The options builder used to configure the context.</param>
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        string connectionString = configuration.GetConnectionString("DefaultConnection")!;

        optionsBuilder.UseSqlite(connectionString);
    }
}
