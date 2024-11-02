using Azure;
using Azure.Communication.Email;
using Microsoft.Extensions.Options;
using XkliburSolutions.Shield.CrossCutting.Configuration;

namespace XkliburSolutions.Shield.Infrastructure.Services;

/// <summary>
/// Service for sending emails using Azure Communication Services.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="AzureCommunicationEmailService"/> class.
/// </remarks>
/// <param name="communicationServiceOptions">The options for Azure Communication Service.</param>
public class AzureCommunicationEmailService(
    IOptions<AzureCommunicationService> communicationServiceOptions)
    : ICommunicationService
{
    /// <summary>
    /// Sends an email to the specified recipients.
    /// </summary>
    /// <returns>True if the email was sent successfully; otherwise, false.</returns>
    public bool SendEmail(string emailSubject, string emailBody, string from, params string[] to)
    {
        // Retrieve the connection string from the configuration options.
        string connectionString = communicationServiceOptions.Value.ConnectionString;
        var emailClient = new EmailClient(connectionString);

        // Create a list of email recipients.
        EmailAddress[] recipients = to.Select(x => new EmailAddress(x)).ToArray();

        // Create the email message.
        var emailMessage = new EmailMessage(
            senderAddress: from,
            content: new EmailContent(emailSubject)
            {
                Html = emailBody,
            },
            recipients: new EmailRecipients(recipients));

        // Send the email and wait for the operation to complete.
        EmailSendOperation emailSendOperation = emailClient.Send(
            WaitUntil.Started,
            emailMessage);

        // Return true to indicate the email was sent successfully.
        return true;
    }
}
