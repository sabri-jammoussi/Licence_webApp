using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LicenceApp.models.GlobalDao
{
    [Table("PARTENAIRE")]

    public class PartenaireDao
    {
        [Key]
        [Column("APP_ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [StringLength(30)]
        [Column("PR_RAISON_SOCIAL")]
        public required string RaisonSocial { get; set; }
        [MaxLength(8), MinLength(8)]
        [Column("PR_TELEPHONE")]
        public required int Telephone { get; set; }
        [StringLength(30)]
        [Column("PR_EMAIL")]
        public required string Email { get; set; }
        [StringLength(30)]
        [Column("PR_VILLE")]
        public required string Ville { get; set; }
        [StringLength(30)]
        [Column("PR_ADRESSE")]
        public required string Adresse { get; set; }
        [StringLength(30)]
        [Column("PR_RESPONSABLE")]
        public required string Responsable { get; set; }
        [StringLength(30)]
        [Column("PR_PAYS")]
        public required string Pays { get; set; }
    }
}
