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

            //for testing purposes
            var profile = new Model.FinancialOfficer();
            profile.Email = "alex@hotmail.com";
            profile.UserProfileID = new Random().Next(1000);

            var profile2 = new Model.FinancialAdministrator();
            profile2.Email = "mathieu@gmail.com";
            profile2.Name = "my name";
            profile2.UserProfileID = new Random().Next(1000);
            addToDatabase(profile2);

            var ledger = new Model.LedgerAccount();
            ledger.ID = new Random().Next(1000);
            ledger.Name = "Bob";
            ledger.Number = 0;
            ledger.ChequeReqs = new List<Model.ChequeReq>();

            var chequeReq = new Model.ChequeReq();
            var chequeReqID = new Random().Next(1000);
            chequeReq.ChequeReqID = chequeReqID;
            chequeReq.Account = ledger;
            chequeReq.ApprovedBy = profile;
            chequeReq.Description = "This is the first chequre req ever !";
            chequeReq.GST = 5.1f;
            chequeReq.HST = 4.9f;
            chequeReq.MailingAddress = new Model.MailingAddress{ChequeReqID = chequeReqID, Line1 = "244", City = "Montreal", Province = Model.Enums.Province.QC, PostalCode = "J4P3A5"};
            chequeReq.PayeeName = "Mathieu";
            chequeReq.PreTax = 2.0f;
            chequeReq.PST = 6.7f;
            chequeReq.Questions = new List<Model.ClarifyingQuestion>();
            chequeReq.StatusHistory = new List<Model.Status>();
            chequeReq.Submitters = new List<Model.FinancialOfficer>();
            chequeReq.SupportingDocuments = new List<Model.SupportingDocument>();
            addToDatabase(chequeReq);

            BuildWebHost(args).Run();

        }

        public static void addToDatabase<T>(T obj)
        {

            using (var context = new DatabaseContext ()) {

                // Create the database if it does not exist
                context.Database.EnsureCreated ();

                if (obj is Model.FinancialOfficer)
                {
                    context.FinancialOfficers.Add (obj as Model.FinancialOfficer);
                }
                else if (obj is Model.FinancialAdministrator)
                {
                    context.FinancialAdministrators.Add (obj as Model.FinancialAdministrator);
                }
                else if (obj is Model.LedgerAccount)
                {
                    context.LedgerAccounts.Add (obj as Model.LedgerAccount);
                }
                else if (obj is Model.ChequeReq)
                {
                    context.ChequeReqs.Add (obj as Model.ChequeReq);
                }

                // Save changes to the database
                context.SaveChanges ();

            }

        }

        public static Model.UserProfile tryGetUserById(long id)
        {
            using (var context = new DatabaseContext()) {

                context.Database.EnsureCreated();
                var officers = from v in context.FinancialOfficers where v.UserProfileID == id
                select v;

                var admins = from v in context.FinancialAdministrators where v.UserProfileID == id
                select v;

                if (officers.Any())
                    return officers.First();
                else if (admins.Any())
                    return admins.First();
                else
                    return null;
            }
        }

        public static IEnumerable<Model.UserProfile> getAllUsers()
        {
            using (var context = new DatabaseContext ()) {

                context.Database.EnsureCreated();

                var officers = (IEnumerable<Model.UserProfile>)context.FinancialOfficers.ToList().Select(x => (Model.UserProfile) x);
                var admins = (IEnumerable<Model.UserProfile>)context.FinancialAdministrators.ToList().Select(x => (Model.UserProfile) x);

                return officers.Concat(admins);
            }
        }

        // public static void createChequeReq(long id, string payeeName)
        // {

        //     using (var context = new DatabaseContext ()) {

        //         // Create the database if it does not exist
        //         context.Database.EnsureCreated ();

        //         var chequeReq = new Model.ChequeReq();
        //         chequeReq.ChequeReqID = id;
        //         chequeReq.PayeeName = payeeName;
        //         context.ChequeReqs.Add (chequeReq);

        //         // Save changes to the database
        //         context.SaveChanges ();

        //     }

        // }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}
