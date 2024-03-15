

using LicenceApp.models.Applications;

namespace LicenceApp.Services.ApplicationService
{
    public interface IApplicationService
    {
        Task<List<ApplicationDto>> GetAll();
        Task<ApplicationDto> GetApplicationtbyId(int id);
        Task Create(NewApplication newApplication);
        Task Delete(int id);
        Task Update(UpdateApplication updateApplication);
    }
}
