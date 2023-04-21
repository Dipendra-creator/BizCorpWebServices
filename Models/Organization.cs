using System;
using System.Collections.Generic;

namespace BizCorp.Models;

public partial class Organization
{
    public int OrganizationId { get; set; }

    public string OrganizationName { get; set; } = null!;

    public int OrganizationType { get; set; }

    public string? OrganizationAddress { get; set; }

    public long? OrganizationNumberEmployee { get; set; }

    public string? OrganizationDescription { get; set; }

    public string? OrganizationTag { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual OrganizationTypeLookup OrganizationTypeNavigation { get; set; } = null!;

    public virtual ICollection<ReactionType> ReactionTypes { get; } = new List<ReactionType>();

    public virtual ICollection<UserPast> UserPasts { get; } = new List<UserPast>();

    public virtual ICollection<User> Users { get; } = new List<User>();
}
