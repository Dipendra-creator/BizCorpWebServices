using System;
using System.Collections.Generic;

namespace BizCorp.Models;

public partial class OrganizationTypeLookup
{
    public int OrganizationTypeLookupId { get; set; }

    public string OrganizationTypeName { get; set; } = null!;

    public string? OrganizationTypeDescription { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual ICollection<Organization> Organizations { get; } = new List<Organization>();
}
