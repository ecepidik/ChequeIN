using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChequeIN.Models.API.Input
{
    public class ChequeReq
    {
        private String payeeName;
        private String description;
        private String approvedBy;

        [Key]
        public int ChequeReqID { get; set; }
        public Boolean FreeFood { get; set; }
        public Boolean OnlinePurchases { get; set; }
        public Boolean ToBeMailed { get; set; }
        public int LedgerAccountID { get; set; }

        [DisplayName("Pre-Tax Cost")]
        [StrictlyPositive(ErrorMessage = "Must be greater than 0")]
        public float PreTax { get; set; }

        [Positive(ErrorMessage = "Must be equal or greater than 0")]
        public float GST { get; set; }

        [Positive(ErrorMessage = "Must be equal or greater than 0")]
        public float PST { get; set; }

        [Positive(ErrorMessage = "Must be equal or greater than 0")]
        public float HST { get; set; }

        [DisplayName("Mailing Address")]
        public MailingAddress MailingAddress { get; set; }

        [DisplayName("Payee Name")]
        public String PayeeName {
            get { return this.payeeName;}
            set { this.payeeName = value.Trim(); }
        }

        public String Description {
            get { return this.description; }
            set { this.description = value.Trim(); }
        }

        [DisplayName("Approver Name")]
        public String ApprovedBy
        {
            get { return this.approvedBy; }
            set { this.approvedBy = value.Trim(); }
        }

    }
}
