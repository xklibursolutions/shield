using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Moq;
using XkliburSolutions.Shield.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using XkliburSolutions.Shield.Core.Entities;

namespace Shield.Infrastructure.Tests.Data;

public class ApplicationDbContextTests
{
    private readonly DbContextOptions<TestDbContext> _dbContextOptions;
    private readonly Mock<IConfiguration> _configurationMock;

    public ApplicationDbContextTests()
    {
        _dbContextOptions = new DbContextOptionsBuilder<TestDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

        _configurationMock = new Mock<IConfiguration>();
        var configurationSectionMock = new Mock<IConfigurationSection>();
        configurationSectionMock.Setup(a => a.Value).Returns("DataSource=:memory:");
        _configurationMock.Setup(a => a.GetSection("ConnectionStrings:DefaultConnection")).Returns(configurationSectionMock.Object);
    }

    [Fact]
    public void OnModelCreating_ShouldConfigureEntities()
    {
        // Arrange
        using TestDbContext context = new(_dbContextOptions, _configurationMock.Object);

        // Act
        Microsoft.EntityFrameworkCore.Metadata.IModel model = context.Model;

        // Assert
        Assert.NotNull(model.FindEntityType(typeof(ApplicationUser)));
        Assert.NotNull(model.FindEntityType(typeof(ApplicationRole)));
        Assert.NotNull(model.FindEntityType(typeof(IdentityUserRole<Guid>)));
        Assert.NotNull(model.FindEntityType(typeof(IdentityUserClaim<Guid>)));
        Assert.NotNull(model.FindEntityType(typeof(IdentityRoleClaim<Guid>)));
        Assert.NotNull(model.FindEntityType(typeof(IdentityUserLogin<Guid>)));
        Assert.NotNull(model.FindEntityType(typeof(IdentityUserToken<Guid>)));
    }

    [Fact]
    public async void SeedData_ShouldSeedRolesAndUsers()
    {
        // Arrange
        using TestDbContext context = new(
            _dbContextOptions,
            _configurationMock.Object);

        // Act
        context.Database.EnsureCreated();

        // Assert
        Assert.Equal(2, await context.Roles.CountAsync());
        Assert.Equal(28, await context.RoleClaims.CountAsync());
        Assert.Equal(1, await context.Users.CountAsync());
        Assert.Equal(1, await context.UserRoles.CountAsync());
    }
}

public class TestDbContext : ApplicationDbContext
{
    public TestDbContext(DbContextOptions options, IConfiguration configuration)
        : base(options, configuration)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseInMemoryDatabase(databaseName: "TestDatabase");
    }
}
