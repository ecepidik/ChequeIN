using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ChequeIN.Models;


namespace ChequeIN.Controllers
{
    [Route("api/[controller]")]
    public class AccountsController : Controller
    {
        // GET api/accounts
        [HttpGet]
        public IActionResult Get()
        {
            bool exists = Database.Accounts.TryGetAccountsOfUserId("301", out AuthorizedAccountSet account); //TODO replace this id
            if (!exists)
                return StatusCode(404);
            return Ok(account);
        }
    }
}
