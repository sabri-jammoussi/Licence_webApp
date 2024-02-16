using Azure.Core;
using LicenceApp.models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace LicenceApp.Data
{
    public class LicenceDBContext : DbContext
    {
        public LicenceDBContext(DbContextOptions<LicenceDBContext> options) : base(options)
        {
        }
        public DbSet<UserDao> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
          ////  CreatePasswordHash(NewUser, out byte[] passwordHash, out byte[] passwordSalt);

          //  builder.Entity<UserDao>().HasData(
          //      new UserDao
          //      {
          //          Id = 1,
          //          FirstName = "sabri",
          //          LastName = "jammoussi",
          //          Email = "sabrijm123@gmail.com",
          //          PasswordHash = 0xB3BC80F6512ED66BAECDD041AB8BF83763B1D3EF87E9AEF85AC17F62EEB93A762B50F22BAAEFC67184027CADF93964C611F685FAF8B517A7FFB2AE004E73710E,
          //          PasswordSalt = 0x441162FE3D854009CC2F211F56EA1BB85B11F5AD1481EB21D21F0E5C079429ABDE707148C4074B200E1BDBCF6E3AE381105F31E4C44BC59859CCA654DDCEC223F05BF1B6B4D9F27A84419BA6FDF8CBCBDA0CECC164F412124006BA66F9E7A94E532D0D646592939C94F3ADB0E7BA7CDBB59FACFE422F003B923B8FF5FF7D76DC,
          //          Role = Enums.Role.Admin
          //      });
        }
    }
}
