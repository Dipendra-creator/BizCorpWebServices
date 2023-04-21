using System;
using System.Collections.Generic;

namespace BizCorp.Models;

public partial class PostComment
{
    public int PostCommentsId { get; set; }

    public int? Post { get; set; }

    public string PostComment1 { get; set; } = null!;

    public long? PostCommentLikes { get; set; }

    public int? FromUser { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual User? FromUserNavigation { get; set; }

    public virtual Post? PostNavigation { get; set; }
}
