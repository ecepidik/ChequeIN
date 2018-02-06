using ChequeIN.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Xunit;

namespace ChequeIN.Tests
{
    public class ModelTests
    {
        [Theory]
        [InlineData("1645 rue des rigoles")]
        [InlineData("2100 Queen Street")]
        [InlineData("   0     s4d   Str33t  16  ")]
        public void MailingAddress_Line1TestValid(String line1)
        {
            MailingAddress address = new MailingAddress()
            {
                Line1 = line1,
                City = "Sherbrooke",
                PostalCode = "J1M2H2",
                Province = Enums.Province.QC
            };

            var validationContext = new ValidationContext(address, serviceProvider: null, items: null);
            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(address, validationContext, validationResults, true);

            Assert.True(isValid);
        }

        [Theory]
        [InlineData("1645, rue des rigoles")]
        [InlineData("2100 Queen Street!!!")]
        [InlineData("rue des rigoles")]
        [InlineData("1645rue des rigoles")]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("       ")]
        [InlineData("1 ")]
        public void MailingAddress_Line1TestInvalid(String line1)
        {
            MailingAddress address = new MailingAddress();
            if(line1 == null)
            {
                address = new MailingAddress()
                {
                    City = "Sherbrooke",
                    PostalCode = "J1M2H2",
                    Province = Enums.Province.QC
                };
            } else
            {
                address = new MailingAddress()
                {
                    Line1 = line1,
                    City = "Sherbrooke",
                    PostalCode = "J1M2H2",
                    Province = Enums.Province.QC
                };
            }
            

            var validationContext = new ValidationContext(address, serviceProvider: null, items: null);
            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(address, validationContext, validationResults, true);

            Assert.False(isValid);
        }

        [Theory]
        [InlineData("  1645 rue des rigoles   ")]
        [InlineData("2100 Queen Street")]
        [InlineData("   0     s4d   Str33t  16  ")]
        [InlineData(null)]
        public void MailingAddress_Line2TestValid(string line2)
        {
            MailingAddress address;
            if (line2 == null)
            {
                address = new MailingAddress()
                {
                    Line1 = "1645 rue des rigoles",
                    City = "Sherbrooke",
                    PostalCode = "J1M2H2",
                    Province = Enums.Province.QC
                };
            } else
            {
                address = new MailingAddress()
                {
                    Line1 = "1645 rue des rigoles",
                    Line2 = line2,
                    City = "Sherbrooke",
                    PostalCode = "J1M2H2",
                    Province = Enums.Province.QC
                };
            }
            

            var validationContext = new ValidationContext(address, serviceProvider: null, items: null);
            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(address, validationContext, validationResults, true);

            Assert.True(isValid);
        }

        [Theory]
        [InlineData("1645, rue des rigoles")]
        [InlineData("2100 Queen Street!!!")]
        [InlineData("rue des rigoles")]
        [InlineData("1645rue des rigoles")]
        [InlineData("1 ")]
        public void MailingAddress_Line2TestInvalid(String line2)
        {
            MailingAddress address = new MailingAddress()
            {
                Line1 = "1645 rue des rigoles",
                Line2 = line2,
                City = "Sherbrooke",
                PostalCode = "J1M2H2",
                Province = Enums.Province.QC
            };


            var validationContext = new ValidationContext(address, serviceProvider: null, items: null);
            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(address, validationContext, validationResults, true);

            Assert.False(isValid);
        }


    }
}
