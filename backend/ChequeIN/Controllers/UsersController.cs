using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using ChequeIN;
using ChequeIN.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

namespace ChequeIN.Controllers
{
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        private DatabaseContext _dbContext;
        private Configurations.Authentication _authSettings;

        public UsersController(DatabaseContext dbContext, IOptions<Configurations.Authentication> authSettings){
            _authSettings = authSettings.Value;
            _dbContext = dbContext;
        }

        // GET /api/users
        [HttpGet]
        [Authorize]
        public IEnumerable<UserProfile> GetAll([FromQuery(Name = "userType")] string userType) {
            switch (userType)
            {
                case "officer":
                    return Database.Users.GetAllFinancialOfficers(_dbContext);
                case "administrator":
                    return Database.Users.GetAllFinancialAdministrators(_dbContext);
                default:
                    return Database.Users.GetAllUsers(_dbContext);
            }
        }

        // GET api/users/id
        [HttpGet("{id}")]
        [Authorize]
        public IActionResult Get(string id)
        {
            var exists = Database.Users.TryGetUserById(_dbContext, id, out UserProfile user);

            if (!exists) {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Post([FromBody] ChequeIN.Models.FinancialOfficer fo){
            Database.Users.StoreFinancialOfficer(_dbContext, fo);

            return Ok(fo);
        }
    }
}
