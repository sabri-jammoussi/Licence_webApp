using LicenceApp.models.AttributeLicence;
using LicenceApp.models.GlobalDao;

namespace LicenceApp.models.Applications
{
    public class ApplicationDto
    {
        public int Id { get; set; }
        public int? Identifiant { get; set; }
        public required string Nom { get; set; }
        public required string Description { get; set; }
        public IList<AttributeLicenceDto> Attributes{ get; set; }

    }
}
