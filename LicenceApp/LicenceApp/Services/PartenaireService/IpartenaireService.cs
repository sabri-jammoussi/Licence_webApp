using LicenceApp.models.Partenaires;

namespace LicenceApp.Services.PartenaireService
{
    public interface IpartenaireService
    {
        Task<List<PartenaireDto>> GetAll();
        Task<PartenaireDto> GetPartenaireById(int id);
        Task Create(NewPartenaire newPartenaire);
        Task Delete(int id);
        Task Update(UpdatePartenaire updatePartenaire);
    }
}
