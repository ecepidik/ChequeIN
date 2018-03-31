using ChequeIN.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Xunit;

namespace ChequeIN.Tests
{
    public class LedgerAccountTests
    {
        private LedgerAccount GenerateValidLedgerAccount(){
            return new LedgerAccount
            {
                Number = 1234,
                LedgerAccountID = 1,
                Type = "RANDOM",
                Name = "Account"
            };
        }

        [Theory]
        [InlineData("Rocket Team")]
        [InlineData("Grad Committee Rev")]
        [InlineData("ÉUS Account")]
        [InlineData("E-Week Beer Exp")]
        [InlineData("MAME-Photocopy Exp")]
        [InlineData("McGill Students' Flying Club Air Race Rev")]
        [InlineData("SEAM (Sustainability Chair) Rev")]
        [InlineData("McGill Robotics - Project 1 Rev")]
        public void ChequeReq_AccountNameValid(String value)
        {
            LedgerAccount acct = GenerateValidLedgerAccount();
            acct.Name = value;

            var validationContext = new ValidationContext(acct, null, null);
            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(acct, validationContext, validationResults, true);
            Assert.True(isValid);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        public void ChequeReq_AccountNameInvalid(String value)
        {
            LedgerAccount acct = GenerateValidLedgerAccount();
            acct.Name = value;

            var validationContext = new ValidationContext(acct, null, null);
            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(acct, validationContext, validationResults, true);
            Assert.False(isValid);
        }

        [Theory]
        [InlineData(5520-23)]
        [InlineData(5420-42)]
        public void ChequeReq_AccountNumberValid(int value)
        {
            LedgerAccount acct = GenerateValidLedgerAccount();
            acct.Number = value;

            var validationContext = new ValidationContext(acct, null, null);
            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(acct, validationContext, validationResults, true);
            Assert.True(isValid);
        }

        //[Theory]
        public void ChequeReq_AccountNumberInvalid(int value)
        {
            LedgerAccount acct = GenerateValidLedgerAccount();
            acct.Number = value;

            var validationContext = new ValidationContext(acct, null, null);
            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(acct, validationContext, validationResults, true);
            Assert.False(isValid);
        }
    }
}
