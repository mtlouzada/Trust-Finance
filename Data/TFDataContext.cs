using TF.Models;
using Microsoft.EntityFrameworkCore;

namespace TF.Data
{
    public class TFDataContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options) => options.UseSqlServer("Server=localhost;Database=Finance;User Id=sa;Password=1q2w3e4r@#$");        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserMap());
            modelBuilder.ApplyConfiguration(new CategoryMap());
            modelBuilder.ApplyConfiguration(new TransactionMap());
        }
    }
}