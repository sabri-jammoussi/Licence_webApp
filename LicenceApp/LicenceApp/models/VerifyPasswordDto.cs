using System.ComponentModel.DataAnnotations;

namespace LicenceApp.models
{
    public class VerifyPasswordDto
    {
        public int Id { get; set; }
      
        public string CurrentPassword { get; set; } = string.Empty;
    }
}
