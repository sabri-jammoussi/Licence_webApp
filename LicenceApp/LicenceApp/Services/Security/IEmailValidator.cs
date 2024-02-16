namespace LicenceApp.Services.Security
{
    public interface IEmailValidator
    {
        Task<bool> IsEmailValid(string email);
    }
}
