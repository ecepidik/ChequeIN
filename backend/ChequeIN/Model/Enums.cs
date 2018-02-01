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
            NONE,
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
            QC,
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
