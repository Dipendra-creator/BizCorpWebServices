using System;
using System.Collections.Generic;

namespace BizCorp.Models;

public partial class User
{
    public int UserId { get; set; }

    public string Name { get; set; } = null!;

    public DateTime? Dob { get; set; }

    public string Username { get; set; } = null!;

    public string? Email { get; set; }

    public string? PasswordHash { get; set; }

    public long? Exprience { get; set; }

    public int? Organization { get; set; }

    public DateTime? OrganizationJoinDate { get; set; }

    /// <summary>
    /// Ammount Of salary in LPA
    /// </summary>
    public long? Salary { get; set; }

    /// <summary>
    /// Residential Address
    /// </summary>
    public string? Address { get; set; }

    public int? City { get; set; }

    public int? Country { get; set; }

    public int? NumberPosts { get; set; }

    public DateTime? LastSeen { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual CityLookup? CityNavigation { get; set; }

    public virtual CountryLookup? CountryNavigation { get; set; }

    public virtual Organization? OrganizationNavigation { get; set; }

    public virtual ICollection<PostComment> PostComments { get; } = new List<PostComment>();

    public virtual ICollection<PostReaction> PostReactions { get; } = new List<PostReaction>();

    public virtual ICollection<Post> Posts { get; } = new List<Post>();

    public virtual ICollection<UserPast> UserPasts { get; } = new List<UserPast>();
}
