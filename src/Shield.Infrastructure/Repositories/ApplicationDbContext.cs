using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using XkliburSolutions.Shield.CrossCutting.Entities;

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
public class ApplicationDbContext(DbContextOptions options)
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

        builder.Entity<IdentityRole>().ToTable("Roles");
        builder.Entity<ApplicationUser>().ToTable("Users");
        builder.Entity<IdentityUserRole<Guid>>().ToTable("UserRoles");
        builder.Entity<IdentityUserClaim<Guid>>().ToTable("UserClaims");
        builder.Entity<IdentityRoleClaim<Guid>>().ToTable("RoleClaims");
        builder.Entity<IdentityUserLogin<Guid>>().ToTable("UserLogins");
        builder.Entity<IdentityUserToken<Guid>>().ToTable("UserTokens");

        // Ensure unique constraints
        builder.Entity<ApplicationUser>()
            .HasIndex(u => u.UserName)
            .IsUnique();

        builder.Entity<ApplicationRole>()
            .HasIndex(r => r.Name)
            .IsUnique();
    }
}
