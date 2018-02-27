using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ChequeIN.Models;
using Microsoft.AspNetCore.Authorization;

namespace ChequeIN.Controllers
{
    [Route("api/[controller]")]
    public class ChequeReqsController : Controller
    {
        // GET api/ChequeReqs
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(Database.ChequeReqs.GetAllChequeReqs());
        }

        [HttpPost]
        public IActionResult Create([FromBody] ChequeReq cheque)
        {
            // if (cheque == null)
            //     return BadRequest();

            Console.WriteLine(cheque);

            var t = ModelState.IsValid;

            return Ok(t);
        }
    }
}
