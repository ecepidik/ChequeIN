using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChequeIN.Models
{
    public abstract class UserProfile
    {
        private String email;

        public UserProfile()
        {
            AuthorizedAccountGroups = new List<AccountType>();
            SubmittedChequeReqs = new List<ChequeReq>();
        }

        [Key]
        public int UserProfileID { get; set; }

        public string AuthenticationIdentifier { get; set; }

        [EmailAddress]
        public String Email {
            get { return this.email; }
            set { this.email = value.Trim(); }
        }

        [DisplayName("Authorized Account Groups")]
        [MinimumLength(1, ErrorMessage = "An account must have at least one authorized account group.")]
        public ICollection<AccountType> AuthorizedAccountGroups { get; set; }

        [DisplayName("Submitted Cheque Reqs")]
        [ForeignKey("UserProfileID")]
        public ICollection<ChequeReq> SubmittedChequeReqs { get; set; }

    }
}
