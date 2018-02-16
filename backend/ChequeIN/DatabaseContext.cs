using Microsoft.EntityFrameworkCore;

namespace ChequeIN
{
    /// <summary>
    /// This class handles the sqlite database
    /// </summary>
    public class DatabaseContext : DbContext
    {
        /// <summary>
        /// This property allows to manipoulate the video games table
        /// </summary>
        public DbSet<Models.FinancialOfficer> FinancialOfficers {get; set;}
        public DbSet<Models.FinancialAdministrator> FinancialAdministrators {get; set;}
        public DbSet<Models.ChequeReq> ChequeReqs {get; set;}
        public DbSet<Models.LedgerAccount> LedgerAccounts {get; set;}

        public DbSet<Models.AuthorizedAccountSet> AuthorizedAccountSet {get; set;}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            // Specify the path of the database here
            optionsBuilder.UseSqlite("Filename=./chequein_db.sqlite");
        }
    }
}
