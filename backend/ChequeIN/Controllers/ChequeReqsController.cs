using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using ChequeIN.Models;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authorization;
using Amazon.S3;
using Amazon.S3.Model;
using System.Threading.Tasks;

namespace ChequeIN.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class ChequeReqsController : Controller
    {
        private Configurations.Authentication _authSettings;
        private DatabaseContext _dbContext;
        private IAmazonS3 _s3Client { get; set; }

        public ChequeReqsController(DatabaseContext dbContext, IOptions<Configurations.Authentication> authSettings, IAmazonS3 s3Client)
        {
            _authSettings = authSettings.Value;
            _dbContext = dbContext;
            _s3Client = s3Client;
        }

        //GET api/ChequeReqs
        [HttpGet]
        public IActionResult Get()
        {
            var user = Database.Users.GetCurrentUser(_dbContext, User, _authSettings.DisableAuthentication, _authSettings.DevelopmentUserId);
            if (user == null) {
                return Forbid();
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

        // GET api/ChequeReqs/id
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            bool exists = Database.ChequeReqs.TryGetChequeReq(_dbContext, id, out ChequeReq cheque);
            if (!exists)
            {
                return NotFound("The specified chequeReq does not exist");
            }
            else
            {
                var convert = ChequeIN.Models.API.Output.ChequeReq.FromModel(cheque);
                return Ok(convert);
            }
        }

        //Put api/ChequeReqs
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ChequeIN.Models.API.Input.ChequeReq cheque)
        {
            var status = new List<Status>{
                new Status {
                    SelectedStatus = Enums.StatusType.SUBMITTED,
                    StatusDate = DateTime.UtcNow,
                }
            };

            // TODO: Refactor
            List<SupportingDocument> supportingDocuments = new List<SupportingDocument>();
            foreach (var file in cheque.UploadedDocuments)
            {
                var fileId = Guid.NewGuid().ToString();
                var request = new PutObjectRequest
                {
                    BucketName = "chequein-dev-file-upload",
                    Key = fileId,
                    ContentBody = file.Base64Content,
                };
                var response = await _s3Client.PutObjectAsync(request);
                if (response.HttpStatusCode != System.Net.HttpStatusCode.OK)
                {
                    // Delete already uploaded files in case of an error
                    DeleteObjectsRequest multiObjectDeleteRequest = new DeleteObjectsRequest();
                    multiObjectDeleteRequest.BucketName = "chequein-dev-file-upload";
                    foreach (var doc in supportingDocuments) {
                        multiObjectDeleteRequest.AddKey(doc.FileIdentifier, null);
                    }
                    await _s3Client.DeleteObjectsAsync(multiObjectDeleteRequest);
                    return StatusCode(500, "Problem ecountered while uploading a file.");
                }
                supportingDocuments.Add(new SupportingDocument
                {
                    FileIdentifier = fileId,
                    Description = file.Description,
                });
            }

            var user = Database.Users.GetCurrentUser(_dbContext, User, _authSettings.DisableAuthentication, _authSettings.DevelopmentUserId);
            if (user == null)
                return Forbid();

            var convert = ChequeIN.Models.API.Input.ChequeReq.ToModel(cheque, user.UserProfileID, supportingDocuments, status);
            Database.ChequeReqs.StoreChequeReq(_dbContext, convert);

            return Ok(convert);
        }
    }
}
