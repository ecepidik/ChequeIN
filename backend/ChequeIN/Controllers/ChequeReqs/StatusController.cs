using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChequeIN.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ChequeIN.Controllers.ChequeReqs
{
    [Route("api/chequereqs/{chequeId}/[controller]")]
    public class StatusController : Controller
    {
        private DatabaseContext _dbContext;

        public StatusController(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
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
