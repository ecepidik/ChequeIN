﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChequeIN.Models;
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
        public IActionResult Get(int chequeId)
        {
            if(!Database.ChequeReqs.TryGetChequeReq(_dbContext, chequeId, out ChequeReq cheque))
            {
                return NotFound("The specified checkreq does not exist");
            }

            return Ok(cheque.StatusHistory);
        }

        [HttpPost]
        public IActionResult Post(int chequeId, [FromBody]Status status)
        {
            if (!Database.ChequeReqs.TryGetChequeReq(_dbContext, chequeId, out ChequeReq cheque))
            {
                return NotFound("The specified checkreq does not exist");
            }

            cheque.StatusHistory.Add(status);
            Database.ChequeReqs.UpdateChequeReq(_dbContext, cheque);

            return Ok(cheque.StatusHistory);
        }

    }
}
