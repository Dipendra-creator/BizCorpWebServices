using System;
using System.Collections.Generic;

namespace BizCorp.Models;

public partial class CityLookup
{
    public int CityLookupId { get; set; }

    public string CityName { get; set; } = null!;

    public long? CityCode { get; set; }

    public int? CountryId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual CountryLookup? Country { get; set; }

    public virtual ICollection<UserPast> UserPasts { get; } = new List<UserPast>();

    public virtual ICollection<User> Users { get; } = new List<User>();
}
