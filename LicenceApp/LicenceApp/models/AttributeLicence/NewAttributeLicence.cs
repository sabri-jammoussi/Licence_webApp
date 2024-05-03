using LicenceApp.Enums;

namespace LicenceApp.models.AttributeLicence
{
    public class NewAttributeLicence
    {
        public required string Intutile { get; set; }
        public required string Description { get; set; }
        public required Types Type { get; set; }
        public bool Obligations { get; set; }
        public required int ApplicationId { get; set; }
        public int? EnumerationId { get; set; }
    }
}
