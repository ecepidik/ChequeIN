using System;
using System.Linq;
using ChequeIN.Models;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ChequeIN.Database
{
    public static class Accounts
    {
        public static bool TryGetAccountsOfUserId(DatabaseContext context, string id, out List<LedgerAccount> accounts)
        {

            bool userExists = Users.TryGetUserById(context, id, out UserProfile user);

            if (!userExists)
            {
                accounts = null;
                return false;
            }

            List<AccountType> accountTypes;

            if (user is UserProfile)
            {
                accountTypes = user.AuthorizedAccountGroups.ToList();
            }
            else
            {
                accounts = null;
                return false;
            }

            var ledgerAccounts = context.LedgerAccounts
                            .ToList();

            accounts = new List<LedgerAccount>();
            FindAllAuthorizedAccounts(ledgerAccounts, accounts, accountTypes);
            if (accounts.Any())
            {
                return true;
            }
            return false;
        }

        private static void FindAllAuthorizedAccounts(List<LedgerAccount> ledgerAccounts, List<LedgerAccount> accounts, List<AccountType> accountTypes)
        {
           
            foreach (AccountType t in accountTypes)
            {
                foreach(LedgerAccount a in ledgerAccounts)
                {
                    if(a.Group.Type == t.Type)
                    {
                        accounts.Add(a);
                    }
                }

            }
            return;
        }
    }
}
