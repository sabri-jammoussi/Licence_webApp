using LicenceApp.Enums;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LicenceApp.models
{
    [Table("USERS")]
    public class UserDao 
    {//this class is the global user 
        [Key]
        [Column("U_ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [StringLength(30)]
        [Column("U_FIRSTNAME")]
        public required string FirstName { get; set; }

        [StringLength(30)]
        [Column("U_LASTNAME")]
         public required string LastName { get; set; }

        [StringLength(30)]
        [Column("U_EMAIL")]
         public required string Email { get; set; }

        [Column("U_PASSWORD_HASH")]
        public byte[]? PasswordHash { get; set; }
        
        [Column("U_PASSWORD_SALT")]
        public byte[]? PasswordSalt { get; set; } 

        [EnumDataType(typeof(Role))]
        [Column("U_ROLE")]
        public Role Role { get; set; }
    }
}
