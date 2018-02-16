using System.Linq;
using ChequeIN.Models;

namespace ChequeIN.Database
{
    public static class Accounts
    {
        public static bool TryGetAccountsOfUserId(long id, out AuthorizedAccountSet account)
        {
            using (var context = new DatabaseContext())
            {

                context.Database.EnsureCreated();

                bool userExists = Users.TryGetUserById(id, out UserProfile user);

                if (!userExists)
                {
                    account = null;
                    return false;
                }

                long accountID;

                if (user is FinancialOfficer)
                {
                    accountID = (user as FinancialOfficer).AuthorizedAccountsID;
                }
                else if (user is FinancialAdministrator)
                {
                    accountID = (user as FinancialAdministrator).RootID;
                }
                else
                {
                    account = null;
                    return false;
                }

                var accounts = from v in context.AuthorizedAccountSet
                              where v.ID == accountID
                              select v;

                if (accounts.Any())
                {
                    account = accounts.First();
                    return true;
                }
                account = null;
                return false;
            }
        }
    }
}
