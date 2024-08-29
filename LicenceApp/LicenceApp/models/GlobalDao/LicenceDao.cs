using LicenceApp.models.Applications;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace LicenceApp.models.GlobalDao
{
    [Table("LICENCE")]
    public class LicenceDao
    {
        [Key]
        [Column("LIC_ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Column("LIC_DATE_EXP", TypeName = "Date")]
        public DateTime? DateExp  { get; set; }
        [Column("LIC_USER_ID")]
        public int  UserId { get; set; }
        [Column("PR_ID")]
        public int? PartenaireId { get; set; }
        [Column("APP_ID")]
        public required int ApplicationId { get; set; }
       
        [ForeignKey(nameof(ApplicationId))]
        public virtual ApplicationDao? Application { get; set; }

        [Column("CL_ID")]
        public required int ClientId { get; set; }
        [ForeignKey(nameof(ClientId))]
        public virtual ClientDao? Client { get; set;}
        public virtual IList<AttributeLicenceValeurDao>? AttributeValeurs { get; set; }


    }
}
