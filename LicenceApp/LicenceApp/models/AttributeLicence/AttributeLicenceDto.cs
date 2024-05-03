using LicenceApp.Enums;
using LicenceApp.models.AttributeLicenceValeur;
using LicenceApp.models.GlobalDao;

namespace LicenceApp.models.AttributeLicence
{
    public class AttributeLicenceDto
    {
        public int Id { get; set; }

        public required string Intutile { get; set; }
        public required string Description { get; set; }
        public bool Obligations { get; set; }

        public required Types Type { get; set; }
        public int? EnumerationId { get; set; }



    }
}
