using LicenceApp.models.GlobalDao;

namespace LicenceApp.models.Enumeration
{
    public class EnumerationDto
    {
        public int Id { get; set; }
        public required string Code { get; set; }
        public required string Nom { get; set; }
        public IList<EnumerationValeurDao> Valeurs{get;set;}
    }
}
