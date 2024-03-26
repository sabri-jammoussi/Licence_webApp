using LicenceApp.models.EnumerationValeur;

namespace LicenceApp.Services.EnumerationValeur
{
    public interface IEnumerationValeurService
    {
        Task<List<EnumerationValeurDto>> GetAll();
        Task<EnumerationValeurDto> GetById(int id);
        Task<List<EnumValDto>> GetEnumValDto(int id);

        Task Create(NewEnumerationValeur newEnumerationValeur);
        Task Delete(int id);
        Task Update(UpdateEnumerationValeur updateEnumerationValeur);
        
    }
}
