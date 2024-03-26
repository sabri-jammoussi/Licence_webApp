using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LicenceApp.models.GlobalDao
{
    [Table("CLIENTS")]
    public class ClientDao
    {
        [Key]
        [Column("CL_ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [StringLength(30)]
        [Column("CL_RAISON_SOCIAL")]
        public required string RaisonSocial { get; set; }
        [MaxLength(20)]
        [Column("CL_IDENTIFIANT")]
        public int? Identifiant { get; set; }
        [MaxLength(8), MinLength(8)]
        [Column("CL_TELEPHONE")]
        public required int Telephone { get; set; }
        [StringLength(30)]
        [Column("CL_EMAIL")]
        public required string Email { get; set; }
        [StringLength(30)]
        [Column("CL_VILLE")]
        public required string Ville { get; set; }
        [StringLength(30)]
        [Column("CL_ADRESSE")]
        public required string Adresse { get; set; }
        [StringLength(30)]
        [Column("CL_CODE_POSTAL")]
        public string? CodePostal { get; set; }
        [StringLength(30)]
        [Column("CL_PAYS")]
        public required string Pays { get; set; }
    }
}
