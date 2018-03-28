using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChequeIN.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ChequeIN.Controllers.Users
{
    [Route("api/users/{userId}/[controller]")]
    public class AccountsController : Controller
    {
        private DatabaseContext _dbContext;

        public AccountsController(DatabaseContext dbContext)
        {
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
    }
}
