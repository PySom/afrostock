using Microsoft.EntityFrameworkCore;
using AfrroStock.Models;

namespace AfrroStock.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) :
            base(options)
        {
        }

        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        //public DbSet<Category> Categories { get; set; }
        public DbSet<Collection> Collections { get; set; }
        public DbSet<CollectionType> CollectionTypes { get; set; }
        public DbSet<Collect> Collects { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<ImageTag> ImageTags { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<UserImage> UserImages { get; set; }
        public DbSet<ApplicationUser> Users { get; set; }
        public DbSet<UserSubscription> UserSubscriptions { get; set; }

    }
}
