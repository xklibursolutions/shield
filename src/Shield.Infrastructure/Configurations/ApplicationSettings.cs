namespace XkliburSolutions.Shield.Infrastructure.Configurations;

/// <summary>
/// Represents the application settings.
/// </summary>
public class ApplicationSettings
{
    /// <summary>
    /// Gets or sets the registration settings.
    /// </summary>
    public RegistrationSettings? RegistrationSettings { get; set; }

    /// <summary>
    /// Gets or sets the Azure Communication Service settings.
    /// </summary>
    public AzureCommunicationServiceSettings? AzureCommunicationService { get; set; }

    /// <summary>
    /// Gets or sets the base URL of the web application.
    /// </summary>
    public string? WebAppBaseUrl { get; set; }

    /// <summary>
    /// Gets or sets the sender email address for communications.
    /// </summary>
    public string FromEmailAddress { get; set; } = "";
}
