using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace LicenceApp.Services.Security
{
    public class PasswordValidator : IPasswordValidator
    {
        public Task<bool> IsPasswordValid(string password)
        {
            return Task.FromResult(ValidatePassword(password));
        }

        private bool ValidatePassword(string password)
        {

            var pattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[#$^+=!*()@%&]).{8,}$";
            var regex = new Regex(pattern);

            return regex.IsMatch(password);
            //// Check the password conditions
            //if (password.Length < 8)
            //{

            //    return false;
            //}

            //if (!password.Any(char.IsUpper))
            //{

            //    return false;
            //}

            //if (!password.Any(char.IsNumber))
            //{

            //    return false;
            //}

            //// Password is valid
            //return true;
        }
    }
}
