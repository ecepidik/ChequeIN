using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ChequeIN.Models
{
    public class LedgerAccount : AuthorizedAccountSet
    {
        private String name;

        [DisplayName("Account Name")]
        [Required]
        public String Name {
            get
            {
                return this.name;
            }
            set
            {
                this.name = value.Trim();
            }
        }

        [DisplayName("Account Number")]
        [Required]
        public int Number { get; set; }

        [DisplayName("Associated Cheque Reqs")]
        [Required]
        public ICollection<ChequeReq> ChequeReqs { get; set; }

    }
}
