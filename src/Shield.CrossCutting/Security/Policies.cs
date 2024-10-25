namespace XkliburSolutions.Shield.CrossCutting.Security;

/// <summary>
/// Provides a set of constants representing various policies within the application.
/// </summary>
public static class Policies
{
    /// <summary>
    /// Contains constants for user management policies.
    /// </summary>
    public static class UserManagement
    {
        /// <summary>
        /// Policy for creating a user.
        /// </summary>
        public const string Create = Permissions.UserManagement.Create;

        /// <summary>
        /// Policy for reading user information.
        /// </summary>
        public const string Read = Permissions.UserManagement.Read;

        /// <summary>
        /// Policy for updating user information.
        /// </summary>
        public const string Update = Permissions.UserManagement.Update;

        /// <summary>
        /// Policy for deleting a user.
        /// </summary>
        public const string Delete = Permissions.UserManagement.Delete;

        /// <summary>
        /// Policy for managing user roles.
        /// </summary>
        public const string ManageRoles = Permissions.UserManagement.ManageRoles;

        /// <summary>
        /// Policy for managing user claims.
        /// </summary>
        public const string ManageClaims = Permissions.UserManagement.ManageClaims;

        /// <summary>
        /// Policy for locking a user account.
        /// </summary>
        public const string Lock = Permissions.UserManagement.Lock;

        /// <summary>
        /// Policy for unlocking a user account.
        /// </summary>
        public const string Unlock = Permissions.UserManagement.Unlock;

        /// <summary>
        /// Policy for resetting a user's password.
        /// </summary>
        public const string ResetPassword = Permissions.UserManagement.ResetPassword;
    }

    /// <summary>
    /// Contains constants for role management policies.
    /// </summary>
    public static class RoleManagement
    {
        /// <summary>
        /// Policy for creating a role.
        /// </summary>
        public const string Create = Permissions.RoleManagement.Create;

        /// <summary>
        /// Policy for reading role information.
        /// </summary>
        public const string Read = Permissions.RoleManagement.Read;

        /// <summary>
        /// Policy for updating role information.
        /// </summary>
        public const string Update = Permissions.RoleManagement.Update;

        /// <summary>
        /// Policy for deleting a role.
        /// </summary>
        public const string Delete = Permissions.RoleManagement.Delete;

        /// <summary>
        /// Policy for managing role claims.
        /// </summary>
        public const string ManageClaims = Permissions.RoleManagement.ManageClaims;
    }

    /// <summary>
    /// Contains constants for security-related policies.
    /// </summary>
    public static class Security
    {
        /// <summary>
        /// Policy for enabling two-factor authentication.
        /// </summary>
        public const string EnableTwoFactorAuthentication = Permissions.Security.EnableTwoFactorAuthentication;

        /// <summary>
        /// Policy for disabling two-factor authentication.
        /// </summary>
        public const string DisableTwoFactorAuthentication = Permissions.Security.DisableTwoFactorAuthentication;

        /// <summary>
        /// Policy for viewing login history.
        /// </summary>
        public const string ViewLoginHistory = Permissions.Security.ViewLoginHistory;
    }

    /// <summary>
    /// Contains constants for access control policies.
    /// </summary>
    public static class AccessControl
    {
        /// <summary>
        /// Policy for granting access.
        /// </summary>
        public const string Grant = Permissions.AccessControl.Grant;

        /// <summary>
        /// Policy for revoking access.
        /// </summary>
        public const string Revoke = Permissions.AccessControl.Revoke;

        /// <summary>
        /// Policy for viewing access logs.
        /// </summary>
        public const string ViewLogs = Permissions.AccessControl.ViewLogs;
    }

    /// <summary>
    /// Contains constants for application settings policies.
    /// </summary>
    public static class ApplicationSettings
    {
        /// <summary>
        /// Policy for viewing application settings.
        /// </summary>
        public const string View = Permissions.ApplicationSettings.View;

        /// <summary>
        /// Policy for updating application settings.
        /// </summary>
        public const string Update = Permissions.ApplicationSettings.Update;

        /// <summary>
        /// Policy for managing API keys.
        /// </summary>
        public const string ManageAPIKeys = Permissions.ApplicationSettings.ManageAPIKeys;
    }

    /// <summary>
    /// Contains constants for analytics policies.
    /// </summary>
    public static class Analytics
    {
        /// <summary>
        /// Policy for viewing analytics.
        /// </summary>
        public const string View = Permissions.Analytics.View;

        /// <summary>
        /// Policy for generating analytics reports.
        /// </summary>
        public const string Generate = Permissions.Analytics.Generate;

        /// <summary>
        /// Policy for exporting analytics data.
        /// </summary>
        public const string Export = Permissions.Analytics.Export;
    }
}
