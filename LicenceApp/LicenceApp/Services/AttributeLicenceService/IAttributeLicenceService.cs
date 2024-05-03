using LicenceApp.models.AttributeLicence;

namespace LicenceApp.Services.AttributeLicenceService
{
    public interface IAttributeLicenceService
    {
        Task<List<AttributeLicenceDto>> GetAll();
        Task<AttributeType> GetAttributeType();
        Task<AttributeLicenceDto> GetById(int id);
        Task<List<AttributeLicenceDto>> GetAttributeValue(int id);
        Task Create(NewAttributeLicence newAttributeLicence);
        Task Delete(int id);
        Task Update(UpdateAttributeLicence updateAttributeLicence);

    }
}
