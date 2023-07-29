using System.ComponentModel.DataAnnotations;

namespace ClarkCodingChallenge.Models
{
    public class ContactModel
    {
        /*
         * When the user submits this data, the input should be validated against the following rules.Any validation errors should be presented to the user.
         * 
         *   First Name cannot be empty
         *   Last Name cannot be empty
         *   Email must be a valid email address.
         *   If all validation succeeds, a confirmation page should be displayed which tells the user the data was received by the system.
         *   
         */

        [Required]
        [StringLength(500)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(500)]
        public string LastName { get; set; }

        // RFC 5321 specifies max email address length of 254
        [StringLength(254, MinimumLength = 1)]
        [Required]
        [EmailAddress]
        public string EmailAddress { get; set; }
    }
}
