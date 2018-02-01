using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ChequeIN.Model
{
    public class ClarifyingQuestion
    {

        [Required]
        public String Question { get; set; }

        [DisplayName("Message Type")]
        [Required]
        public Boolean IsResponse { get; set; }

    }
}
