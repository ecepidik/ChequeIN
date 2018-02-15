using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ChequeIN.Model
{
    public class FinancialOfficer : UserProfile
    {

        [DisplayName("Authorized Accounts")]
        [Required]
        public long AuthorizedAccountsID { get; set; }

    }
}
