using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using LicenceApp.models.AttributeLicence;

namespace LicenceApp.models.GlobalDao
{
    [Table("APPLICATION")]
    public class ApplicationDao
    {
        [Key]
        [Column("APP_ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("APP_IDENTIFIANT")]
        public int? Identifiant { get; set; }

        [StringLength(30)]
        [Column("APP_NOM")]
        public required string Nom { get; set; }

        [StringLength(30)]
        [Column("APP_DESCRIPTION")]
        public required string Description { get; set; }
        public virtual IList<AttributeLicenceDao> Attributes { get; set; }
        public virtual IList<LicenceDao> Licences { get; set; }

    }
}
