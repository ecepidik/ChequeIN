using System;
using System.Collections.Generic;
using System.Linq;
using ChequeIN.Models;

namespace ChequeIN.Database
{
    public static class Seed
    {
        public static void SeedDatabase(DatabaseContext ctx)
        {
            if (!ctx.LedgerAccounts.Any())
            {
                GenerateLedgerAccounts(ctx);
            }

            if (!ctx.FinancialOfficers.Any())
            {
                 GenerateFinancialOfficer(ctx);
            }

            if (!ctx.FinancialAdministrators.Any())
            {
                GenerateFinancialAdministrator(ctx);
            }

            if (!ctx.ChequeReqs.Any())
            {
                int ledgerID = ctx.LedgerAccounts.ToList().ElementAt(0).LedgerAccountID;
                int userID = ctx.FinancialOfficers.ToList().ElementAt(0).UserProfileID;
                var chequereqs = GenerateChequeReq(ledgerID, userID);
                DatabaseUtils.AddToDatabase(ctx, chequereqs);
            }
        }

        private static ChequeReq GenerateChequeReq(int ledgerID, int userID)
        {
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
                SupportingDocuments = new List<SupportingDocument>() { new SupportingDocument() { Description = "blank", FileIdentifier = "none" } },
                StatusHistory = new List<Status>() { new Status() { } },
                LedgerAccountID = ledgerID,
                UserProfileID = userID
            };
            return c;
        }

        private static void GenerateLedgerAccounts(DatabaseContext context) {
            var accounts = new List<LedgerAccount>
            {
                new LedgerAccount()
                {
                    Name = "Common Room Exp",
                    Number = 5190,
                },
                new LedgerAccount()
                {
                    Name = "Lockers Exp",
                    Number = 5520,
                },
                new LedgerAccount()
                {
                    Name = "VP Services Exp",
                    Number = 5585,
                },
                new LedgerAccount()
                {
                    Name = "General Store Exp",
                    Number = 5750,
                },
                new LedgerAccount()
                {
                    Name = "General Store Supplies Exp",
                    Number = 5751,
                },
                new LedgerAccount()
                {
                    Name = "General Store Paraphernalia Exp",
                    Number = 5752,
                },
                new LedgerAccount()
                {
                    Name = "General Store Food Exp",
                    Number = 5753,
                },
                new LedgerAccount()
                {
                    Name = "General Store Wages Exp",
                    Number = 5755,
                },
                new LedgerAccount()
                {
                    Name = "Copi Eus Lease Exp",
                    Number = 5210,
                },
                new LedgerAccount()
                {
                    Name = "Copi Eus Supplies Exp",
                    Number = 5211,
                },
                new LedgerAccount()
                {
                    Name = "Copi Eus Wages Exp",
                    Number = 5212,
                },
                new LedgerAccount()
                {
                    Name = "Copi Eus Service Exp",
                    Number = 5213,
                },
                new LedgerAccount()
                {
                    Name = "Copi Eus Other Exp",
                    Number = 5215,
                },
                new LedgerAccount()
                {
                    Name = "Copi Eus Telecom Exp",
                    Number = 5216,
                },
                new LedgerAccount()
                {
                    Name = "Frostbite Exp",
                    Number = 5410,
                },
                new LedgerAccount()
                {
                    Name = "Frostbite Salaries Exp",
                    Number = 5411,
                },
                new LedgerAccount()
                {
                    Name = "Frostbite Ice Cream Exp",
                    Number = 5412,
                },
                new LedgerAccount()
                {
                    Name = "Frostbite Supplies Exp",
                    Number = 5413,
                },
                new LedgerAccount()
                {
                    Name = "Frostbite Rennovations Exp",
                    Number = 5414,
                },

                //FAKE ACCOUNT
                new LedgerAccount()
                {
                    Name = "Random Exp",
                    Number = 5666,
                }
            };

            //This was an attempt at getting the foreign key to not be null when LedgerAccounts held an AccountType object instead of just the enum.
            //Now that it's an enum, it looks a bit dumb, but I'm not changing it.
            accounts.ElementAt(0).Group = Enums.Group.SERVICES;
            accounts.ElementAt(1).Group = Enums.Group.SERVICES;
            accounts.ElementAt(2).Group = Enums.Group.SERVICES;
            accounts.ElementAt(3).Group = Enums.Group.GSTORE;
            accounts.ElementAt(4).Group = Enums.Group.GSTORE;
            accounts.ElementAt(5).Group = Enums.Group.GSTORE;
            accounts.ElementAt(6).Group = Enums.Group.GSTORE;
            accounts.ElementAt(7).Group = Enums.Group.GSTORE;
            accounts.ElementAt(8).Group = Enums.Group.COPIEUS;
            accounts.ElementAt(9).Group = Enums.Group.COPIEUS;
            accounts.ElementAt(10).Group = Enums.Group.COPIEUS;
            accounts.ElementAt(11).Group = Enums.Group.COPIEUS;
            accounts.ElementAt(12).Group = Enums.Group.COPIEUS;
            accounts.ElementAt(13).Group = Enums.Group.COPIEUS;
            accounts.ElementAt(14).Group = Enums.Group.FROSTBITE;
            accounts.ElementAt(15).Group = Enums.Group.FROSTBITE;
            accounts.ElementAt(16).Group = Enums.Group.FROSTBITE;
            accounts.ElementAt(17).Group = Enums.Group.FROSTBITE;
            accounts.ElementAt(18).Group = Enums.Group.FROSTBITE;
            accounts.ElementAt(19).Group = Enums.Group.RANDOM;

            foreach (LedgerAccount l in accounts)
            {
                context.LedgerAccounts.Add(l);
            }

            context.SaveChanges();
        }

        private static void GenerateFinancialOfficer(DatabaseContext context) {
            var profile = new FinancialOfficer()
            {
                Email = "user@test.com",
                AuthenticationIdentifier = "auth0|5a84eafef5c8213cb27c27e2",
            };

            profile.AuthorizedAccountGroups.Add(new AccountType()
            {
                Type = Enums.Group.SERVICES
            });
            profile.AuthorizedAccountGroups.Add(new AccountType()
            {
                Type = Enums.Group.COPIEUS
            });
            profile.AuthorizedAccountGroups.Add(new AccountType()
            {
                Type = Enums.Group.GSTORE
            });
            profile.AuthorizedAccountGroups.Add(new AccountType()
            {
                Type = Enums.Group.FROSTBITE
            });

            context.FinancialOfficers.Add(profile);
            context.SaveChanges();
        }

        private static void GenerateFinancialAdministrator(DatabaseContext context) {
            var profile = new Models.FinancialAdministrator() {
                Email = "mathieu@gmail.com",
                Name = "my name",
                AuthenticationIdentifier = new Random().Next(1000).ToString()
            };
            context.FinancialAdministrators.Add(profile);
        }

    }
}
