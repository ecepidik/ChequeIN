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
    public class ChequeReqsController : Controller
    {
        private Configurations.Authentication _authSettings;

        public ChequeReqsController(IOptions<Configurations.Authentication> authSettings)
        {
            _authSettings = authSettings.Value;
        }

        // GET api/ChequeReqs
        [HttpGet]
        public IActionResult Get()
        {
            var user = Database.Users.GetCurrentUser(User, _authSettings.DisableAuthentication, _authSettings.DevelopmentUserId);
            if (user == null)
            {
                return StatusCode(403);
            }
            bool exists = Database.ChequeReqs.TryGetAllChequeReqs(user.AuthenticationIdentifier, out List<ChequeReq> cheques);
            if (!exists)
            {
                return Ok(Enumerable.Empty<ChequeReq>().ToList<ChequeReq>());
            }
            else
            {
                //to avoid serializing circular references, set the submitter cheque req's list to null
                foreach (ChequeReq c in cheques)
                {
                    c.Submitter.Clear();
                }
                return Ok(cheques);
            }
        }

        [HttpPost]
        public IActionResult Create([FromBody] ChequeReq cheque)
        {
            return Ok(true);
        }
    }
}
