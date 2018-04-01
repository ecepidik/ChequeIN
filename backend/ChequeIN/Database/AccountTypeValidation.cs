using ChequeIN.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChequeIN.Database
{
    public class AccountTypeValidation
    {
        public static bool TypeExists(DatabaseContext ctx, String type)
        {
            var validStrings = ctx.ValidTypes.ToList();

            foreach (ValidType t in validStrings)
            {
                if (t.Type.ToLower().Equals(type.ToLower()))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
