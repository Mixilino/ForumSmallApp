using ForumWebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace ForumWebApi.Data
{
    public class DataContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<PostCategory> PostCategories { get; set; }
        public DbSet<Vote> Votes { get; set; }
        public DbSet<Comment> Comments { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;
                    Database=ForumDb; Trusted_Connection=True;");
            //.LogTo(Console.WriteLine)
            //.EnableSensitiveDataLogging(true);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Define relationships
            modelBuilder.Entity<Post>()
                        .HasOne(p => p.User)
                        .WithMany(u => u.Posts)
                        .HasForeignKey(p => p.UserId)
                        .HasConstraintName("FK_Post_UserId")
                        .OnDelete(DeleteBehavior.ClientCascade);

            modelBuilder.Entity<User>()
                        .HasIndex(u => u.UserName)
                        .IsUnique();

            modelBuilder.Entity<Post>()
                        .HasMany(p => p.PostCategories)
                        .WithMany(p => p.Posts);

            modelBuilder.Entity<Comment>()
                        .HasOne(c => c.Post)
                        .WithMany(p => p.Comments)
                        .HasForeignKey(c => c.PostId);
            modelBuilder.Entity<Comment>()
                        .HasOne(c => c.User)
                        .WithMany(p => p.Comments)
                        .HasForeignKey(c => c.UserId);

            modelBuilder.Entity<Vote>()
                       .HasOne(c => c.Post)
                       .WithMany(p => p.Votes)
                       .HasForeignKey(c => c.PostId);
            modelBuilder.Entity<Vote>()
                        .HasOne(c => c.User)
                        .WithMany(p => p.Votes)
                        .HasForeignKey(c => c.UserId);

            // Unique columns
            //modelBuilder.Entity<PostCategory>()
            //.HasAlternateKey(pc => pc.CategoryName);

            //modelBuilder.Entity<User>()
            //.HasAlternateKey(pc => pc.UserName);

        }
    }
}
