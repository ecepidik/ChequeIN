using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ChequeIN.Model
{
    public class LedgerAccount : AuthorizedAccountSet
    {
        [DisplayName("Account Name")]
        [Required]
        public String Name { get; set; }

        [DisplayName("Account Number")]
        [Required]
        public int Number { get; set; }

        [DisplayName("Associated Cheque Reqs")]
        [Required]
        public List<ChequeReq> ChequeReqs { get; set; }

    }
}
