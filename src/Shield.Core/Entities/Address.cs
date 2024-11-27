using System.ComponentModel.DataAnnotations;
using XkliburSolutions.Shield.Core.Enums;

namespace XkliburSolutions.Shield.Core.Entities;

/// <summary>
/// Represents an address.
/// </summary>
public class Address
{
    /// <summary>
    /// Gets or sets the address type such as home or office.
    /// </summary>
    public required AddressType Type { get; set; }
    /// <summary>
    /// Gets or sets the address line 1.
    /// </summary>
    public string? AddressLine1 { get; set; }

    /// <summary>
    /// Gets or sets the address line 2.
    /// </summary>
    public string? AddressLine2 { get; set; }

    /// <summary>
    /// Gets or sets the address line 3.
    /// </summary>
    public string? AddressLine3 { get; set; }

    /// <summary>
    /// Gets or sets the city of the address.
    /// </summary>
    public string? City { get; set; }

    /// <summary>
    /// Gets or sets the state or province of the address.
    /// </summary>
    public string? Region { get; set; }

    /// <summary>
    /// Gets or sets the combined postal code, ZIP code, Eircode, PIN, CAP, and block number.
    /// </summary>
    [RegularExpression(
        @"^(\d{5}(-\d{4})?|[A-Za-z]\d[A-Za-z] \d[A-Za-z]\d|[A-Za-z0-9]{7}|\d{6}|\d{5}|\d{1,4}|[A-Za-z]{1,2}\d[A-Za-z\d]? \d[A-Za-z]{2}|\d{4} [A-Za-z]{2}|[A-Za-z]{3} \d{4}|[A-Za-z]?\d{4}[A-Za-z]{0,3}|\d{5}-\d{3}|\d{4,6})$",
        ErrorMessage = "Invalid Code Format")]
    public string? Code { get; set; }

    /// <summary>
    /// Gets or sets the country of the address.
    /// </summary>
    public string? Country { get; set; }
}
