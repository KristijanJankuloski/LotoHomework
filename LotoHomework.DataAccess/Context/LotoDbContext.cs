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
    }
}
