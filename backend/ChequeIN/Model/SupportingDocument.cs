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
        private String description;

        [DisplayName("Document Description")]
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

        [DisplayName("Document ID")]
        [Required]
        public long FileIdentifier { get; set; }

    }
}
