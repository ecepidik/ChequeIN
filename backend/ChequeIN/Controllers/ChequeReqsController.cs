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
        private DatabaseContext _dbContext;

        public ChequeReqsController(DatabaseContext dbContext, IOptions<Configurations.Authentication> authSettings)
        {
            _authSettings = authSettings.Value;
            _dbContext = dbContext;
        }

        //GET api/ChequeReqs
        [HttpGet]
        public IActionResult Get()
        {
            var user = Database.Users.GetCurrentUser(_dbContext, User, _authSettings.DisableAuthentication, _authSettings.DevelopmentUserId);
            if (user == null) {
                return StatusCode(403);
            }
            bool exists = Database.ChequeReqs.TryGetAllChequeReqs(_dbContext, user.AuthenticationIdentifier, out List<ChequeReq> cheques);
            if (!exists) {
                return Ok(Enumerable.Empty<ChequeReq>().ToList<ChequeReq>());
            }
            else {
                var convert = cheques.Select(x => ChequeIN.Models.API.Output.ChequeReq.FromModel(x));
                return Ok(convert);
            }
        }

        //Put api/ChequeReqs
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

            var user = Database.Users.GetCurrentUser(_dbContext, User, _authSettings.DisableAuthentication, _authSettings.DevelopmentUserId);

            var convert = ChequeIN.Models.API.Input.ChequeReq.ToModel(cheque, user.UserProfileID, fakeSupporingDocs, status);
            Database.ChequeReqs.StoreChequeReq(_dbContext, convert);
            return StatusCode(200);
        }
    }
}
