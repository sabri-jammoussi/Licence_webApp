using LicenceApp.models.Applications;
using LicenceApp.models.AttributeLicence;
using LicenceApp.models.AttributeLicenceValeur;
using LicenceApp.models.GlobalDao;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace LicenceApp.models.Licences
{
    public class LicenceDto
    {
        public int Id { get; set; }
        public DateTime? DateExp { get; set; }
        public required string User { get; set; }
        public  int ClientId {  get; set; }
        public string ClientRaison { get; set; }
        public string ApplicationNom {  get; set; }
        public int? PartenaireId { get; set; }
        

        public int ApplicationId { get; set; }
        public IList<AttributeLicenceValeurDao> AttributesValues { get; set; }


    }
}
