namespace XkliburSolutions.Shield.CrossCutting.Configuration;

/// <summary>
/// Represents the settings for Azure Communication Service.
/// </summary>
public class AzureCommunicationService
{
    /// <summary>
    /// Gets or sets the connection string for Azure Communication Service.
    /// </summary>
    public string ConnectionString { get; set; } = "";

    /// <summary>
    /// Gets or sets the sender email address used by Azure Communication Service.
    /// </summary>
    public string SenderEmail { get; set; } = "";
}
