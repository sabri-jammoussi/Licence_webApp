namespace LicenceApp.models.EnumerationValeur
{
    public class EnumerationValeurDto
    {
        public int Id { get; set; }
        public required string Valeur { get; set; }
        public required int EnumerationId { get; set; }

    }
}
