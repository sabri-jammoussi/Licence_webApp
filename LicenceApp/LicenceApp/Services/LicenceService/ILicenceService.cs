using LicenceApp.models.Licences;

namespace LicenceApp.Services.LicenceService
{
    public interface ILicenceService
    {
        Task<List<LicenceDto>> GetAll();
        Task<LicenceDto> GetById(int id);
        Task Create(NewLicence newLicence);
        Task Update(UpdateLicence updateLicence);
        Task Delete(int id);
    }
}
