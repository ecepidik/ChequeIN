using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChequeIN.Models
{
    public class LedgerAccount
    {
        private String name;

        public LedgerAccount()
        {
            ChequeReqs = new List<ChequeReq>();
        }

        [DisplayName("Account Number")]
        [Required]
        public int Number { get; set; }

        [DisplayName("Associated Cheque Reqs")]
        [ForeignKey("LedgerAccountID")]
        public ICollection<ChequeReq> ChequeReqs { get; private set; }

        [Key]
        public int LedgerAccountID { get; set; }

        [DisplayName("Account Group")]
        [Required]
        public Enums.Group Group { get; set; }

        [DisplayName("Account Name")]
        [Required]
        public String Name {
            get { return this.name; }
            set { this.name = value.Trim(); }
        }
    }
}
