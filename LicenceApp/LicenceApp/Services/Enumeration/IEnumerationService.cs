using LicenceApp.models.clients;
using LicenceApp.models.Enumeration;

namespace LicenceApp.Services.Enumeration
{
    public interface IEnumerationService
    {
        Task<List<EnumerationDto>> GetAll();
        Task<EnumerationDto> GetEnumerationtbyId(int id);
        Task Create(NewEnumeration newEnumeration);
        Task Delete(int id);
        Task Update(UpdateEnumeration updateEnumeration);
    }
}
