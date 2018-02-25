using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ChequeIN.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

namespace ChequeIN.Controllers
{
    [Route("api/[controller]")]
    public class AccountsController : Controller
    {
        private Configurations.Authentication _authSettings;

        public AccountsController(IOptions<Configurations.Authentication> authSettings)
        {
            _authSettings = authSettings.Value;
        }

        // GET api/accounts
        [HttpGet]
        [Authorize]
        public IActionResult Get()
        {
            var user = Database.Users.GetCurrentUser(User, _authSettings.DisableAuthentication);
            if (user == null) // TODO: This shouldn't have to be handled by individual api calls
                return StatusCode(404);
            bool exists = Database.Accounts.TryGetAccountsOfUserId(user.AuthenticationIdentifier, out List<LedgerAccount> accounts); //TODO replace this id
            if (!exists)
                return Ok(accounts);
            return Ok(accounts);
        }
    }
}
