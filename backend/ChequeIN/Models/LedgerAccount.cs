using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ChequeIN.Models
{
    public class LedgerAccount
    {
        private String name;

        public LedgerAccount()
        {
            ChequeReqs = new List<ChequeReq>();
        }

        public int LedgerAccountID { get; set; }

        [DisplayName("Account Type")]
        [Required]
        public Enums.AccountType Type { get; set; }

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
        public ICollection<ChequeReq> ChequeReqs { get; private set; }

    }
}
