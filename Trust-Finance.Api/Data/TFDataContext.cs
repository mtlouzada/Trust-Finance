using TF.Models;
using Microsoft.EntityFrameworkCore;
using TF.Data.Mappings;


namespace TF.Data
{
    public class TFDataContext : DbContext
    {
        public TFDataContext(DbContextOptions<TFDataContext> options)
        : base(options) { }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CategoryMap());
            modelBuilder.ApplyConfiguration(new TransactionMap());
            modelBuilder.ApplyConfiguration(new UserMap());

            base.OnModelCreating(modelBuilder);
        }
    }
}