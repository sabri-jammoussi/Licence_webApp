namespace LicenceApp.models.Applications
{
    public class NewApplication
    {
        public int? Identifiant { get; set; }
        public required string Nom { get; set; }
        public required string Description { get; set; }
    }
}
