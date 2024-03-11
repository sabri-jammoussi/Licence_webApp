namespace LicenceApp.models
{
    public class UpdateProfilePassword
    {
        public required int Id { get; set; } 
        public required string CurrentPassword { get; set; }
        public required string NewPassword { get; set; }

    }
}
