using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ChequeIN.Models;

namespace ChequeIN.Database
{
    public static class ChequeReqs
    {
        public static IEnumerable<ChequeReq> GetAllChequeReqs(DatabaseContext context) {
            return (IEnumerable<ChequeReq>)context.ChequeReqs.ToList();
        }

        public static bool TryGetAllChequeReqs(DatabaseContext context, string id, out List<ChequeReq> cheques)
        {
            bool userExists = Users.TryGetUserById(context, id, out UserProfile user);

            if (!userExists) {
                cheques = null;
                return false;
            }

            if (user is Models.FinancialAdministrator)
            {
                try
                {
                    cheques = context.ChequeReqs
                                     .Include(x => x.StatusHistory)
                                     .Include(x => x.MailingAddress)
                                     .Include(x => x.SupportingDocuments)
                                     .ToList();
                    return true;
                }
                catch (Exception)
                {
                    cheques = null;
                    return false;
                }
            }
            else
            {
                try
                {
                    cheques = context.ChequeReqs
                                     .Where(x => x.UserProfileID == user.UserProfileID)
                                     .Include(x => x.StatusHistory)
                                     .Include(x => x.MailingAddress)
                                     .Include(x => x.SupportingDocuments)
                                     .ToList();
                    return true;
                }
                catch (Exception)
                {
                    cheques = null;
                    return false;
                }
            }

        }

        public static bool TryGetChequeReq(DatabaseContext context, int id, out ChequeReq cheque) {
            try {
                cheque = context.ChequeReqs
                               .Where(x => x.ChequeReqID == id)
                               .Include(x => x.StatusHistory)
                               .Include(x => x.MailingAddress)
                               .Include(x => x.SupportingDocuments)
                               .Single();
                return true;
            }
            catch (Exception) {
                cheque = null;
                return false;
            }
        }

        public static void UpdateChequeReq(DatabaseContext context, ChequeReq cheque)
        {
            context.Update(cheque as ChequeReq);
            context.SaveChanges();
        }

        public static void StoreChequeReq(DatabaseContext context, ChequeReq cheque) {
            context.Add(cheque as ChequeReq);
            context.SaveChanges();
        }
   }
}
