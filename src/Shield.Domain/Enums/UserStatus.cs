namespace XkliburSolutions.Shield.Domain.Enums;

/// <summary>
/// Represents the status of a user.
/// </summary>
public enum UserStatus
{
    /// <summary>
    /// The user is active and has full access.
    /// </summary>
    Active,

    /// <summary>
    /// The user is inactive and cannot access the system.
    /// </summary>
    Inactive,

    /// <summary>
    /// The user is pending approval or activation.
    /// </summary>
    Pending,

    /// <summary>
    /// The user is suspended and temporarily cannot access the system.
    /// </summary>
    Suspended,

    /// <summary>
    /// The user is banned and permanently cannot access the system.
    /// </summary>
    Banned
}
