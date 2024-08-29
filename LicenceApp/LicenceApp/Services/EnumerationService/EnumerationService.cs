using LicenceApp.Data;
using LicenceApp.models.Enumeration;
using LicenceApp.models.EnumerationValeur;
using LicenceApp.models.GlobalDao;
using Microsoft.EntityFrameworkCore;

namespace LicenceApp.Services.Enumeration
{
    public class EnumerationService : IEnumerationService
    {
        private readonly LicenceDBContext _dbContext ;

        public EnumerationService(LicenceDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Create(NewEnumeration newEnumeration)
        {
            var data = new EnumerationDao
            {
                Code = newEnumeration.Code,
                Nom = newEnumeration.Nom,

            };
            _dbContext.Enumerations.Add(data);
            await _dbContext.SaveChangesAsync();

        }

        public async Task Delete(int id)
        {
            var existinEnumValInAttVal = await _dbContext.attributeLicences.FirstOrDefaultAsync(x => x.EnumurationValue == id);
            if (existinEnumValInAttVal != null)
                throw new ApplicationException("Enumeration effectuer to attribute ");
            var enumeration  = await _dbContext.Enumerations.SingleOrDefaultAsync(u => u.Id == id);
            if (enumeration == null)
                throw new ApplicationException("enumeration n'existe pas");
            _dbContext.Enumerations.Remove(enumeration);
            await _dbContext.SaveChangesAsync();

        }

        public async Task<List<EnumerationDto>> GetAll()
        {
            List<EnumerationDto> result = await _dbContext.Enumerations
            .Select(u => new EnumerationDto
            {
                Id = u.Id,
                Code = u.Code,
                Nom = u.Nom,
                Valeurs = u.Valeurs.Select(ev => new EnumValDto
                {
                    Id = ev.Id,
                    Valeur = ev.Valeur
                }).ToList<EnumValDto>()
            }).ToListAsync();
            return result;
        }

        public async Task<EnumerationDto> GetEnumerationtbyId(int id)
        {
            var enumeration = await _dbContext.Enumerations
                .Include(e => e.Valeurs)
                .SingleOrDefaultAsync(u => u.Id == id);
            if(enumeration == null)
                throw new ApplicationException($"the id : {id} does not  existe  ");
       //     await _dbContext.Entry(enumeration)
       //.Collection(e => e.Valeurs)
       //.LoadAsync();
            var enumerationdto = new EnumerationDto
            {
                Id = enumeration.Id,
                Code = enumeration.Code,
                Nom = enumeration.Nom,
                Valeurs = enumeration.Valeurs.Select(ev => new EnumValDto
                {
                    Id = ev.Id,
                    Valeur = ev.Valeur
                }).ToList<EnumValDto>()
            };
             return enumerationdto;
        }

        public async Task Update(UpdateEnumeration updateEnumeration)
        {
            var existingEnumeration = await _dbContext.Enumerations.FirstOrDefaultAsync(u => u.Id == updateEnumeration.Id);
            if (existingEnumeration == null)
                throw new ApplicationException($"the id : {updateEnumeration.Id} does not  existe  ");
            existingEnumeration.Code = updateEnumeration.Code;
            existingEnumeration.Nom = updateEnumeration.Nom;
            await _dbContext.SaveChangesAsync();
        
        }
    }
}
