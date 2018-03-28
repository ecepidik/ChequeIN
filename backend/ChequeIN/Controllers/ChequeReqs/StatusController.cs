using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChequeIN.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ChequeIN.Controllers.ChequeReqs
{
    [Route("api/chequereqs/{chequeId}/[controller]")]
    public class StatusController : Controller
    {
        private DatabaseContext _dbContext;
        private Configurations.Authentication _authSettings;

        public StatusController(DatabaseContext dbContext, IOptions<Configurations.Authentication> authSettings)
        {
            _dbContext = dbContext;
            _authSettings = authSettings;
        }

        [HttpGet]
        [Authorize]
        public IActionResult Get(int chequeId)
        {
            if(!Database.ChequeReqs.TryGetChequeReq(_dbContext, chequeId, out ChequeReq cheque))
            {
                return NotFound("The specified chequeReq does not exist");
            }

            return Ok(cheque.StatusHistory);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Post(int chequeId, [FromBody]Status status)
        {
            if (!Database.Users.IsCurrentUserAdmin(_dbContext, User, _authSettings.DisableAuthentication, _authSettings.DevelopmentUserId))
            {
                return BadRequest("You are not an admin");
            }

            if (!Database.ChequeReqs.TryGetChequeReq(_dbContext, chequeId, out ChequeReq cheque))
            {
                return NotFound("The specified chequeReq does not exist");
            }

            status.StatusDate = DateTime.UtcNow;

            cheque.StatusHistory.Add(status);
            Database.ChequeReqs.UpdateChequeReq(_dbContext, cheque);

            return Ok(status);
        }

    }
}
