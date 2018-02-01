using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ChequeIN.Model
{
    public class ChequeReq
    {

        [Required]
        public long ID { get; set; }

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
        public String PayeeName { get; set; }

        [Required]
        public String Description { get; set; }

        [DisplayName("Financial Officer Approver")]
        public String ApprovedBy { get; set; }

        public List<ClarifyingQuestion> Questions { get; set; }

        [DisplayName("Mailing Address")]
        [Required]
        public MailingAddress MailingAddress { get; set; }

        // TODO: Need a custom DataAnnotation that ensures that the list is min length 1.
        [DisplayName("Supporting Documents")]
        [Required]
        public List<SupportingDocument> SupportingDocuments { get; set; }

        // TODO: Need a custom DataAnnotation that ensures that the list is min length 1.
        [DisplayName("Status History")]
        [Required]
        public List<Status> StatusHistory { get; set; }

        // TODO: Need a custom DataAnnotation that ensures that the list is max length 2 and min length 1.
        // Could instead switch this to 2 separate associations.
        [DisplayName("Submitters")]
        [Required]
        public List<UserProfile> Submitters { get; set; }

        [DisplayName("Ledger Account")]
        [Required]
        public LedgerAccount Account { get; set; }

    }
}
