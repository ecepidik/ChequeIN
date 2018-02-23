using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ChequeIN.Models
{
    public abstract class UserProfile
    {
        private String email;

        public UserProfile()
        {
            AuthorizedAccountTypes = new List<Enums.AccountType>();
        }

        [Key]
        public string UserProfileID { get; set; }

        public int ChequeReqID { get; set; }

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

        [DisplayName("Authorized Account Types")]
        [Required]
        [MinimumLengthAttribute(1, ErrorMessage = "An account must have at least one authorized account type.")]
        public ICollection<Enums.AccountType> AuthorizedAccountTypes { get; private set; }

    }
}
