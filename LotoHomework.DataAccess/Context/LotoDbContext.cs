using LotoHomework.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace LotoHomework.DataAccess.Context
{
    public class LotoDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Combination> Combinations { get; set; }
        public DbSet<Session> Sessions { get; set; }

        public LotoDbContext(DbContextOptions options) : base(options) {}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Session>()
                .HasMany(x => x.NumberMatches)
                .WithOne(x => x.Session)
                .HasForeignKey(x => x.SessionId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Combination>()
                .HasMany(x => x.NumberMatches)
                .WithOne(x => x.Combination)
                .HasForeignKey(x => x.CombinationId);
        }
    }
}
