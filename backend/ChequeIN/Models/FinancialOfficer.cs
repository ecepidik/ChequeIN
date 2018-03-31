using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ChequeIN.Models
{
    public class FinancialOfficer : UserProfile
    {
        private String name;
        private String organization;

        [Required]
        public String Name
        {
            get { return this.name; }
            set { this.name = value.Trim(); }
        }

        public String Organization
        {
            get { return this.organization; }
            set { this.organization = value.Trim(); }
        }

    }
}
