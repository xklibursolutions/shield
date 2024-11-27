using Microsoft.Extensions.Configuration;
using XkliburSolutions.Shield.Infrastructure.Configurations;

namespace Shield.IntegrationTests.Configuration;

public class ApplicationSettingsTests
{
    [Fact]
    public void ApplicationSettings_ShouldBindCorrectly()
    {
        // Arrange
        Dictionary<string, string?> inMemorySettings = new()
        {
            {"ApplicationSettings:RegistrationSettings:RequireConfirmation", "true"},
            {"ApplicationSettings:AzureCommunicationService:ConnectionString", "Endpoint=sb://example.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=abc123"},
            {"ApplicationSettings:AzureCommunicationService:SenderEmail", "sender@example.com"},
            {"ApplicationSettings:WebAppBaseUrl", "https://example.com"},
            {"ApplicationSettings:FromEmailAddress", "noreply@example.com"}
        };

        IConfiguration configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(inMemorySettings)
            .Build();

        // Act
        ApplicationSettings appSettings = new();
        configuration.GetSection("ApplicationSettings").Bind(appSettings);

        // Assert
        Assert.NotNull(appSettings.RegistrationSettings);
        Assert.True(appSettings.RegistrationSettings.RequireConfirmation);
        Assert.NotNull(appSettings.AzureCommunicationService);
        Assert.Equal("Endpoint=sb://example.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=abc123", appSettings.AzureCommunicationService.ConnectionString);
        Assert.Equal("sender@example.com", appSettings.AzureCommunicationService.SenderEmail);
        Assert.Equal("https://example.com", appSettings.WebAppBaseUrl);
        Assert.Equal("noreply@example.com", appSettings.FromEmailAddress);
    }

    [Fact]
    public void RegistrationSettings_ShouldBindCorrectly()
    {
        // Arrange
        Dictionary<string, string?> inMemorySettings = new()
        {
            {"RegistrationSettings:RequireConfirmation", "true"}
        };

        IConfiguration configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(inMemorySettings)
            .Build();

        // Act
        RegistrationSettings registrationSettings = new();
        configuration.GetSection("RegistrationSettings").Bind(registrationSettings);

        // Assert
        Assert.True(registrationSettings.RequireConfirmation);
    }

    [Fact]
    public void AzureCommunicationServiceSettings_ShouldBindCorrectly()
    {
        // Arrange
        Dictionary<string, string?> inMemorySettings = new()
        {
            {"AzureCommunicationServiceSettings:ConnectionString", "Endpoint=sb://example.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=abc123"},
            {"AzureCommunicationServiceSettings:SenderEmail", "sender@example.com"}
        };

        IConfiguration configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(inMemorySettings)
            .Build();

        // Act
        AzureCommunicationServiceSettings azureCommunicationServiceSettings = new();
        configuration.GetSection("AzureCommunicationServiceSettings").Bind(azureCommunicationServiceSettings);

        // Assert
        Assert.Equal("Endpoint=sb://example.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=abc123", azureCommunicationServiceSettings.ConnectionString);
        Assert.Equal("sender@example.com", azureCommunicationServiceSettings.SenderEmail);
    }
}
