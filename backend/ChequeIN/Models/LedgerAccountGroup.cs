using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ChequeIN.Models
{
    public class LedgerAccountGroup : AuthorizedAccountSet
    {
        private String groupName;

        [DisplayName("Group Name")]
        [Required]
        public String GroupName {
            get
            {
                return this.groupName;
            }
            set
            {
                this.groupName = value.Trim();
            }
        }

        [Required]
        public ICollection<AuthorizedAccountSet> Children { get; set; }

    }
}
