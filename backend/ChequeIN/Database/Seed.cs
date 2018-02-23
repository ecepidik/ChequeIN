using System;
using System.Collections.Generic;
using System.Linq;
using ChequeIN.Models;

namespace ChequeIN.Database
{
    public static class Seed
    {
        public static void seedDatabase ()
        {

            using (var context = new DatabaseContext ()) {
                // Clear the old database before each test run
                context.Database.EnsureDeleted ();
                // Create the database if it does not exist
                context.Database.EnsureCreated ();
            }
            
            DatabaseUtils.addToDatabase(generateLedgerAccount());

            DatabaseUtils.addToDatabase(generateFinancialOfficer());

            DatabaseUtils.addToDatabase(generateFinancialAdministrator());


            // var chequeReq = new Models.ChequeReq();
            // var chequeReqID = new Random().Next(1000);
            // chequeReq.ChequeReqID = chequeReqID;
            // chequeReq.Account = ledger;
            // chequeReq.ApprovedBy = profile;
            // chequeReq.Description = "This is the first chequre req ever !";
            // chequeReq.GST = 5.1f;
            // chequeReq.HST = 4.9f;
            // chequeReq.MailingAddress = new Models.MailingAddress{ChequeReqID = chequeReqID, Line1 = "244", City = "Montreal", Province = Models.Enums.Province.QC, PostalCode = "J4P3A5"};
            // chequeReq.PayeeName = "Mathieu";
            // chequeReq.PreTax = 2.0f;
            // chequeReq.PST = 6.7f;
            // chequeReq.Questions = new List<Models.ClarifyingQuestion>();
            // chequeReq.StatusHistory = new List<Models.Status>();
            // chequeReq.Submitters = new List<Models.FinancialOfficer>();
            // chequeReq.SupportingDocuments = new List<Models.SupportingDocument>();
            // TO DO Add chequeReq in database without duplicating the Account and ApprovedBy (LedgerAccount)

        }

        private static LedgerAccount generateLedgerAccount()
        {
            var ledger = new Models.LedgerAccount();
            var ledgerAccountID = new Random().Next(1000);
            ledger.LedgerAccountID = ledgerAccountID;
            ledger.Name = "General Expenses";
            ledger.Number = 6530;

            return ledger;
        }

        private static FinancialOfficer generateFinancialOfficer()
        {
            var profile = new Models.FinancialOfficer();
            profile.Email = "alex@hotmail.com";
            profile.UserProfileID = "301"; // TODO: use the real test user ID
            
            return profile;
        }

        private static FinancialAdministrator generateFinancialAdministrator()
        {
            var profile = new Models.FinancialAdministrator();
            profile.Email = "mathieu@gmail.com";
            profile.Name = "my name";
            profile.UserProfileID = new Random().Next(1000).ToString();
            
            return profile;
        }
        
    }
}
