using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ChequeIN.Model;


namespace ChequeIN.Controllers
{
    [Route("api/[controller]")]
    public class AccountsController : Controller
    {
        LedgerAccount account = new LedgerAccount()
        {
            Name = "myAccount",
            Number = 60
        };

        // GET api/accounts
        [HttpGet]
        public AuthorizedAccountSet Get()
        {
            return account;
        }
    }
}
