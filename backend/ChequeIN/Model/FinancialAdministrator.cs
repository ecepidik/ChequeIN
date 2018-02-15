using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace ChequeIN.Model
{
    public class FinancialAdministrator : UserProfile
    {
        private String name;

        [Required]
        public String Name {
            get
            {
                return this.name;
            }
            set
            {
                this.name = value.Trim();
            }
        }

        [Required]
        public long RootID { get; set; }

    }
}
