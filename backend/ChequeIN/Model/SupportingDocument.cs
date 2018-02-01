using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ChequeIN.Model
{
    public class SupportingDocument
    {

        [DisplayName("Document Description")]
        [Required]
        public String Description { get; set; }

        [DisplayName("Document ID")]
        [Required]
        public long FileID { get; set; }

    }
}
