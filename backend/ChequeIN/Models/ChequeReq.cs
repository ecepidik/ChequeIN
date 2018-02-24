using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChequeIN.Models
{
    public class ChequeReq
    {
        private String payeeName;
        private String description;
        private String approvedBy;

        [Key]
        public int ChequeReqID { get; set; }

        [Required]
        public Boolean FreeFood { get; set; }

        [Required]
        public Boolean OnlinePurchases { get; set; }

        [Required]
        public Boolean ToBeMailed { get; set; }

        [DisplayName("Pre-Tax Cost")]
        [Required]
        [StrictlyPositive(ErrorMessage = "Must be greater than 0")]
        public float PreTax { get; set; }

        [Positive(ErrorMessage = "Must be equal or greater than 0")]
        public float GST { get; set; }

        [Positive(ErrorMessage = "Must be equal or greater than 0")]
        public float PST { get; set; }

        [Positive(ErrorMessage = "Must be equal or greater than 0")]
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

        [DisplayName("Approver Name")]
        public String ApprovedBy
        {
            get
            {
                return this.approvedBy;
            }
            set
            {
                this.approvedBy = value.Trim();
            }
        }

        [DisplayName("Mailing Address")]
        public MailingAddress MailingAddress { get; set; }

        [DisplayName("Supporting Documents")]
        [Required]
        [MinimumLength(1, ErrorMessage = "There must be at least one supporting document.")]
        public ICollection<SupportingDocument> SupportingDocuments { get; set; }

        [DisplayName("Status History")]
        [Required]
        [MinimumLength(1, ErrorMessage = "There must be at least one status in a ChequeReq's history.")]
        public ICollection<Status> StatusHistory { get; set; }

        [ForeignKey("SubmittedChequeReqs")]
        public int UserProfileID { get; set; }

        [DisplayName("Submitter")]
        [Required]
        [InverseProperty("SubmittedChequeReqs")]
        public UserProfile Submitter { get; set; }

        [ForeignKey("ChequeReqs")]
        public int LedgerAccountID { get; set; }

        [DisplayName("Associated Account")]
        [InverseProperty("ChequeReqs")]
        public LedgerAccount AssociatedAccount { get; set; }

    }
}
