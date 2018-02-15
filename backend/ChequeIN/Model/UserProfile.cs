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
        private String email;

        public long UserProfileID { get; set; }

        [Required]
        [EmailAddress]
        public String Email {
            get
            {
                return this.email;
            }
            set
            {
                this.email = value.Trim();
            }
        }

    }
}
