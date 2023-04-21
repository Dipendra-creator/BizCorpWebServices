using System;
using System.Collections.Generic;

namespace BizCorp.Models;

public partial class PostReaction
{
    public int PostReactionId { get; set; }

    public int? UserId { get; set; }

    public int? ReactionId { get; set; }

    public int? PostId { get; set; }

    public virtual Post? Post { get; set; }

    public virtual ReactionType? Reaction { get; set; }

    public virtual User? User { get; set; }
}
