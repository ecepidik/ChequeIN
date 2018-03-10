using System;
using System.ComponentModel.DataAnnotations;

namespace ChequeIN.Models.API.Input
{
    public class DocumentUpload
    {
        [MinLength(5, ErrorMessage = "The file description must be at least 5 characters.")]
        [Required]
        public string Description { get; set; }
        [Required]
        [MaxLength(52428800, ErrorMessage = "The file is too large")]
        public string Base64Content { get; set; }
    }
}
