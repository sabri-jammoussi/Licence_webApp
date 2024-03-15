namespace LicenceApp.models.clients
{
    public class NewClient
    {
        public required string RaisonSocial {  get; set; }
        public int? Identifiant { get; set; }
        public required int Telephone { get; set; }

        public required string Email { get; set; }
        public required string Ville { get; set; }
        public required string Adresse { get; set; }
        public  string? CodePostal { get; set; }

        public required string Pays { get; set; }

    }
}
