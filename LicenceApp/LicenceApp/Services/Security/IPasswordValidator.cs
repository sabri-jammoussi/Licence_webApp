using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace LicenceApp.Services.Security
{
    public interface IPasswordValidator
    {
        Task<bool> IsPasswordValid(string password);
    }
}
