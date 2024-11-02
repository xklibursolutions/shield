using XkliburSolutions.Shield.Domain.Entities;

namespace XkliburSolutions.Shield.Infrastructure.Services;

/// <summary>
/// Defines the contract for communication services.
/// </summary>
public interface ICommunicationService
{
    /// <summary>
    /// Sends a registration email to the specified recipients.
    /// </summary>
    /// <returns>True if the email was sent successfully; otherwise, false.</returns>
    bool SendEmail(string emailSubject, string emailBody, string from, params string[] to);
}
