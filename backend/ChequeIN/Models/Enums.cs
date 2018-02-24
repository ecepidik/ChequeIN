using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChequeIN.Models
{
    public class Enums
    {
        public enum UserType { Admin, Officer };

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
            QC,
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

        public enum Group
        {
            GSTORE,
            COPIEUS,
            FROSTBITE,
        };
    }
}
