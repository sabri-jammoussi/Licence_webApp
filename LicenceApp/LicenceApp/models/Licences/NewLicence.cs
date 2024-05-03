using LicenceApp.models.AttributeLicenceValeur;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace LicenceApp.models.Licences
{
    public class NewLicence
    {
        public DateTime? DateExp { get; set; }
        public int UserId{ get; set; }//
        public required int   ApplicationId { get; set; }
        public required int ClientId { get; set; }
        public IList<NewAttributeLicenceValeur> AttributesValues { get; set; }

    }
}
