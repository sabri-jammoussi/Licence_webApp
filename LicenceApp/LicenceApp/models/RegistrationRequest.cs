using LicenceApp.Enums;
using System.ComponentModel.DataAnnotations;

namespace LicenceApp.models
{
    public class RegistrationRequest
    {


        [Required]
        public string? FirstName { get; set; }
        [Required]

        public string? LastName { get; set; }
        [Required]
        public string? Email { get; set; }

        [Required]
        public string? Password { get; set; }

        public Role Role { get; set; }
    }
}
