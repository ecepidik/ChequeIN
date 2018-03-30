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
        public IActionResult Post([FromBody] ChequeIN.Models.API.Input.LedgerAccount account)
        {
            var user = Database.Users.GetCurrentUser(_dbContext, User, _authSettings.DisableAuthentication, _authSettings.DevelopmentUserId);
            if (user == null)
                return Forbid();
            
            bool alreadyExists = Database.Accounts.TryGetAccountByNumber(_dbContext, account.Number, out LedgerAccount existingAccount);
            if (alreadyExists)
                return BadRequest("This account number already exists");


            String accountType = Database.Accounts.GetNewLedgerAccoundID(_dbContext).ToString();

            ChequeIN.Models.LedgerAccount a = new ChequeIN.Models.LedgerAccount()
            {
                Number = account.Number,
                Name = account.Name,
                Type = accountType
            };

            ChequeIN.Models.AccountType t = new ChequeIN.Models.AccountType()
            {
                UserProfileID = user.UserProfileID,
                Type = accountType
            };

            if (Database.AccountTypeValidation.TypeExists(_dbContext, accountType))
                return BadRequest("This account type already exists");

            Database.Accounts.StoreLedgerAccount(_dbContext, a);
            Database.Accounts.StoreAccountType(_dbContext, t);

            user.AuthorizedAccountGroups.Add(t);
            _dbContext.SaveChanges();

            bool exists = Database.Accounts.TryGetAccountByNumber(_dbContext, account.Number, out LedgerAccount savedAccount);
            if (!exists)
                return NotFound("The ledger account was not saved correctly.");

            return Ok(savedAccount);
        }
    }
}
