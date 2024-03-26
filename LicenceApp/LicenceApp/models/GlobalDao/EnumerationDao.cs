using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LicenceApp.models.GlobalDao
{
    public class EnumerationDao
    {
        [Key]
        [Column("EN_ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [MaxLength(10)]
        [Column("EN_CODE")]
        public required string Code { get; set; }

        [StringLength(30)]
        [Column("EN_NOM")]
        public required string Nom { get; set; }
        public virtual IList<EnumerationValeurDao> Valeurs { get; set;}
    }
}
