using ChequeIN.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Xunit;

namespace ChequeIN.Tests
{
    public class MaillingAddressTests
    {
        [Theory]
        [InlineData("1645 rue des rigoles")]
        [InlineData("2100 Queen Street")]
        [InlineData("   0     s4d   Str33t  16  ")]
        [InlineData("0 ASD--èéàçôŝĝĥĵîûŷêŵâĉÉÈÖÇÀ")]
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
        [InlineData("0 ASD--èéàçôŝĝĥĵîûŷêŵâĉÉÈÖÇÀ")]
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

        [Theory]
        [InlineData("  Trois Rivières   ")]
        [InlineData("Trois-Rivières")]
        [InlineData("ASD--èéàçôŝĝĥĵîûŷêŵâĉÉÈÖÇÀ")]
        public void MailingAddress_CityTestValid(string city)
        {
            MailingAddress address = new MailingAddress()
            {
                Line1 = "1645 rue des rigoles",
                City = city,
                PostalCode = "J1M2H2",
                Province = Enums.Province.QC
            };

            var validationContext = new ValidationContext(address, serviceProvider: null, items: null);
            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(address, validationContext, validationResults, true);

            Assert.True(isValid);
        }

        [Theory]
        [InlineData("  3 Rivières   ")]
        [InlineData("Trois_Rivières")]
        [InlineData("35476")]
        [InlineData("     ")]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("|}[''./`")]
        public void MailingAddress_CityTestInvalid(string city)
        {
            MailingAddress address = new MailingAddress();
            if (city == null)
            {
                address = new MailingAddress()
                {
                    Line1 = "1645 rue des rigoles",
                    PostalCode = "J1M2H2",
                    Province = Enums.Province.QC
                };
            } else
            {
                address = new MailingAddress()
                {
                    Line1 = "1645 rue des rigoles",
                    City = city,
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
        [InlineData("j1m2h2")]
        [InlineData("j1m 2h2")]
        [InlineData("    h2x2e5   ")]
        [InlineData("J1M2H2")]
        [InlineData("J1M 2H2")]
        [InlineData("    H2X2E5   ")]
        [InlineData("T5K1W1")]
        public void MailingAddress_PostalCodeTestValid(string postalCode)
        {
            MailingAddress address = new MailingAddress()
            {
                Line1 = "1645 rue des rigoles",
                City = "Sherbrooke",
                PostalCode = postalCode,
                Province = Enums.Province.QC
            };

            var validationContext = new ValidationContext(address, serviceProvider: null, items: null);
            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(address, validationContext, validationResults, true);

            Assert.True(isValid);
        }

        [Theory]
        [InlineData("J1MH2")]
        [InlineData("J1M  2H2")]
        [InlineData("H2X2E5P")]
        [InlineData("J1MHH2")]
        [InlineData("J1D2H2")] //Canadian postal codes can't include certain letters like 'D'.
        [InlineData("2H2J1M")]
        [InlineData("j1mh2")]
        [InlineData("j1m  2h2")]
        [InlineData("h2x2e5p")]
        [InlineData("j1mhh2")]
        [InlineData("j1d2h2")]
        [InlineData("2h2j1m")]
        [InlineData("  ")]
        [InlineData("")]
        [InlineData(null)]
        public void MailingAddress_PostalCodeTestInvalid(string postalCode)
        {
            MailingAddress address = new MailingAddress();

            if (postalCode == null)
            {
                address = new MailingAddress()
                {
                    Line1 = "1645 rue des rigoles",
                    City = "Sherbrooke",
                    Province = Enums.Province.QC
                };
            } else
            {
                address = new MailingAddress()
                {
                    Line1 = "1645 rue des rigoles",
                    City = "Sherbrooke",
                    PostalCode = postalCode,
                    Province = Enums.Province.QC
                };
            }

            var validationContext = new ValidationContext(address, serviceProvider: null, items: null);
            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(address, validationContext, validationResults, true);

            Assert.False(isValid);
        }

        [Fact]
        public void MailingAddress_ProvinceTestValid()
        {
            MailingAddress address = new MailingAddress()
            {
                Line1 = "1645 rue des rigoles",
                City = "Sherbrooke",
                PostalCode = "J1M2H2",
                Province = Enums.Province.QC
            };

            var validationContext = new ValidationContext(address, serviceProvider: null, items: null);
            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(address, validationContext, validationResults, true);

            Assert.True(isValid);
        }

        [Fact]
        public void MailingAddress_ProvinceTestInvalid()
        {
            MailingAddress address = new MailingAddress()
            {
                Line1 = "1645 rue des rigoles",
                City = "Sherbrooke",
                PostalCode = "J1M2H2",
            };

            var validationContext = new ValidationContext(address, serviceProvider: null, items: null);
            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(address, validationContext, validationResults, true);

            Assert.False(isValid);
        }
    }
}
