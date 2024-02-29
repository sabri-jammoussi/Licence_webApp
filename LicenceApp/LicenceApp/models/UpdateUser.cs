using LicenceApp.Enums;

namespace LicenceApp.models
{
    public class UpdateUser 
    {
        public required string? FirstName { get; set; }

        public required string LastName { get; set; }

        public required string Email { get; set; }

        public required Role Role { get; set; }

    }
}
