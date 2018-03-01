using ChequeIN.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace ChequeIN.Tests
{
    public class DatabaseTests
    {
        
        private void SetupDatabase()
        {
            
            using (var context = new DatabaseContext() { IsTest = true })
            {
                // Clear the old database before each test run
                context.Database.EnsureDeleted();
                // Create the database if it does not exist
                context.Database.EnsureCreated();

                var ledger = new Models.LedgerAccount()
                {
                    Name = "General Expenses",
                    Number = 6530,
                    Group = Enums.Group.COPIEUS
                };

                var officer = new Models.FinancialOfficer()
                {
                    Email = "alex@hotmail.com",
                    AuthenticationIdentifier = "301" // TODO: use the real test user ID
                };

                var admin = new Models.FinancialAdministrator()
                {
                    Email = "mathieu@gmail.com",
                    Name = "my name",
                    AuthenticationIdentifier = new Random().Next(1000).ToString()
                };

                context.Add(ledger as LedgerAccount);
                context.Add(officer as FinancialOfficer);
                context.Add(admin as FinancialAdministrator);
                context.SaveChanges();
            }

            
        }

        [Fact]
        public void Database_addValidChequeReq()
        {
            SetupDatabase();
            DatabaseContext context = new DatabaseContext() { IsTest = true };

            var ledgerAccounts = context.LedgerAccounts
                                .ToList();

            var officers = context.FinancialOfficers
                                .ToList();


            ChequeReq c = new ChequeReq()
            {
                PreTax = 1,
                ChequeReqID = 1,
                GST = 1,
                PST = 1,
                HST = 1,
                PayeeName = "User",
                Description = "Desc",
                ApprovedBy = "Kareem Halabi",
                FreeFood = false,
                OnlinePurchases = false,
                ToBeMailed = true,
                MailingAddress = new MailingAddress() { Line1 = "1645 rue des rigoles", City = "Sherb", PostalCode = "J1M2H2" },
                SupportingDocuments = new List<SupportingDocument>() { new SupportingDocument() { Description = "blank" } },
                StatusHistory = new List<Status>() { new Status() {  } },
                LedgerAccountID = ledgerAccounts.ElementAt(0).LedgerAccountID,
                UserProfileID = officers.ElementAt(0).UserProfileID
            };

            context.Add(c as ChequeReq);
            context.SaveChanges();

            context = new DatabaseContext() { IsTest = true };

            var crs = context.ChequeReqs
                        .ToList();
            ledgerAccounts = context.LedgerAccounts
                                .ToList();
            officers = context.FinancialOfficers
                                .ToList();

            Assert.Equal(1, crs.Count);
            Assert.True(crs.ElementAt(0).LedgerAccountID == ledgerAccounts.ElementAt(0).LedgerAccountID);
            Assert.True(ledgerAccounts.ElementAt(0).ChequeReqs.ElementAt(0).ChequeReqID == crs.ElementAt(0).ChequeReqID);
            Assert.Equal(crs.ElementAt(0).UserProfileID, officers.ElementAt(0).UserProfileID);
            Assert.Equal(officers.ElementAt(0).SubmittedChequeReqs.ElementAt(0).ChequeReqID, crs.ElementAt(0).ChequeReqID);

        }

    }
}
