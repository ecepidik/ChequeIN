using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ChequeIN.Models
{
    public class SupportingDocument
    {
        private String description;

        public int SupportingDocumentID { get; set; }

        [DisplayName("Document Description")]
        public String Description {
            get { return this.description; }
            set { this.description = value.Trim(); }
        }

        [DisplayName("Document ID")]
        public long FileIdentifier { get; set; }

    }
}
