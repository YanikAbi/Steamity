using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Steamity.Models;


namespace Steamity.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<SteamityUser>? SteamityUsers { get; set; }

        public DbSet<Game>? Games { get; set; }

        public DbSet<BoughtGame>? BoughtGames { get; set; }

        public DbSet<Wishlist>? Wishlists { get; set; }

        public DbSet<Wishlist>? WishlistGames { get; set; }

        public DbSet<Image>? Images { get; set; }

        public DbSet<Transaction>? Transactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SteamityUser>()
                .HasOne(u => u.Wishlist)
                .WithOne(w => w.User)
                .HasForeignKey<Wishlist>(w => w.UserId);

            modelBuilder.Entity<WishlistGame>()
                .HasKey(wg => new { wg.WishlistId, wg.GameId });

            modelBuilder.Entity<WishlistGame>()
                .HasOne(wg => wg.Wishlist)
                .WithMany(w => w.WishlistGames)
                .HasForeignKey(wg => wg.WishlistId);

            modelBuilder.Entity<WishlistGame>()
                .HasOne(wg => wg.Game)
                .WithMany(g => g.WishlistGames)
                .HasForeignKey(wg => wg.GameId);

            base.OnModelCreating(modelBuilder);
        }
    }
}