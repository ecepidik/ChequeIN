using System;
using System.Linq;
using ChequeIN.Models;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ChequeIN.Database
{
    public static class Accounts
    {
        public static bool TryGetAccountsOfUserId(string id, out List<AuthorizedAccountSet> accounts)
        {
            using (var context = new DatabaseContext())
            {

                context.Database.EnsureCreated();

                bool userExists = Users.TryGetUserById(id, out UserProfile user);

                if (!userExists)
                {
                    accounts = null;
                    return false;
                }

                long accountID;

                if (user is FinancialOfficer)
                {
                    accountID = (user as FinancialOfficer).AuthorizedAccounts;
                }
                else if (user is FinancialAdministrator)
                {
                    accountID = (user as FinancialAdministrator).RootAccount;
                }
                else
                {
                    accounts = null;
                    return false;
                }

                var groups = context.LedgerAccountGroups
                                .Include(ledgerAccountGroup => ledgerAccountGroup.Children)
                                .ToList();

                accounts = new List<AuthorizedAccountSet>();
                FindAllAuthorizedAccounts(groups, accounts, accountID);
                if (accounts.Any())
                {
                    return true;
                }
                return false;
            }
        }

        private static void FindAllAuthorizedAccounts(List<LedgerAccountGroup> groups, List<AuthorizedAccountSet> accounts, long accountID)
        {
           
            foreach (LedgerAccountGroup g in groups)
            {
                if (g.ID == accountID)
                {
                    accounts.Add(g);
                    foreach (AuthorizedAccountSet a in g.Children)
                    {
                        accounts.Add(a);
                        if(a.GetType() == typeof(LedgerAccountGroup)) {
                            FindAllAuthorizedAccounts(groups, accounts, a.ID);
                        }
                    }
                    break;
                }

            }
            return;
        }
    }
}
