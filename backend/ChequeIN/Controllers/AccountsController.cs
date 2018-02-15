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
        // GET api/accounts
        [HttpGet]
        public IActionResult Get()
        {
            var account = Program.getAccountsOfUserId(301);
            if (account == null)
                return StatusCode(500);
            return Ok(account);
        }
    }
}
