using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ChequeIN.Model
{
    public class ChequeReq
    {
        private String payeeName;
        private String description;
        private LedgerAccount account;

        public long ChequeReqID { get; set; }

        [DisplayName("Pre-Tax Cost")]
        [Required]
        public float PreTax { get; set; }

        [Required]
        public float GST { get; set; }

        [Required]
        public float PST { get; set; }

        public float HST { get; set; }

        [DisplayName("Payee Name")]
        [Required]
        public String PayeeName {
            get
            {
                return this.payeeName;
            }
            set
            {
                this.payeeName = value.Trim();
            }
        }

        [Required]
        public String Description {
            get
            {
                return this.description;
            }
            set
            {
                this.description = value.Trim();
            }
        }

        [DisplayName("Financial Officer Approver")]
        public FinancialOfficer ApprovedBy { get; set; }

        public ICollection<ClarifyingQuestion> Questions { get; set; }

        [DisplayName("Mailing Address")]
        [Required]
        public MailingAddress MailingAddress { get; set; }

        // TODO: Need a custom DataAnnotation that ensures that the list is min length 1.
        [DisplayName("Supporting Documents")]
        [Required]
        public ICollection<SupportingDocument> SupportingDocuments { get; set; }

        // TODO: Need a custom DataAnnotation that ensures that the list is min length 1.
        [DisplayName("Status History")]
        [Required]
        public ICollection<Status> StatusHistory { get; set; }

        // TODO: Need a custom DataAnnotation that ensures that the list is max length 2 and min length 1.
        // Could instead switch this to 2 separate associations.
        [DisplayName("Submitters")]
        [Required]
        public ICollection<UserProfile> Submitters { get; set; }

        [DisplayName("Ledger Account")]
        [Required]
        public LedgerAccount Account
        {
            get
            {
                return this.account;
            }

            set
            {
                this.account = value;
                if(!value.ChequeReqs.Contains(this))
                {
                    value.ChequeReqs.Add(this);
                }
            }

        }

    }
}
