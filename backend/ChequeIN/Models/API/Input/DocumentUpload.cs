using System;
namespace ChequeIN.Models.API.Input
{
    public class DocumentUpload
    {
        public string Description { get; set; }
        public string Base64Content { get; set; }
    }
}
