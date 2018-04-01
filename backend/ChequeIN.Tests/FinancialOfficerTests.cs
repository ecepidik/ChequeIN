using System;
using ChequeIN.Models;
using ChequeIN.Controllers;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Xunit;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

namespace ChequeIN.Tests
{
    public class FinancialOfficerTests
    {
        private FinancialOfficer GenerateFinancialOfficer()
        {
            List<AccountType> aag = new List<AccountType>();
            AccountType account = new AccountType{
                AccountTypeID = 123456789
            };
            aag.Add(account);
                
            return new FinancialOfficer
            {
                Email = "kareem@eus.com",
                AuthenticationIdentifier = "kareemh",
            };
        }

        [Theory]
        [InlineData("Lucy")]
        [InlineData("Lucy   ")]
        public void validAuthId(string value){
            FinancialOfficer fo = GenerateFinancialOfficer();
            fo.AuthenticationIdentifier = value;

            var validationContext = new ValidationContext(fo, null, null);
            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(fo, validationContext, validationResults, true);
            Assert.True(isValid);
        }

        [Theory]
        [InlineData("")]
        [InlineData("   ")]
        public void invalidAuthId(string value)
        {
            FinancialOfficer fo = GenerateFinancialOfficer();
            fo.AuthenticationIdentifier = value;

            var validationContext = new ValidationContext(fo, null, null);
            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(fo, validationContext, validationResults, true);
            Assert.False(isValid);
        }

        [Theory]
        [InlineData("kareem@mail.mcgill.ca")]
        [InlineData("kareem@eus.ca   ")]
        public void validEmail(string value)
        {
            FinancialOfficer fo = GenerateFinancialOfficer();
            fo.Email = value;

            var validationContext = new ValidationContext(fo, null, null);
            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(fo, validationContext, validationResults, true);
            Assert.True(isValid);
        }

        [Theory]
        [InlineData("k")]
        [InlineData("k   ")]
        [InlineData("")]
        [InlineData("   ")]
        public void invalidEmail(string value)
        {
            FinancialOfficer fo = GenerateFinancialOfficer();
            fo.Email = value;

            var validationContext = new ValidationContext(fo, null, null);
            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(fo, validationContext, validationResults, true);
            Assert.False(isValid);
        }
    }
}
