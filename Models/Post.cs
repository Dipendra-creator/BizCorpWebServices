using System;
using System.Collections.Generic;

namespace BizCorp.Models;

public partial class Post
{
    public int PostId { get; set; }

    public int PostUser { get; set; }

    public string? PostType { get; set; }

    public string? PostTags { get; set; }

    /// <summary>
    /// Post content
    /// </summary>
    public string? PostDescription { get; set; }

    /// <summary>
    /// Post pictorial content
    /// </summary>
    public byte[]? PostBlob { get; set; }

    public int? PostLikes { get; set; }

    public int? PostViews { get; set; }

    public int? PostComments { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual ICollection<PostComment> PostCommentsNavigation { get; } = new List<PostComment>();

    public virtual ICollection<PostReaction> PostReactions { get; } = new List<PostReaction>();

    public virtual User PostUserNavigation { get; set; } = null!;
}
