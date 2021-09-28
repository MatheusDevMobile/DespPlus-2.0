using DespPlus.Models;
using Microsoft.EntityFrameworkCore;

namespace DespPlus.Data
{
    public class DespPlusContext : DbContext
    {
        public DbSet<CashFlow> CashFlows { get; set; }
        public DbSet<PaymentMethod> PaymentMethods { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DespPlusContext()
        {
            SQLitePCL.Batteries_V2.Init();

            this.Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Filename={Constants.DatabasePath}");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CashFlow>().Ignore(c => c.LabelColor);
            modelBuilder.Entity<CashFlow>().Ignore(c => c.Icon);
        }
    }
}
