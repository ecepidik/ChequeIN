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
        FinancialOfficer mockOfficer = new FinancialOfficer()
        {
            UserProfileID = 45,
            Email = "chicken@st-hubert.ca"
        };

        FinancialAdministrator mockAdmin = new FinancialAdministrator()
        {
            UserProfileID = 12,
            Name = "Michel",
            Email = "michel@benny.ca"
        };

        List<UserProfile> users = new List<UserProfile>();

        // GET api/users
        [HttpGet]
        public IEnumerable<UserProfile> Get()
        {
            return Program.getAllFinancialOfficers().Select(x => (UserProfile)x);
        }

        // GET api/users/id
        [HttpGet("{id}")]
        public UserProfile Get(long id)
        {
            return (UserProfile) Program.getFinancialOfficerFromId(id);
        }
    }
}
