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
    }
}
