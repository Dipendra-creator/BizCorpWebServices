using System;
using System.Collections.Generic;

namespace BizCorp.Models;

public partial class ReactionType
{
    public int ReactionTypeId { get; set; }

    public int? OrganizationId { get; set; }

    public string? Name { get; set; }

    public byte[]? Image { get; set; }

    public DateTime? EndDate { get; set; }

    public virtual Organization? Organization { get; set; }

    public virtual ICollection<PostReaction> PostReactions { get; } = new List<PostReaction>();
}
