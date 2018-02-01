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

        [DisplayName("Address Line 1")]
        [Required]
        public String Line1 { get; set; }

        [DisplayName("Address Line 2")]
        public String Line2 { get; set; }

        [Required]
        public String City { get; set; }

        [DisplayName("Postal Code")]
        [Required]
        [RegularExpression(@"^[ABCEGHJKLMNPRSTVXYabceghjklmnprstvxy]{1}\d{1}[ABCEGHJKLMNPRSTVWXYZabceghjklmnprstv‌​xy]{1} ??\d{1}[ABCEGHJKLMNPRSTVWXYZabceghjklmnprstvxy]{1}\d{1}$",
            ErrorMessage = "Invalid Postal Code format.")]
        public String PostalCode { get; set; }

        [Required]
        public Enums.Province Province { get; set; }

    }
}
