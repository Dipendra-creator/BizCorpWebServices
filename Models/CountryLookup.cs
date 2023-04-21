using System;
using System.Collections.Generic;

namespace BizCorp.Models;

public partial class CountryLookup
{
    public int CountryLookupId { get; set; }

    public string CountryName { get; set; } = null!;

    public string? CountryCodeName { get; set; }

    /// <summary>
    /// +00
    /// </summary>
    public string? CountryCode { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual ICollection<CityLookup> CityLookups { get; } = new List<CityLookup>();

    public virtual ICollection<UserPast> UserPasts { get; } = new List<UserPast>();

    public virtual ICollection<User> Users { get; } = new List<User>();
}
