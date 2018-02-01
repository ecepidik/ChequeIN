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

        [Required]
        public String Name { get; set; }

        [Required]
        public AuthorizedAccountSet Root { get; set; }

    }
}
