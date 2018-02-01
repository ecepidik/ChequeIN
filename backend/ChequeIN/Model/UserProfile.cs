using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ChequeIN.Model
{
    public abstract class UserProfile
    {

        [Required]
        // TODO: Add a RegularExpression specifying acceptable email formatting.
        public String Email { get; set; }

    }
}
