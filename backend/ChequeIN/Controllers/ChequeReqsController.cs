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
            // bool exists = Database.ChequeReqs.TryGetAllChequeReqs("3033", out List<ChequeReq> cheques);
            // if (!exists) {
            //     return Ok(Enumerable.Empty<ChequeReq>().ToList<ChequeReq>());
            // }
            // else {
            //     return Ok(cheques);
            // }
            return Ok(Database.ChequeReqs.GetAllChequeReqs());
        }

        [HttpPost]
        public IActionResult Create([FromBody] ChequeReq cheque)
        {
            return Ok(true);
        }
    }
}
