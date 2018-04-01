using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ChequeIN.Models
{
    public class ValidType
    {
        private String type;

        [Key]
        public int ValidTypeID { get; set; }

        public String Type
        {
            get
            {
                return this.type;
            }
            set
            {
                this.type = value.Trim();
            }
        }

    }
}
