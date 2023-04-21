using System;
using System.Collections.Generic;
using BizCorp.Models;
using Microsoft.EntityFrameworkCore;

namespace BizCorp.Data;

public partial class BizCorpContext : DbContext
{
    public BizCorpContext(DbContextOptions<BizCorpContext> options)
        : base(options)
    {
    }

    public virtual DbSet<CityLookup> CityLookups { get; set; }

    public virtual DbSet<CountryLookup> CountryLookups { get; set; }

    public virtual DbSet<Organization> Organizations { get; set; }

    public virtual DbSet<OrganizationTypeLookup> OrganizationTypeLookups { get; set; }

    public virtual DbSet<Post> Posts { get; set; }

    public virtual DbSet<PostComment> PostComments { get; set; }

    public virtual DbSet<PostReaction> PostReactions { get; set; }

    public virtual DbSet<ReactionType> ReactionTypes { get; set; }

    public virtual DbSet<Trai> Trais { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserPast> UserPasts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<CityLookup>(entity =>
        {
            entity.HasKey(e => e.CityLookupId).HasName("PRIMARY");

            entity.ToTable("city_lookup");

            entity.HasIndex(e => e.CityCode, "city_lookup_index_10");

            entity.HasIndex(e => e.CityName, "city_lookup_index_11");

            entity.HasIndex(e => e.CityLookupId, "city_lookup_index_9");

            entity.HasIndex(e => e.CountryId, "country_id");

            entity.Property(e => e.CityCode).HasColumnName("city_code");
            entity.Property(e => e.CityName)
                .HasMaxLength(45)
                .HasColumnName("city_name");
            entity.Property(e => e.CountryId).HasColumnName("country_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnType("datetime")
                .HasColumnName("created_at");

            entity.HasOne(d => d.Country).WithMany(p => p.CityLookups)
                .HasForeignKey(d => d.CountryId)
                .HasConstraintName("city_lookup_ibfk_1");
        });

        modelBuilder.Entity<CountryLookup>(entity =>
        {
            entity.HasKey(e => e.CountryLookupId).HasName("PRIMARY");

            entity.ToTable("country_lookup");

            entity.HasIndex(e => e.CountryLookupId, "country_lookup_index_6");

            entity.HasIndex(e => e.CountryCode, "country_lookup_index_7");

            entity.HasIndex(e => e.CountryName, "country_lookup_index_8");

            entity.Property(e => e.CountryCode)
                .HasComment("+00")
                .HasColumnName("country_code");
            entity.Property(e => e.CountryCodeName)
                .HasMaxLength(255)
                .HasColumnName("country_code_name");
            entity.Property(e => e.CountryName).HasColumnName("country_name");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
        });

        modelBuilder.Entity<Organization>(entity =>
        {
            entity.HasKey(e => e.OrganizationId).HasName("PRIMARY");

            entity.ToTable("organization");

            entity.HasIndex(e => e.OrganizationId, "organization_index_1");

            entity.HasIndex(e => e.OrganizationType, "organization_type");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.OrganizationAddress)
                .HasMaxLength(255)
                .HasColumnName("organization_address");
            entity.Property(e => e.OrganizationDescription)
                .HasMaxLength(255)
                .HasColumnName("organization_description");
            entity.Property(e => e.OrganizationName)
                .HasMaxLength(255)
                .HasColumnName("organization_name");
            entity.Property(e => e.OrganizationNumberEmployee).HasColumnName("organization_number_employee");
            entity.Property(e => e.OrganizationTag)
                .HasMaxLength(255)
                .HasColumnName("organization_tag");
            entity.Property(e => e.OrganizationType).HasColumnName("organization_type");

            entity.HasOne(d => d.OrganizationTypeNavigation).WithMany(p => p.Organizations)
                .HasForeignKey(d => d.OrganizationType)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("organization_ibfk_1");
        });

        modelBuilder.Entity<OrganizationTypeLookup>(entity =>
        {
            entity.HasKey(e => e.OrganizationTypeLookupId).HasName("PRIMARY");

            entity.ToTable("organization_type_lookup");

            entity.HasIndex(e => e.OrganizationTypeLookupId, "organization_type_lookup_index_0");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.OrganizationTypeDescription)
                .HasMaxLength(255)
                .HasColumnName("organization_type_description");
            entity.Property(e => e.OrganizationTypeName)
                .HasMaxLength(255)
                .HasColumnName("organization_type_name");
        });

        modelBuilder.Entity<Post>(entity =>
        {
            entity.HasKey(e => e.PostId).HasName("PRIMARY");

            entity.ToTable("posts");

            entity.HasIndex(e => e.PostUser, "post_user");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.PostBlob)
                .HasComment("Post pictorial content")
                .HasColumnType("blob")
                .HasColumnName("post_blob");
            entity.Property(e => e.PostComments).HasColumnName("post_comments");
            entity.Property(e => e.PostDescription)
                .HasMaxLength(255)
                .HasComment("Post content")
                .HasColumnName("post_description");
            entity.Property(e => e.PostLikes)
                .HasDefaultValueSql("'0'")
                .HasColumnName("post_likes");
            entity.Property(e => e.PostTags)
                .HasMaxLength(255)
                .HasColumnName("post_tags");
            entity.Property(e => e.PostType)
                .HasMaxLength(255)
                .HasColumnName("post_type");
            entity.Property(e => e.PostUser).HasColumnName("post_user");
            entity.Property(e => e.PostViews)
                .HasDefaultValueSql("'0'")
                .HasColumnName("post_views");

            entity.HasOne(d => d.PostUserNavigation).WithMany(p => p.Posts)
                .HasForeignKey(d => d.PostUser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("posts_ibfk_1");
        });

        modelBuilder.Entity<PostComment>(entity =>
        {
            entity.HasKey(e => e.PostCommentsId).HasName("PRIMARY");

            entity.ToTable("post_comments");

            entity.HasIndex(e => e.FromUser, "from_user");

            entity.HasIndex(e => e.Post, "post");

            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.FromUser).HasColumnName("from_user");
            entity.Property(e => e.Post).HasColumnName("post");
            entity.Property(e => e.PostComment1)
                .HasMaxLength(255)
                .HasColumnName("post_comment");
            entity.Property(e => e.PostCommentLikes)
                .HasDefaultValueSql("'0'")
                .HasColumnName("post_comment_likes");

            entity.HasOne(d => d.FromUserNavigation).WithMany(p => p.PostComments)
                .HasForeignKey(d => d.FromUser)
                .HasConstraintName("post_comments_ibfk_2");

            entity.HasOne(d => d.PostNavigation).WithMany(p => p.PostCommentsNavigation)
                .HasForeignKey(d => d.Post)
                .HasConstraintName("post_comments_ibfk_1");
        });

        modelBuilder.Entity<PostReaction>(entity =>
        {
            entity.HasKey(e => e.PostReactionId).HasName("PRIMARY");

            entity.ToTable("post_reaction");

            entity.HasIndex(e => e.PostId, "post_id");

            entity.HasIndex(e => e.ReactionId, "reaction_id");

            entity.HasIndex(e => e.UserId, "user_id");

            entity.Property(e => e.PostId).HasColumnName("post_id");
            entity.Property(e => e.ReactionId).HasColumnName("reaction_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Post).WithMany(p => p.PostReactions)
                .HasForeignKey(d => d.PostId)
                .HasConstraintName("post_reaction_ibfk_3");

            entity.HasOne(d => d.Reaction).WithMany(p => p.PostReactions)
                .HasForeignKey(d => d.ReactionId)
                .HasConstraintName("post_reaction_ibfk_2");

            entity.HasOne(d => d.User).WithMany(p => p.PostReactions)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("post_reaction_ibfk_1");
        });

        modelBuilder.Entity<ReactionType>(entity =>
        {
            entity.HasKey(e => e.ReactionTypeId).HasName("PRIMARY");

            entity.ToTable("reaction_type");

            entity.HasIndex(e => e.OrganizationId, "organization_id");

            entity.Property(e => e.EndDate)
                .HasColumnType("datetime")
                .HasColumnName("end_date");
            entity.Property(e => e.Image)
                .HasColumnType("blob")
                .HasColumnName("image");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.OrganizationId).HasColumnName("organization_id");

            entity.HasOne(d => d.Organization).WithMany(p => p.ReactionTypes)
                .HasForeignKey(d => d.OrganizationId)
                .HasConstraintName("reaction_type_ibfk_1");
        });

        modelBuilder.Entity<Trai>(entity =>
        {
            entity.HasKey(e => e.TraiId).HasName("PRIMARY");

            entity.ToTable("trai");

            entity.Property(e => e.TraiId).HasColumnName("trai_id");
            entity.Property(e => e.Month)
                .HasMaxLength(45)
                .HasColumnName("month");
            entity.Property(e => e.PNumber)
                .HasMaxLength(45)
                .HasColumnName("p_number");
            entity.Property(e => e.Year)
                .HasMaxLength(45)
                .HasColumnName("year");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PRIMARY");

            entity.ToTable("user");

            entity.HasIndex(e => e.City, "city");

            entity.HasIndex(e => e.Country, "country");

            entity.HasIndex(e => e.Organization, "organization");

            entity.HasIndex(e => e.UserId, "user_index_2");

            entity.HasIndex(e => e.Name, "user_index_3");

            entity.HasIndex(e => e.Username, "user_index_4");

            entity.HasIndex(e => e.Email, "user_index_5");

            entity.Property(e => e.Address)
                .HasMaxLength(255)
                .HasComment("Residential Address")
                .HasColumnName("address");
            entity.Property(e => e.City).HasColumnName("city");
            entity.Property(e => e.Country).HasColumnName("country");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.Dob)
                .HasDefaultValueSql("now()")
                .HasColumnType("datetime")
                .HasColumnName("dob");
            entity.Property(e => e.Email).HasColumnName("email");
            entity.Property(e => e.Exprience).HasColumnName("exprience");
            entity.Property(e => e.LastSeen)
                .HasDefaultValueSql("now()")
                .HasColumnType("datetime")
                .HasColumnName("last_seen");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.NumberPosts).HasColumnName("number_posts");
            entity.Property(e => e.Organization).HasColumnName("organization");
            entity.Property(e => e.OrganizationJoinDate)
                .HasDefaultValueSql("now()")
                .HasColumnType("datetime")
                .HasColumnName("organization_join_date");
            entity.Property(e => e.PasswordHash)
                .HasMaxLength(255)
                .HasColumnName("password_hash");
            entity.Property(e => e.Salary)
                .HasComment("Ammount Of salary in LPA")
                .HasColumnName("salary");
            entity.Property(e => e.Username).HasColumnName("username");

            entity.HasOne(d => d.CityNavigation).WithMany(p => p.Users)
                .HasForeignKey(d => d.City)
                .HasConstraintName("user_ibfk_2");

            entity.HasOne(d => d.CountryNavigation).WithMany(p => p.Users)
                .HasForeignKey(d => d.Country)
                .HasConstraintName("user_ibfk_3");

            entity.HasOne(d => d.OrganizationNavigation).WithMany(p => p.Users)
                .HasForeignKey(d => d.Organization)
                .HasConstraintName("user_ibfk_1");
        });

        modelBuilder.Entity<UserPast>(entity =>
        {
            entity.HasKey(e => e.UserPastId).HasName("PRIMARY");

            entity.ToTable("user_past");

            entity.HasIndex(e => e.City, "city");

            entity.HasIndex(e => e.Country, "country");

            entity.HasIndex(e => e.Organization, "organization");

            entity.HasIndex(e => e.User, "user");

            entity.Property(e => e.City).HasColumnName("city");
            entity.Property(e => e.Country).HasColumnName("country");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.FromDate)
                .HasDefaultValueSql("now()")
                .HasColumnType("datetime")
                .HasColumnName("from_date");
            entity.Property(e => e.Organization).HasColumnName("organization");
            entity.Property(e => e.ToDate)
                .HasDefaultValueSql("now()")
                .HasColumnType("datetime")
                .HasColumnName("to_date");
            entity.Property(e => e.User).HasColumnName("user");

            entity.HasOne(d => d.CityNavigation).WithMany(p => p.UserPasts)
                .HasForeignKey(d => d.City)
                .HasConstraintName("user_past_ibfk_3");

            entity.HasOne(d => d.CountryNavigation).WithMany(p => p.UserPasts)
                .HasForeignKey(d => d.Country)
                .HasConstraintName("user_past_ibfk_4");

            entity.HasOne(d => d.OrganizationNavigation).WithMany(p => p.UserPasts)
                .HasForeignKey(d => d.Organization)
                .HasConstraintName("user_past_ibfk_2");

            entity.HasOne(d => d.UserNavigation).WithMany(p => p.UserPasts)
                .HasForeignKey(d => d.User)
                .HasConstraintName("user_past_ibfk_1");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
