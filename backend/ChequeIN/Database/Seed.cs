using System;
using System.Collections.Generic;
using System.Linq;
using ChequeIN.Models;

namespace ChequeIN.Database
{
    public static class Seed
    {
        public static void SeedDatabase ()
        {

            using (var context = new DatabaseContext ()) {
                // Clear the old database before each test run
                context.Database.EnsureDeleted ();
                // Create the database if it does not exist
                context.Database.EnsureCreated ();
            }

            DatabaseUtils.AddToDatabase(GenerateLedgerAccount());

            DatabaseUtils.AddToDatabase(GenerateFinancialOfficer());

            DatabaseUtils.AddToDatabase(GenerateFinancialAdministrator());


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

        private static LedgerAccount GenerateLedgerAccount()
        {
            var ledger = new Models.LedgerAccount()
            {
                Name = "General Expenses",
                Number = 6530,
                Group = new AccountType()
                {
                    Type = Enums.Group.COPIEUS
                },
            };
            return ledger;
        }

        private static FinancialOfficer GenerateFinancialOfficer()
        {
            var profile = new Models.FinancialOfficer()
            {
                Email = "user@test.com",
                AuthenticationIdentifier = "auth0|5a84eafef5c8213cb27c27e2"
            };
            return profile;
        }

        private static FinancialAdministrator GenerateFinancialAdministrator()
        {
            var profile = new Models.FinancialAdministrator()
            {
                Email = "mathieu@gmail.com",
                Name = "my name",
                AuthenticationIdentifier = new Random().Next(1000).ToString()
            };
            return profile;
        }
        
    }
}
