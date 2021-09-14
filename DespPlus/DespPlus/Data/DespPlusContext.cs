using DespPlus.Models;
using Microsoft.EntityFrameworkCore;

namespace DespPlus.Data
{
    public class DespPlusContext : DbContext
    {
        public DbSet<CashFlow> CashFlows { get; set; }
        public DespPlusContext()
        {
            SQLitePCL.Batteries_V2.Init();

            this.Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Filename={Constants.DatabasePath}");
        }
    }
}
