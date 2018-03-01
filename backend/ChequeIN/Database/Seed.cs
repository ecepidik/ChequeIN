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
                context.Database.EnsureDeleted ();
                context.Database.EnsureCreated ();
            }

            DatabaseUtils.AddToDatabase(GenerateLedgerAccount());
            DatabaseUtils.AddToDatabase(GenerateFinancialOfficer());
            DatabaseUtils.AddToDatabase(GenerateFinancialAdministrator());

            using (var context = new DatabaseContext() { IsTest = false })
            {
                var ledgerAccounts = context.LedgerAccounts.ToList();
                var officers = context.FinancialOfficers.ToList();

                ChequeReq c = new ChequeReq() {
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
                    StatusHistory = new List<Status>() { new Status() { } },
                    LedgerAccountID = ledgerAccounts.ElementAt(0).LedgerAccountID,
                    UserProfileID = officers.ElementAt(0).UserProfileID
                };
                context.Add(c as ChequeReq);
                context.SaveChanges();
            }
        }

        private static LedgerAccount GenerateLedgerAccount() {
            var ledger = new Models.LedgerAccount() {
                Name = "General Expenses",
                Number = 6530,
                Group = new AccountType()
                {
                    Type = Enums.Group.COPIEUS
                },
            };
            return ledger;
        }

        private static FinancialOfficer GenerateFinancialOfficer() {
            var profile = new Models.FinancialOfficer() {
                Email = "user@test.com",
                AuthenticationIdentifier = "auth0|5a84eafef5c8213cb27c27e2"
            };
            return profile;
        }

        private static FinancialAdministrator GenerateFinancialAdministrator() {
            var profile = new Models.FinancialAdministrator() {
                Email = "mathieu@gmail.com",
                Name = "my name",
                AuthenticationIdentifier = new Random().Next(1000).ToString()
            };
            return profile;
        }

    }
}
