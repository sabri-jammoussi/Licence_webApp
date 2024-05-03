using LicenceApp.Enums;
using LicenceApp.models.AttributeLicence;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Permissions;

namespace LicenceApp.models.GlobalDao
{
    [Table("ATTRIBUTE_LICENCE_VALUE")]
    public class AttributeLicenceValeurDao
    {
        [Key]
        [Column("ATTV_ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Column("ATTV_VALEUR")]
        public required string valeur { get; set; }
        [Column("ATT_ID")]
        public required int AttributeId { get; set; }
        [Column("LIC_ID")]
        public required int LicenceId { get; set; }
        [ForeignKey(nameof(LicenceId))]
        public virtual LicenceDao? LicenceDao { get; set; }

        [NotMapped]
        public AttributeLicenceDao? AttributeLicenceDto { get; set;}
   
    }
}
