using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChequeIN.Models
{
    public class AccountType
    {

        public int AccountTypeID { get; set; }

        public Enums.Group Type { get; set; }

    }
}
