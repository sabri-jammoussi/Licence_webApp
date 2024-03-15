using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LicenceApp.models.clients
{
    [Table("CLIENTS")]
    public class ClientDao
    {
        [Key]
        [Column("U_ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [StringLength(30)]
        [Column("U_RAISON_SOCIAL")]
        public required string RaisonSocial { get; set; }
        [MaxLength(20)]
        [Column("U_IDENTIFIANT")]
        public  int? Identifiant { get; set; }
        [MaxLength(8),MinLength(8)]
        [Column("U_TELEPHONE")]
        public required int Telephone{ get; set; }
        [StringLength(30)]
        [Column("U_EMAIL")]
        public required string Email { get; set; }
        [StringLength(30)]
        [Column("U_VILLE")]
        public required string Ville { get; set; }
        [StringLength(30)]
        [Column("U_ADRESSE")]
        public required string Adresse{ get; set; }
        [StringLength(30)]
        [Column("U_CODE_POSTAL")]
        public  string? CodePostal{ get; set; }
        [StringLength(30)]
        [Column("U_PAYS")]
        public required string Pays{ get; set; }
    }
}
