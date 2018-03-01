using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using ChequeIN;
using ChequeIN.Models;
using Microsoft.AspNetCore.Authorization;

namespace ChequeIN.Controllers
{
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        // GET api/users
        [HttpGet]
        [Authorize]
        public IEnumerable<UserProfile> Get()
        {
            return Database.Users.GetAllUsers();
        }

        // GET api/users/id
        [HttpGet("{id}")]
        [Authorize]
        public IActionResult Get(string id)
        {
            var exists = Database.Users.TryGetUserById(id, out UserProfile user);

            if (!exists) {
                return StatusCode(404);
            }
            return Ok(user);
        }
    }
}
