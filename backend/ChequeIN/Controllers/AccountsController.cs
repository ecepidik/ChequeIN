using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ChequeIN.Models;
using Microsoft.AspNetCore.Authorization;

namespace ChequeIN.Controllers
{
    [Route("api/[controller]")]
    public class AccountsController : Controller
    {
        // GET api/accounts
        [HttpGet]
        [Authorize]
        public IActionResult Get()
        {
            var user = Database.Users.GetCurrentUser(User);
            if (user == null) // TODO: This shouldn't have to be handled by individual api calls
                return StatusCode(404);
            bool exists = Database.Accounts.TryGetAccountsOfUserId(user.UserProfileID, out List<AuthorizedAccountSet> account); //TODO replace this id
            if (!exists)
                return Ok(account);
            return Ok(account);
        }
    }
}
