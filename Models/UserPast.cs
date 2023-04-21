using System;
using System.Collections.Generic;

namespace BizCorp.Models;

public partial class UserPast
{
    public int UserPastId { get; set; }

    public int? User { get; set; }

    public int? Organization { get; set; }

    public int? City { get; set; }

    public int? Country { get; set; }

    public DateTime? FromDate { get; set; }

    public DateTime? ToDate { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual CityLookup? CityNavigation { get; set; }

    public virtual CountryLookup? CountryNavigation { get; set; }

    public virtual Organization? OrganizationNavigation { get; set; }

    public virtual User? UserNavigation { get; set; }
}
