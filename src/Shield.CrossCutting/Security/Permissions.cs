namespace XkliburSolutions.Shield.CrossCutting.Security;

/// <summary>
/// Provides a set of constants representing various permissions within the application.
/// </summary>
public static class Permissions
{
    /// <summary>
    /// Claim Type for Permissions.
    /// </summary>
    public const string ClaimType = "Permission";

    /// <summary>
    /// Contains constants for user management permissions.
    /// </summary>
    public static class UserManagement
    {
        /// <summary>
        /// Permission to create a user.
        /// </summary>
        public const string Create = "UserManagement.Create";

        /// <summary>
        /// Permission to read user information.
        /// </summary>
        public const string Read = "UserManagement.Read";

        /// <summary>
        /// Permission to update user information.
        /// </summary>
        public const string Update = "UserManagement.Update";

        /// <summary>
        /// Permission to delete a user.
        /// </summary>
        public const string Delete = "UserManagement.Delete";

        /// <summary>
        /// Permission to manage user roles.
        /// </summary>
        public const string ManageRoles = "UserManagement.ManageRoles";

        /// <summary>
        /// Permission to manage user claims.
        /// </summary>
        public const string ManageClaims = "UserManagement.ManageClaims";

        /// <summary>
        /// Permission to lock a user account.
        /// </summary>
        public const string Lock = "UserManagement.Lock";

        /// <summary>
        /// Permission to unlock a user account.
        /// </summary>
        public const string Unlock = "UserManagement.Unlock";

        /// <summary>
        /// Permission to reset a user's password.
        /// </summary>
        public const string ResetPassword = "UserManagement.ResetPassword";
    }

    /// <summary>
    /// Contains constants for role management permissions.
    /// </summary>
    public static class RoleManagement
    {
        /// <summary>
        /// Permission to create a role.
        /// </summary>
        public const string Create = "RoleManagement.Create";

        /// <summary>
        /// Permission to read role information.
        /// </summary>
        public const string Read = "RoleManagement.Read";

        /// <summary>
        /// Permission to update role information.
        /// </summary>
        public const string Update = "RoleManagement.Update";

        /// <summary>
        /// Permission to delete a role.
        /// </summary>
        public const string Delete = "RoleManagement.Delete";

        /// <summary>
        /// Permission to manage role claims.
        /// </summary>
        public const string ManageClaims = "RoleManagement.ManageClaims";
    }

    /// <summary>
    /// Contains constants for security-related permissions.
    /// </summary>
    public static class Security
    {
        /// <summary>
        /// Permission to enable two-factor authentication.
        /// </summary>
        public const string EnableTwoFactorAuthentication = "Security.EnableTwoFactorAuthentication";

        /// <summary>
        /// Permission to disable two-factor authentication.
        /// </summary>
        public const string DisableTwoFactorAuthentication = "Security.DisableTwoFactorAuthentication";

        /// <summary>
        /// Permission to view login history.
        /// </summary>
        public const string ViewLoginHistory = "Security.ViewLoginHistory";
    }

    /// <summary>
    /// Contains constants for access control permissions.
    /// </summary>
    public static class AccessControl
    {
        /// <summary>
        /// Permission to grant access.
        /// </summary>
        public const string Grant = "AccessControl.Grant";

        /// <summary>
        /// Permission to revoke access.
        /// </summary>
        public const string Revoke = "AccessControl.Revoke";

        /// <summary>
        /// Permission to view access logs.
        /// </summary>
        public const string ViewLogs = "AccessControl.ViewLogs";
    }

    /// <summary>
    /// Contains constants for application settings permissions.
    /// </summary>
    public static class ApplicationSettings
    {
        /// <summary>
        /// Permission to view application settings.
        /// </summary>
        public const string View = "ApplicationSettings.View";

        /// <summary>
        /// Permission to update application settings.
        /// </summary>
        public const string Update = "ApplicationSettings.Update";

        /// <summary>
        /// Permission to manage API keys.
        /// </summary>
        public const string ManageAPIKeys = "ApplicationSettings.ManageAPIKeys";
    }

    /// <summary>
    /// Contains constants for analytics permissions.
    /// </summary>
    public static class Analytics
    {
        /// <summary>
        /// Permission to view analytics.
        /// </summary>
        public const string View = "Analytics.View";

        /// <summary>
        /// Permission to generate analytics reports.
        /// </summary>
        public const string Generate = "Analytics.Generate";

        /// <summary>
        /// Permission to export analytics data.
        /// </summary>
        public const string Export = "Analytics.Export";
    }
}
