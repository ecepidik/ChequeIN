using Microsoft.EntityFrameworkCore;

namespace ChequeIN
{
    /// <summary>
    /// This class handles the sqlite database
    /// </summary>
    public class DatabaseContext : DbContext
    {
        public DbSet<Models.FinancialOfficer> FinancialOfficers { get; set; }
        public DbSet<Models.FinancialAdministrator> FinancialAdministrators { get; set; }
        public DbSet<Models.ChequeReq> ChequeReqs { get; set; }
        public DbSet<Models.LedgerAccount> LedgerAccounts { get; set; }

        public DatabaseContext(DbContextOptions options)
            :base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder){
            modelBuilder.Entity<Models.UserProfile>().ToTable("UserProfiles");
        }
    }
}
