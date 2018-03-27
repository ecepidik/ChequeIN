using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ChequeIN.Models
{
    public class AccountType
    {
        [Key]
        public int AccountTypeID { get; set; }

        public int UserProfileID { get; set; }

        public String Type { get; set; }

    }
}
