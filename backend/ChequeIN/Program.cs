using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace ChequeIN
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // TODO: Clear the old database before each test run
            //for testing purposes
            var ledger = new Models.LedgerAccount();
            var ledgerAccountID = new Random().Next(1000);
            ledger.ID = ledgerAccountID;
            ledger.Name = "General Expenses";
            ledger.Number = 6530;
            ledger.ChequeReqs = new List<Models.ChequeReq>();
            addToDatabase(ledger);

            var profile = new Models.FinancialOfficer();
            profile.Email = "alex@hotmail.com";
            profile.UserProfileID = "301"; // TODO: use the real test user ID
            profile.AuthorizedAccountsID = ledgerAccountID;
            addToDatabase(profile);

            var profile2 = new Models.FinancialAdministrator();
            profile2.Email = "mathieu@gmail.com";
            profile2.Name = "my name";
            profile2.UserProfileID = new Random().Next(1000).ToString();
            profile2.RootID = ledgerAccountID;
            addToDatabase(profile2);

            var chequeReq = new Models.ChequeReq();
            var chequeReqID = new Random().Next(1000);
            chequeReq.ChequeReqID = chequeReqID;
            chequeReq.Account = ledger;
            chequeReq.ApprovedBy = profile;
            chequeReq.Description = "This is the first chequre req ever !";
            chequeReq.GST = 5.1f;
            chequeReq.HST = 4.9f;
            chequeReq.MailingAddress = new Models.MailingAddress{ChequeReqID = chequeReqID, Line1 = "244", City = "Montreal", Province = Models.Enums.Province.QC, PostalCode = "J4P3A5"};
            chequeReq.PayeeName = "Mathieu";
            chequeReq.PreTax = 2.0f;
            chequeReq.PST = 6.7f;
            chequeReq.Questions = new List<Models.ClarifyingQuestion>();
            chequeReq.StatusHistory = new List<Models.Status>();
            chequeReq.Submitters = new List<Models.FinancialOfficer>();
            chequeReq.SupportingDocuments = new List<Models.SupportingDocument>();
            // TO DO Add chequeReq in database without duplicating the Account and ApprovedBy (LedgerAccount)

            BuildWebHost(args).Run();

        }

        public static void addToDatabase<T>(T obj)
        {

            using (var context = new DatabaseContext ()) {

                // Create the database if it does not exist
                context.Database.EnsureCreated ();

                if (obj is Models.FinancialOfficer)
                {
                    context.FinancialOfficers.Add (obj as Models.FinancialOfficer);
                }
                else if (obj is Models.FinancialAdministrator)
                {
                    context.FinancialAdministrators.Add (obj as Models.FinancialAdministrator);
                }
                else if (obj is Models.LedgerAccount)
                {
                    context.LedgerAccounts.Add (obj as Models.LedgerAccount);
                }
                else if (obj is Models.ChequeReq)
                {
                    context.ChequeReqs.Add (obj as Models.ChequeReq);
                }

                // Save changes to the database
                context.SaveChanges();
            }
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}
