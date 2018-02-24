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

        [ForeignKey("AuthorizedAccountGroups")]
        public int? UserProfileID { get; set; }

        [ForeignKey("Group")]
        public int LedgerAccountID { get; set; }

        public Enums.Group Type { get; set; }

    }
}
