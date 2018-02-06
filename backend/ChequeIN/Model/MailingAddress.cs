using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ChequeIN.Model
{
    public class MailingAddress
    {
        private String line1;
        private String line2;
        private String city;
        private String postalCode;

        [DisplayName("Address Line 1")]
        [Required]
        [RegularExpression(@"^\d+ +(\w| )+$", ErrorMessage = "Invalid Address Line 1 format.")]
        public String Line1
        {
            get
            {
                return this.line1;
            }
            set
            {
                this.line1 = value.Trim();
            }
        }

        [DisplayName("Address Line 2")]
        [RegularExpression(@"^\d+ +(\w| )+$", ErrorMessage = "Invalid Address Line 2 format.")]
        public String Line2
        {
            get
            {
                return this.line2;
            }
            set
            {
                this.line2 = value.Trim();
            }
        }

        [Required]
        [RegularExpression(@"^([A-Z]|[a-z]| )+$", ErrorMessage = "Invalid City format.")]
        public String City
        {
            get
            {
                return this.city;
            }
            set
            {
                this.city = value.Trim();
            }
        }

        [DisplayName("Postal Code")]
        [Required]
        [RegularExpression(@"^[ABCEGHJKLMNPRSTVXYabceghjklmnprstvxy]{1}\d{1}[ABCEGHJKLMNPRSTVWXYZabceghjklmnprstv‌​xy]{1} ??\d{1}[ABCEGHJKLMNPRSTVWXYZabceghjklmnprstvxy]{1}\d{1}$",
            ErrorMessage = "Invalid Postal Code format.")]
        public String PostalCode
        {
            get
            {
                return this.postalCode;
            }
            set
            {
                this.postalCode = value.Trim();
            }
        }

        [Required]
        public Enums.Province Province { get; set; }

    }
}
