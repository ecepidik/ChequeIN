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
        public DbSet<Model.FinancialOfficer> FinancialOfficers {get; set;}
        public DbSet<Model.FinancialAdministrator> FinancialAdministrators {get; set;}
        public DbSet<Model.ChequeReq> ChequeReqs {get; set;}
        // public DbSet<Model.LedgerAccount> LedgerAccounts {get; set;}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            // Specify the path of the database here
            optionsBuilder.UseSqlite("Filename=./chequein_db.sqlite");
        }
    }
}
