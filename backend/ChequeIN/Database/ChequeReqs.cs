using System;
using System.Collections.Generic;
using System.Linq;
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

                cheques = user.SubmittedChequeReqs.ToList<ChequeReq>();
                return true;
            }
        }
    }
}
