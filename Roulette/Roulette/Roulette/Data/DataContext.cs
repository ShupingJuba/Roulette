using Microsoft.EntityFrameworkCore;
using Roulette.Models;
using System.Threading;

namespace Roulette.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Bet> Bets { get; set; }
        public DbSet<SpinResult> SpinResults { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
        }
}
