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

        public static bool TryGetAccountById(DatabaseContext context, int id, out LedgerAccount account)
        {
            account = context.LedgerAccounts.Where(a => a.LedgerAccountID == id).FirstOrDefault();

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

        public static bool TryGetAllAccounts(DatabaseContext context, out List<LedgerAccount> accounts)
        {
            accounts = context.LedgerAccounts.ToList();
            return accounts.Count() > 0;
        }

        public static bool TryAuthorizeAccountForUser(DatabaseContext context, UserProfile user, LedgerAccount account)
        {
            context.Attach(user);
            context.Attach(account);
            foreach (AccountType a in user.AuthorizedAccountGroups)
            {
                if (a.Type.Equals(account.Type))
                {
                    return false;
                }
            }
            user.AuthorizedAccountGroups.Add(new AccountType() { Type = account.Type });
            context.SaveChanges();
            return true;
        }

        public static bool TryUnauthorizeAccountForUser(DatabaseContext context, UserProfile user, LedgerAccount account)
        {
            context.Attach(user);
            context.Attach(account);
            foreach (AccountType a in user.AuthorizedAccountGroups)
            {
                if (a.Type.Equals(account.Type))
                {
                    user.AuthorizedAccountGroups.Remove(a);
                    context.SaveChanges();
                    return true;
                }
            }
            return false;
        }
    }
}
