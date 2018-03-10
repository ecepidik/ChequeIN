using ChequeIN.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Xunit;

namespace ChequeIN.Tests
{
    public class ChequeReqTests
    {
        private ChequeReq GenerateValidChequeReq() {
            return new ChequeReq {
                PreTax = 1,
                ChequeReqID = 1,
                GST = 1,
                PST = 1,
                HST = 1,
                PayeeName = "User",
                Description = "Desc",
                ApprovedBy = "Kareem Halabi",
                FreeFood = false,
                OnlinePurchases = false,
                ToBeMailed = true,
                MailingAddress = new MailingAddress(),
                SupportingDocuments = new List<SupportingDocument>() { new SupportingDocument() },
                StatusHistory = new List<Status>() { new Status() }
            };
        }

        [Theory]
        [InlineData(0.0001)]
        [InlineData(1.0)]
        [InlineData(10000)]
        public void ChequeReq_PretaxValid(float value) {
            ChequeReq req = GenerateValidChequeReq();
            req.PreTax = value;

            var validationContext = new ValidationContext(req, null, null);
            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(req, validationContext, validationResults, true);
            Assert.True(isValid);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-0.0001)]
        [InlineData(-1.0)]
        [InlineData(-10000)]
        public void ChequeReq_PretaxInvalid(float value) {
            ChequeReq req = GenerateValidChequeReq();
            req.PreTax = value;

            var validationContext = new ValidationContext(req, null, null);
            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(req, validationContext, validationResults, true);
            Assert.False(isValid);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-0)]
        [InlineData(0.0001)]
        [InlineData(1.0)]
        [InlineData(10000)]
        public void ChequeReq_GSTValid(float value) {
            ChequeReq req = GenerateValidChequeReq();
            req.GST = value;

            var validationContext = new ValidationContext(req, null, null);
            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(req, validationContext, validationResults, true);
            Assert.True(isValid);
        }

        [Theory]
        [InlineData(-0.0001)]
        [InlineData(-1.0)]
        [InlineData(-10000)]
        public void ChequeReq_GSTInvalid(float value) {
            ChequeReq req = GenerateValidChequeReq();
            req.GST = value;

            var validationContext = new ValidationContext(req, null, null);
            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(req, validationContext, validationResults, true);
            Assert.False(isValid);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-0)]
        [InlineData(0.0001)]
        [InlineData(1.0)]
        [InlineData(10000)]
        public void ChequeReq_PSTValid(float value) {
            ChequeReq req = GenerateValidChequeReq();
            req.PST = value;

            var validationContext = new ValidationContext(req, null, null);
            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(req, validationContext, validationResults, true);
            Assert.True(isValid);
        }

        [Theory]
        [InlineData(-0.0001)]
        [InlineData(-1.0)]
        [InlineData(-10000)]
        public void ChequeReq_PSTInvalid(float value) {
            ChequeReq req = GenerateValidChequeReq();
            req.PST = value;

            var validationContext = new ValidationContext(req, null, null);
            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(req, validationContext, validationResults, true);
            Assert.False(isValid);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-0)]
        [InlineData(0.0001)]
        [InlineData(1.0)]
        [InlineData(10000)]
        public void ChequeReq_HSTValid(float value) {
            ChequeReq req = GenerateValidChequeReq();
            req.HST = value;

            var validationContext = new ValidationContext(req, null, null);
            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(req, validationContext, validationResults, true);
            Assert.True(isValid);
        }

        [Theory]
        [InlineData(-0.0001)]
        [InlineData(-1.0)]
        [InlineData(-10000)]
        public void ChequeReq_HSTInvalid(float value) {
            ChequeReq req = GenerateValidChequeReq();
            req.HST = value;

            var validationContext = new ValidationContext(req, null, null);
            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(req, validationContext, validationResults, true);
            Assert.False(isValid);
        }

        [Theory]
        [InlineData("Alexis Giguère-Joannette")]
        [InlineData("Courtney Wright")]
        [InlineData("Courtney Alexandra Elizabeth Renatta Mary Yolanda Caroline Shelly Ellen Hellen Rose Meredith Wright")]
        [InlineData("Jim O'malley")]
        public void ChequeReq_PayeeValid(String value){
            ChequeReq req = GenerateValidChequeReq();
            req.PayeeName = value;

            var validationContext = new ValidationContext(req, null, null);
            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(req, validationContext, validationResults, true);
            Assert.True(isValid);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        public void ChequeReq_PayeeInvalid(String value){
            ChequeReq req = GenerateValidChequeReq();
            req.PayeeName = value;

            var validationContext = new ValidationContext(req, null, null);
            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(req, validationContext, validationResults, true);
            Assert.False(isValid);
        }
    }
}
