using Microsoft.EntityFrameworkCore;

namespace ChequeIN
{
    /// <summary>
    /// This class handles the sqlite database
    /// </summary>
    public class DatabaseContext : DbContext
    {
        public bool IsTest { get; set; }

        public DbSet<Models.FinancialOfficer> FinancialOfficers { get; set; }
        public DbSet<Models.FinancialAdministrator> FinancialAdministrators { get; set; }

        public DbSet<Models.ChequeReq> ChequeReqs { get; set; }

        public DbSet<Models.LedgerAccount> LedgerAccounts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Specify the path of the database here
            if (!IsTest)
            {
                optionsBuilder.UseSqlite("Filename=./chequein_db.sqlite");
            } else
            {
                optionsBuilder.UseSqlite("Filename=./chequein_test_db.sqlite");
            }

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Models.UserProfile>().ToTable("UserProfiles");
        }
    }
}
