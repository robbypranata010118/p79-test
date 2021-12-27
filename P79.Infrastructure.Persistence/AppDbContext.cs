using Microsoft.EntityFrameworkCore;
using P79.Base.Dtos.Transactions;
using P79.Domain.Entities;
using System.Linq;

namespace P79.Infrastructure.Persistence
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {
            Database.SetCommandTimeout(300);

        }
        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<AccountPoint> AccountPoints { get; set; }
        public virtual DbSet<Transaction> Transactions { get; set; }

        public virtual DbSet<TransactionByAccountIdResponse> TransactionByAccountIds { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<TransactionByAccountIdResponse>().HasNoKey();
        }
    }
}
