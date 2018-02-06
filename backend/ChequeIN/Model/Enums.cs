using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChequeIN.Model
{
    public class Enums
    {
        public enum Province
        {
            QC,
            AB,
            BC,
            MB,
            NB,
            NL,
            NS,
            NT,
            NU,
            ON,
            PE,
            SK,
            YT,
        };

        public enum StatusType
        {
            SUBMITTED,
            APPROVED,
            REJECTED,
            PRINTED,
        };

    }
}
