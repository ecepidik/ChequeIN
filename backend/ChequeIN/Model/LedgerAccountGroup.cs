using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ChequeIN.Model
{
    public class LedgerAccountGroup : AuthorizedAccountSet
    {

        [DisplayName("Group Name")]
        [Required]
        public String GroupName { get; set; }

        [Required]
        public List<AuthorizedAccountSet> Children { get; set; }

    }
}
