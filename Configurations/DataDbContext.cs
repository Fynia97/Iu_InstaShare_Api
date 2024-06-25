using Iu_InstaShare_Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Iu_InstaShare_Api.Configurations
{
    //For Code-first, migrate models to db, use: dotnet ef migrations add NAME_OF_MIGRATION, check migration, then use dotnet ef database update
    public class DataDbContext : DbContext
    {
        public DataDbContext() { }

        public DataDbContext(DbContextOptions<DataDbContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=InstaShare;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FriendsModel>()
             .HasKey(f => f.Id);

            modelBuilder.Entity<FriendsModel>()
            .HasOne(f => f.User)
            .WithMany(u => u.Friends)
            .HasForeignKey(f => f.UserId)
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<FriendsModel>()
            .HasOne(f => f.Friend)
            .WithMany()
            .HasForeignKey(f => f.FriendId)
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<BookModel>()
            .HasOne(f => f.User)
            .WithMany()
            .HasForeignKey(f => f.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        }

        //For every model: public DbSet<ADDTYPE> ADDNAME { get; set; }

        public DbSet<BookModel> Books { get; set; }
        public DbSet<LendModel> Lends { get; set; }
        public DbSet<FriendsModel> Friends { get; set; }
        public DbSet<UserProfileModel> UserProfiles { get; set; }

    }
}
