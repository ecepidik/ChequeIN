using System;
using System.Collections.Generic;
using System.Linq;
using ChequeIN.Models;

namespace ChequeIN.Database
{
    public static class DatabaseUtils
    {
        public static void AddToDatabase<T>(T obj) {
            using (var context = new DatabaseContext ()) {

                context.Database.EnsureCreated ();

                if (obj is Models.FinancialOfficer) {
                    context.FinancialOfficers.Add (obj as Models.FinancialOfficer);
                }
                else if (obj is Models.FinancialAdministrator) {
                    context.FinancialAdministrators.Add (obj as Models.FinancialAdministrator);
                }
                else if (obj is Models.LedgerAccount) {
                    context.LedgerAccounts.Add (obj as Models.LedgerAccount);
                }
                else if (obj is Models.ChequeReq) {
                    context.ChequeReqs.Add (obj as Models.ChequeReq);
                }
                context.SaveChanges();
            }
        }
    }
}
