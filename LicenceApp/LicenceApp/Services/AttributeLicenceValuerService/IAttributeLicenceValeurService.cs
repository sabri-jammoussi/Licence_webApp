using LicenceApp.models.AttributeLicenceValeur;


namespace LicenceApp.Services.AttributeLicenceValuerService
{
    public interface IAttributeLicenceValeurService
    {
        Task<List<AttributeLicenceValeurDto>> GetAll();
        Task<AttributeLicenceValeurDto> GetById(int id);
        Task Create(NewAttributeLicenceValeur newAttributeLicenceValeur);
        Task Update(UpdateAttributeLicenceValeur updateAttributeLicenceValeur);
        Task Delete(int id);
    }
}
