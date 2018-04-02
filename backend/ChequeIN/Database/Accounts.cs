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

        public static bool TryGetAccountByNumber(DatabaseContext context, int number, out LedgerAccount account)
        {
            account = context.LedgerAccounts.Where(a => a.Number == number).FirstOrDefault();

            return account != null;
        }

        private static void FindAllAuthorizedAccounts(List<LedgerAccount> ledgerAccounts, List<LedgerAccount> accounts, List<AccountType> accountTypes)
        {
            foreach (AccountType t in accountTypes) {
                foreach(LedgerAccount a in ledgerAccounts) {
                    if(a.Type.ToLower().Equals(t.Type.ToLower())) {
                        accounts.Add(a);
                    }
                }
            }
            return;
        }

        public static int GetNewLedgerAccoundID(DatabaseContext context)
        {
            int maxID = context.LedgerAccounts.Max(a => a.LedgerAccountID as int?) ?? 0;

            return maxID + 1;
        }

        public static void StoreLedgerAccount(DatabaseContext context, LedgerAccount account)
        {
            context.Add(account as LedgerAccount);
            context.SaveChanges();
        }

        public static void StoreAccountType(DatabaseContext context, AccountType account)
        {
            context.Add(account as AccountType);
            context.SaveChanges();
        }
    }
}
