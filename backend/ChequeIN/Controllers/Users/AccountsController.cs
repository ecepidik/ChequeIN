using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChequeIN.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ChequeIN.Controllers.Users
{
    [Route("api/users/{userId}/[controller]")]
    public class AccountsController : Controller
    {
        private DatabaseContext _dbContext;
        private Configurations.Authentication _authSettings;

        public AccountsController(DatabaseContext dbContext, IOptions<Configurations.Authentication> authSettings)
        {
            _authSettings = authSettings.Value;
            _dbContext = dbContext;
        }

        // GET: api/values
        [HttpGet]
        public ActionResult Get(string userId)
        {
            if (!Database.Accounts.TryGetAccountsOfUserId(_dbContext, userId, out List<LedgerAccount> accounts)) {
                return BadRequest("Cannot find the accounts");
            }
            return Ok(accounts);
        }

        // POST: api/users/{userId}/accounts/{ledgerAccountId}
        [HttpPost("{ledgerAccountId}")]
        [Authorize]
        public ActionResult Post(string userId, int ledgerAccountId)
        {
            if(!Database.Users.IsCurrentUserAdmin(_dbContext, User, _authSettings.DisableAuthentication, _authSettings.DevelopmentUserId))
            {
                return Forbid();
            } else if(!Database.Users.TryGetUserById(_dbContext, userId, out UserProfile user))
            {
                return BadRequest("Unable to find User");
            } else if(!Database.Accounts.TryGetAccountById(_dbContext, ledgerAccountId, out LedgerAccount account))
            {
                return BadRequest("Unable to find Account");
            } else if(!Database.Accounts.TryAuthorizeAccountForUser(_dbContext, user, account))
            {
                return BadRequest("Unable to authorize an account that is already authorized");
            }
            return Ok();
        }

        // DELETE: api/users/{userId}/accounts/{ledgerAccountId}
        [HttpDelete("{ledgerAccountId}")]
        [Authorize]
        public ActionResult Delete(string userId, int ledgerAccountId)
        {
            if (!Database.Users.IsCurrentUserAdmin(_dbContext, User, _authSettings.DisableAuthentication, _authSettings.DevelopmentUserId))
            {
                return Forbid();
            }
            else if (!Database.Users.TryGetUserById(_dbContext, userId, out UserProfile user))
            {
                return BadRequest("Unable to find User");
            }
            else if (!Database.Accounts.TryGetAccountById(_dbContext, ledgerAccountId, out LedgerAccount account))
            {
                return BadRequest("Unable to find Account");
            }
            else if(!Database.Accounts.TryUnauthorizeAccountForUser(_dbContext, user, account))
            {
                return BadRequest("Can't unauthorize an account that was never authorized");
            }
            return Ok();
        }

    }
}
