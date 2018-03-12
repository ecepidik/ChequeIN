using ChequeIN.Models;
using ChequeIN.Models.API.Input;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Xunit;

namespace ChequeIN.Tests
{
    public class DocumentUploadTests
    {

        [Theory]
        [InlineData("This is a document")]
        [InlineData("12345")]
        public void DocumentUploadValidDesc(String value)
        {
            DocumentUpload doc = new DocumentUpload();
            doc.Description = value;
            doc.Base64Content = "abcd";

            var validationContext = new ValidationContext(doc, null, null);
            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(doc, validationContext, validationResults, true);
            Assert.True(isValid);
        }

        [Theory]
        [InlineData("1234")]
        [InlineData("")]
        [InlineData(" ")]
        public void DocumentUploadInvalidDesc(String value)
        {
            DocumentUpload doc = new DocumentUpload();
            doc.Description = value;
            doc.Base64Content = "abcd";

            var validationContext = new ValidationContext(doc, null, null);
            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(doc, validationContext, validationResults, true);
            Assert.False(isValid);
        }

        [Theory]
        [InlineData("This is a file that is awesome!")]
        [InlineData("a")]
        public void DocumentUploadValidFileSz(String value)
        {
            DocumentUpload doc = new DocumentUpload();
            doc.Base64Content = value;
            doc.Description = "abcdefg";

            var validationContext = new ValidationContext(doc, null, null);
            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(doc, validationContext, validationResults, true);
            Assert.True(isValid);
        }
    }
}
