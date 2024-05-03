using LicenceApp.Enums;

namespace LicenceApp.models.AttributeLicenceValeur
{
    public class AttributeLicenceValeurDto
    {
        public int Id { get; set; }
        public required string valeur { get; set; }
        public required int LicenceId { get; set; }
        public required int AttributeId { get; set; }

    }
}
