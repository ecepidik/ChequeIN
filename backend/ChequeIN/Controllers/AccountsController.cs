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
        private DatabaseContext _dbContext;

        public AccountsController(IOptions<Configurations.Authentication> authSettings, DatabaseContext dbContext)
        {
            _authSettings = authSettings.Value;
            _dbContext = dbContext;
        }

        // GET api/accounts
        [HttpGet]
        [Authorize]
        public IActionResult Get()
        {
            var user = Database.Users.GetCurrentUser(_dbContext, User, _authSettings.DisableAuthentication, _authSettings.DevelopmentUserId);
            bool exists;
            List<LedgerAccount> accounts;
            if (!Database.Users.IsCurrentUserAdmin(_dbContext, User, _authSettings.DisableAuthentication, _authSettings.DevelopmentUserId)) // TODO: This shouldn't have to be handled by individual api calls
            {
                exists = Database.Accounts.TryGetAccountsOfUserId(_dbContext, user.AuthenticationIdentifier, out accounts);
                if (accounts == null) { accounts = new List<LedgerAccount>(); }
                return Ok(accounts);
            }
            exists = Database.Accounts.TryGetAllAccounts(_dbContext, out accounts);

            if (accounts == null) { accounts = new List<LedgerAccount>(); }
            return Ok(accounts);
        }
    }
}
