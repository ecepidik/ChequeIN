using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ChequeIN.Models;

namespace ChequeIN.Database
{
    public static class ChequeReqs
    {
        public static IEnumerable<ChequeReq> GetAllChequeReqs() {
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

                try {
                  cheques = context.ChequeReqs
                                   .Where(x => x.UserProfileID == user.UserProfileID)
                                   .Include(x => x.StatusHistory)
                                   .Include(x => x.MailingAddress)
                                   .Include(x => x.SupportingDocuments)
                                   .ToList();
                  return true;
                }
                catch (Exception e) {
                   cheques = null;
                   return false;
                }
            }
        }

        public static bool TryGetChequeReq(int id, out ChequeReq cheque) {
            using (var context = new DatabaseContext())
            {
              context.Database.EnsureCreated();
              try {
                cheque = context.ChequeReqs
                               .Where(x => x.ChequeReqID == id)
                               .Include(x => x.StatusHistory)
                               .Include(x => x.MailingAddress)
                               .Include(x => x.SupportingDocuments)
                               .Single();
                return true;
              }
              catch (Exception e) {
                 cheque = null;
                 return false;
              }

            }
        }

        public static bool TryUpdateChequeReq(ChequeReq cheque) {
            using (var context = new DatabaseContext())
            {
                context.Database.EnsureCreated();

                var exist = TryGetChequeReq(cheque.ChequeReqID, out ChequeReq old);
                if (!exist)
                return false;

                context.ChequeReqs.Attach(cheque);
                context.SaveChanges();
                return true;
            }
        }

        public static void StoreChequeReq(ChequeReq cheque) {
            using (var context = new DatabaseContext())
            {
                context.Database.EnsureCreated();

                context.Add(cheque as ChequeReq);
                context.SaveChanges();
            }
        }
   }
}
