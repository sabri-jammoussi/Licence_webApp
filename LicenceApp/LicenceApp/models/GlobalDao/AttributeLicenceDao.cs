using LicenceApp.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LicenceApp.models.GlobalDao
{
    [Table("ATTRIBUTE_LICENCE")]
    public class AttributeLicenceDao
    {
        [Key]
        [Column("ATT_ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [StringLength(20)]
        [Column("ATT_INTUTILE")]
        public required string Intutile { get; set; }
        [StringLength(30)]
        [Column("ATT_DESCRIPTION")]
        public required string Description { get; set; }
        [EnumDataType(typeof(Types))]

        [Column("ATT_TYPE")]
        public required Types Type { get; set; }

        [Column("ATT_ENUMERATION_VALUE")]
        public int? EnumurationValue { get; set; }

        [Column("ATT_OBLIGATION")]
         public bool Obligations {  get; set; }   
        [Column("APP_ID")]
        public required int ApplicationId { get; set; }
        [ForeignKey(nameof(ApplicationId))]
        public virtual ApplicationDao? Application { get; set; }

    }

}
