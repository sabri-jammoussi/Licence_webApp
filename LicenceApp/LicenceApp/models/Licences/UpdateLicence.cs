using static System.Runtime.InteropServices.JavaScript.JSType;

namespace LicenceApp.models.Licences
{
    public class UpdateLicence
    {
        public int id { get; set; }
        public DateTime? DateExp { get; set; }
        public int ClientId{ get; set; }
    }
}
