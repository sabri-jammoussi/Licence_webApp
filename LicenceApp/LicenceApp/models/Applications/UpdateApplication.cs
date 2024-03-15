namespace LicenceApp.models.Applications
{
    public class UpdateApplication
    {
        public int Id { get; set; }
        public int? Identifiant { get; set; }
        public required string Nom { get; set; }
        public required string Description { get; set; }
    }
}
