using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ChequeIN.Models
{
    public class ClarifyingQuestion
    {
        private String question;

        [Key]
        public long ChequeReqID { get; set; }

        [Required]
        public String Question {
            get
            {
                return this.question;
            }
            set
            {
                this.question = value.Trim();
            }
        }

        [DisplayName("Message Type")]
        [Required]
        public Boolean IsResponse { get; set; }

    }
}
