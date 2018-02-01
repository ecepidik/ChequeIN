using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChequeIN.Model
{
    public class Status
    {
        enum StatusType
        {
            SUBMITTED,
            APPROVED,
            REJECTED,
            PRINTED,
        };
    }
}
