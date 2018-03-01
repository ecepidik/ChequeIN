using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ChequeIN.Models;

namespace ChequeIN.Database
{
    public static class ChequeReqs
    {
        public static IEnumerable<ChequeReq> GetAllChequeReqs()
        {
            using (var context = new DatabaseContext())
            {
                context.Database.EnsureCreated();

                return (IEnumerable<ChequeReq>)context.ChequeReqs.ToList();
            }
        }

        public static bool TryGetAllChequeReqs(string id, out List<ChequeReq> cheques)
        {
            using (var context = new DatabaseContext())
            {
                context.Database.EnsureCreated();

                bool userExists = Users.TryGetUserById(id, out UserProfile user);

                if (!userExists) {
                    cheques = null;
                    return false;
                }

                if (user is FinancialAdministrator)
                {
                    var userLoader = context.FinancialAdministrators.Include(u => u.SubmittedChequeReqs)
                                        .Single(u => u.UserProfileID == user.UserProfileID);
                    cheques = userLoader.SubmittedChequeReqs.ToList();
                }
                else
                {
                    var userLoader = context.FinancialOfficers.Include(u => u.SubmittedChequeReqs)
                                        .Single(u => u.UserProfileID == user.UserProfileID);
                    cheques = userLoader.SubmittedChequeReqs.ToList();
                }

                //var accounts = context.LedgerAccounts.Include(x => x).Single(x => x.Group.UserProfileID == user.UserProfileID);

                //foreach (ChequeReq c in cheques) {
                //    c.
                //}

                return true;
            }
        }
    }
}
