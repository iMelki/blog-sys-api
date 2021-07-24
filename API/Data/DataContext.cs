using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<AppUser> Users { get; set; }
        public DbSet<AppPost> Posts { get; set; }
        public DbSet<Follow> Follows { get; set; }
        /*
        public DbSet<UserGroup> UserGroups { get; set; }
        public DbSet<GroupMessage> GroupMessages { get; set; }
        public DbSet<Message> PrivateMessages { get; set; }
        */
        public DbSet<Like> Likes { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            
            //////////

            builder.Entity<Like>()
                .HasKey(like => new{like.LikesUserId, like.LikedPostId});

            builder.Entity<Like>()
                .HasOne(like => like.LikesUser)
                .WithMany(user => user.LikedPosts)
                .HasForeignKey(like => like.LikesUserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Like>()
                .HasOne(like => like.LikedPost)
                .WithMany(post => post.LikedByUsers)
                .HasForeignKey(like => like.LikedPostId)
                .OnDelete(DeleteBehavior.Cascade);

            /////////

            builder.Entity<Follow>()
                .HasKey(follow => new{follow.FollowerUserId, follow.FollowedUserId});

            builder.Entity<Follow>()
                .HasOne(follow => follow.FollowerUser)
                .WithMany(user => user.FollowsUsers)
                .HasForeignKey(follow => follow.FollowerUserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Follow>()
                .HasOne(follow => follow.FollowedUser)
                .WithMany(user => user.FollowedByUsers)
                .HasForeignKey(follow => follow.FollowedUserId)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}