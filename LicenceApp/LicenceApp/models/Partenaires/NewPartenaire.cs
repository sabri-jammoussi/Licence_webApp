namespace LicenceApp.models.Partenaires
{
    public class NewPartenaire
    {
        public required string RaisonSocial { get; set; }
        public required int Telephone { get; set; }

        public required string Email { get; set; }
        public required string Ville { get; set; }
        public required string Adresse { get; set; }
        public required string Responsable { get; set; }

        public required string Pays { get; set; }
    }
}
