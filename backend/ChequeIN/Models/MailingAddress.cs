using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ChequeIN.Models
{
    public class MailingAddress
    {
        private String line1;
        private String line2;
        private String city;
        private String postalCode;

        //Line 1 must begin with a number, followed by a space, and then a collection of letters
        //(including accented letters) or numbers.
        [DisplayName("Address Line 1")]
        [Required]
        [RegularExpression(@"^\d+ +(\p{L}|\p{Pd}|\d| )+$", ErrorMessage = "Invalid Address Line 1 format.")]
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

        //Line 2 must begin with a number, followed by a space, and then a collection of letters
        //(including accented letters) or numbers.
        [DisplayName("Address Line 2")]
        [RegularExpression(@"^\d+ +(\p{L}|\p{Pd}|\d| )+$", ErrorMessage = "Invalid Address Line 2 format.")]
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

        //The City can be a collection of any letters, including accented letters, and can include dashes or spaces.
        //Numbers are not allowed. 
        [Required]
        [RegularExpression(@"^(\p{L}|\p{Pd}| )+$", ErrorMessage = "Invalid City format.")]
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
        
        //Postal Codes can contain any of 'ABCEGHJKLMNPRSTVXY' in uppercase or lowercase. They must follow this format:
        //"C#C#C#" or "C#C #C#" where C is an accepted character, and # is a digit. 
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

        [Range(1, 13)] //When it isn't explicitly set, it takes the default value (0 or NONE in enum form), so this acts as "Required"
        public Enums.Province Province { get; set; }

    }
}
