using Microsoft.AspNetCore.Identity;

namespace XkliburSolutions.Shield.Domain.Entities;

/// <summary>
/// Represents an application-specific role that extends the IdentityRole class.
/// </summary>
public class ApplicationRole : IdentityRole<Guid>
{
}
