using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ChequeIN;
using ChequeIN.Model;


namespace ChequeIN.Controllers
{
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        // GET api/users
        [HttpGet]
        public IEnumerable<UserProfile> Get()
        {
            return Program.getAllUsers();
        }

        // GET api/users/id
        [HttpGet("{id}")]
        public UserProfile Get(long id)
        {
            return (UserProfile) Program.getFinancialOfficerFromId(id);
        }
    }
}