using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LicenceApp.models.GlobalDao
{
    [Table("ENUMERATION_VALUE")]

    public class EnumerationValeurDao
    {
        [Key]
        [Column("EV_ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [StringLength(30)]
        [Column("EV_VALEUR")]
        public required string Valeur { get; set; }
        [Column("EN_ID")]
        public required int EnumerationId { get; set; }
        [ForeignKey(nameof(EnumerationId))]
        public virtual EnumerationDao Enumeration { get; set; }
    }
}
