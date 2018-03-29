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
            if (user == null) // TODO: This shouldn't have to be handled by individual api calls
                return Forbid();
            bool exists = Database.Accounts.TryGetAccountsOfUserId(_dbContext, user.AuthenticationIdentifier, out List<LedgerAccount> accounts); //TODO replace this id
            if (!exists)
                return Ok(accounts);
            return Ok(accounts);
        }

        // POST api/accounts
        [HttpPost]
        [Authorize]
        public IActionResult Post([FromBody] ChequeIN.Models.LedgerAccount account)
        {
            ChequeIN.Models.LedgerAccount a = new ChequeIN.Models.LedgerAccount()
            {
                Number = account.Number,
                Name = account.Name
            };

            Database.Accounts.StoreAccount(_dbContext, a);

            LedgerAccount savedAccount;
            bool exists = Database.Accounts.TryGetAccountByNumber(_dbContext, account.Number, out savedAccount);
            if (!exists)
                return NotFound("Account was not saved correctly.");

            return Ok(savedAccount);
        }
    }
}
