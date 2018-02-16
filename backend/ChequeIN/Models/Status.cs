using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ChequeIN.Models
{
    public class Status
    {

        private String feedback;

        [Key]
        public long ChequeReqID { get; set; }

        [DisplayName("Date of Status")]
        [Required]
        public DateTime StatusDate { get; set; }

        [Required]
        public String Feedback {
            get
            {
                return this.feedback;
            }
            set
            {
                this.feedback = value.Trim();
            }
        }

        [DisplayName("Status")]
        [Required]
        public Enums.StatusType SelectedStatus { get; set; }

        [DisplayName("Administrator Approver")]
        public FinancialAdministrator AdminApprover { get; set; }

    }
}
