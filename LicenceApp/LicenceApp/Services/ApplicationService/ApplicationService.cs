using LicenceApp.Data;
using LicenceApp.models.Applications;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace LicenceApp.Services.ApplicationService
{
    public class ApplicationService : IApplicationService
    {
        private readonly LicenceDBContext _dBContext;

        public ApplicationService(LicenceDBContext dBContext)
        {
            _dBContext = dBContext;
        }

        public async Task Create(NewApplication newApplication)
        {
            var data = new ApplicationDao
            {
                Description = newApplication.Description,
                Identifiant = newApplication.Identifiant,
                Nom = newApplication.Nom,
            };   
            _dBContext.Applications.Add(data);
            await _dBContext.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var exisitingApp = await _dBContext.Applications.SingleOrDefaultAsync(x => x.Id == id);
            if (exisitingApp == null)
                throw new Exception("Application n'existe pas "); 
            _dBContext.Applications.Remove(exisitingApp);
            await _dBContext.SaveChangesAsync();
        }

        public async Task<List<ApplicationDto>> GetAll()
        {
            List<ApplicationDto> result = await _dBContext.Applications
               .Select(u => new ApplicationDto
               {
                   Id = u.Id,
                   Identifiant = u.Identifiant,
                   Nom = u.Nom,
                   Description= u.Description,
               }).ToListAsync();
            return result;
        }

        public async Task<ApplicationDto> GetApplicationtbyId(int id)
        {
            var exisitingApp = await _dBContext.Applications.SingleOrDefaultAsync(x => x.Id == id);
            if (exisitingApp == null)
                throw new ApplicationException($"the id : {id} does not  existe  ");
            var applictionDto = new ApplicationDto
            {
                Id = exisitingApp.Id,
                Identifiant = exisitingApp.Identifiant,
                Nom = exisitingApp.Nom,
                Description = exisitingApp.Description,
            };
            return applictionDto;
        }

        public async Task Update(UpdateApplication updateApplication)
        {
            var existingApp = await _dBContext.Applications.SingleOrDefaultAsync(x => x.Id == updateApplication.Id);
            if(existingApp == null)
                throw new ApplicationException($"The Id : {updateApplication.Id} you have been inserted  is  invalid ");
            existingApp.Identifiant = updateApplication.Identifiant;
            existingApp.Nom = updateApplication.Nom;
            existingApp.Description = updateApplication.Description;
            await _dBContext.SaveChangesAsync();
        }
    }
}
