using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LicenceApp.Services.Security
{
    public class EmailValidator : IEmailValidator
    {
        public Task<bool> IsEmailValid(string email)
        {
            return Task.FromResult(ValidateEmail(email));
        }

        private bool ValidateEmail(string email)
        {
            //var pattern = @"^[a-zA-Z0-9.!#$%&'*+-/=?^_`{|}~]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$";
            //var pattern = @"^[0-9A-Z]([-.\w]*[0-9A-Z])*$";
            var pattern = @"^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$";
            var regex = new Regex(pattern);

            return regex.IsMatch(email);
        }
    }
}
