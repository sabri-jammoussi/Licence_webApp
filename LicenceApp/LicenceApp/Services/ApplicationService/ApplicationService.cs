using LicenceApp.Data;
using LicenceApp.models.Applications;
using LicenceApp.models.AttributeLicence;
using LicenceApp.models.GlobalDao;
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
                Identifiant = newApplication.Identifiant,
                Description = newApplication.Description,
                Nom = newApplication.Nom,
            };   
            _dBContext.Applications.Add(data);
            await _dBContext.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var existingLicence = await _dBContext.licences.FirstOrDefaultAsync( u => u.ApplicationId == id);

            if (existingLicence != null)
                throw new ApplicationException("the app is effected to licence ");
            var exisitingApp = await _dBContext.Applications.SingleOrDefaultAsync(x => x.Id == id);
            if (exisitingApp == null)
                throw new Exception("Application n'existe pas "); 
            _dBContext.Applications.Remove(exisitingApp);
            await _dBContext.SaveChangesAsync();
        }

        public async Task<List<ApplicationDto>> GetAll()
        {
            List<ApplicationDto> result = await _dBContext.Applications
                .Include(app => app.Attributes)
               .Select(u => new ApplicationDto
               {
                   Id = u.Id,
                   Identifiant = u.Identifiant,
                   Nom = u.Nom,
                   Description= u.Description,
                   Attributes = u.Attributes.Select(att => new AttributeLicenceDto
                   {
                       Id = att.Id,
                       Description= att.Description,
                       Intutile = att.Intutile,
                       Type = att.Type,
                       Obligations= att.Obligations,
                       EnumerationId = att.EnumurationValue
                   }).ToList<AttributeLicenceDto>()
               }).ToListAsync();
            return result;
        }

        public async Task<ApplicationDto> GetApplicationtbyId(int id)
        {
            var applicationEntity = await _dBContext.Applications
                 .Include(app => app.Attributes)
                .SingleOrDefaultAsync(app => app.Id == id);

            if (applicationEntity == null)

                throw new ApplicationException($"The Id : {id} you have been inserted  is  invalid ");
 

            var applicationDto = new ApplicationDto
            {
                Id = applicationEntity.Id,
                Identifiant = applicationEntity.Identifiant,
                Nom = applicationEntity.Nom,
                Description = applicationEntity.Description,
                Attributes = applicationEntity.Attributes.Select(att => new AttributeLicenceDto
                {
                    Id = att.Id,
                    Description = att.Description,
                    Intutile = att.Intutile,
                    Type = att.Type,
                    Obligations = att.Obligations,
                    EnumerationId = att.EnumurationValue
                }).ToList<AttributeLicenceDto>()
            };


            return applicationDto;
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
