using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using ChequeIN.Models;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authorization;

namespace ChequeIN.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class ChequeReqsController : Controller
    {
        private Configurations.Authentication _authSettings;

        public ChequeReqsController(IOptions<Configurations.Authentication> authSettings)
        {
            _authSettings = authSettings.Value;
        }

        //GET api/ChequeReqs
        [HttpGet]
        public IActionResult Get()
        {
            var user = Database.Users.GetCurrentUser(User, _authSettings.DisableAuthentication, _authSettings.DevelopmentUserId);
            if (user == null) {
                return StatusCode(403);
            }
            bool exists = Database.ChequeReqs.TryGetAllChequeReqs(user.AuthenticationIdentifier, out List<ChequeReq> cheques);
            if (!exists) {
                return Ok(Enumerable.Empty<ChequeReq>().ToList<ChequeReq>());
            }
            else {
                var convert = cheques.Select(x => ChequeIN.Models.API.Output.ChequeReq.FromModel(x));
                return Ok(convert);
            }
        }

        //Post api/ChequeReqs
        [HttpPost]
        public IActionResult Update([FromBody] ChequeIN.Models.API.Input.ChequeReq cheque)
        {
            bool b = Database.ChequeReqs.TryGetChequeReq(cheque.ChequeReqID, out ChequeReq model);
            if (!b) {
                return StatusCode(400);
            }
            var convert = ChequeIN.Models.API.Input.ChequeReq.ToModel(cheque, model.ChequeReqID, model.SupportingDocuments, model.StatusHistory);
            b = Database.ChequeReqs.TryUpdateChequeReq(convert);
            if (!b) {
                return StatusCode(400);
            }
            return StatusCode(200);
        }

        //Put api/ChequeReqs
        [HttpPut]
        public IActionResult Create([FromBody] ChequeIN.Models.API.Input.ChequeReq cheque)
        {
            var status = new List<Status>{
                new Status {
                    SelectedStatus = Enums.StatusType.SUBMITTED,
                    StatusDate = DateTime.UtcNow,
                }
            };

            var fakeSupporingDocs = new List<SupportingDocument> {
                new SupportingDocument {
                    FileIdentifier = 0,
                    Description = ""
                }
            };

            var convert = ChequeIN.Models.API.Input.ChequeReq.ToModel(cheque, new Random().Next(1000), null, null);
            Database.ChequeReqs.StoreChequeReq(convert);
            return StatusCode(200);
        }
    }
}
