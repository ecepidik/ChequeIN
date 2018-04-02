using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChequeIN.Models.API.Input
{
    public class LedgerAccount
    {
        private String name;

        [DisplayName("Account Number")]
        [Required]
        public int Number { get; set; }

        [Key]
        public int LedgerAccountID { get; set; }

        [DisplayName("Account Name")]
        [Required]
        public String Name
        {
            get { return this.name; }
            set { this.name = value.Trim(); }
        }
    }
}
