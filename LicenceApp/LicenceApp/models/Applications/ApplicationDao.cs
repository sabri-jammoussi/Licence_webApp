using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LicenceApp.models.Applications
{
    [Table("APPLICATION")]
    public class ApplicationDao
    {
        [Key]
        [Column("U_ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("U_IDENTIFIANT")]
        public int? Identifiant { get; set; }

        [StringLength(30)]
        [Column("U_NOM")]
        public required string Nom { get; set; }

        [StringLength(30)]
        [Column("U_DESCRIPTION")]
        public required string Description { get; set; }
    }
}
