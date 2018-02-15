using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ChequeIN.Model
{
    public abstract class AuthorizedAccountSet
    {
        // May need to add associations with the FinancialAdministrator and the FinancialOfficer classes. Need clarification from Kareem.

        [Key]
        public long ID { get; set; }
        
        [DisplayName("Parent Group")]
        [Required]
        public LedgerAccountGroup Parent { get; set; }

    }
}
