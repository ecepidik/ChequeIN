using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace ChequeIN.Models
{
    public class FinancialAdministrator : UserProfile
    {
        private String name;

        public String Name {
            get { return this.name; }
            set { this.name = value.Trim(); }
        }

    }
}
