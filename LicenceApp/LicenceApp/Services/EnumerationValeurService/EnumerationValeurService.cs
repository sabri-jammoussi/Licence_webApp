using LicenceApp.Data;
using LicenceApp.models.Enumeration;
using LicenceApp.models.EnumerationValeur;
using LicenceApp.models.GlobalDao;
using Microsoft.EntityFrameworkCore;

namespace LicenceApp.Services.EnumerationValeur
{
    public class EnumerationValeurService : IEnumerationValeurService
    {
        private readonly LicenceDBContext _dbContext;

        public EnumerationValeurService(LicenceDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Create(NewEnumerationValeur newEnumerationValeur)
        {
            var enumId = await _dbContext.Enumerations.FirstOrDefaultAsync(x => x.Id == newEnumerationValeur.EnumerationId);
            if (enumId == null)
                throw new ApplicationException($"the id : {newEnumerationValeur.EnumerationId} does not  existe or is null  ");
            var enumval = await _dbContext.enumerationValeurs.FirstOrDefaultAsync(w => w.Valeur == newEnumerationValeur.Valeur);
            if (enumval != null)
                throw new ApplicationException("valeur should not be null ");
            var data = new EnumerationValeurDao
            {
                Valeur = newEnumerationValeur.Valeur,
                EnumerationId = newEnumerationValeur.EnumerationId,
            };
            _dbContext.enumerationValeurs.Add(data);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {

            var existingEnumVal = await _dbContext.enumerationValeurs.FirstOrDefaultAsync(u => u.Id == id);
            if (existingEnumVal == null)
                throw new ApplicationException($"the id : {id} does not  existe  ");
            _dbContext.enumerationValeurs.Remove(existingEnumVal);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<EnumerationValeurDto>> GetAll()
        {
            List<EnumerationValeurDto> result = await _dbContext.enumerationValeurs
                .Select(u => new EnumerationValeurDto
                {
                    Id = u.Id,
                    Valeur = u.Valeur,
                    EnumerationId = u.EnumerationId,
                }).ToListAsync();
            return result;
        }

        public async Task<EnumerationValeurDto> GetById(int id)
        {
            var enumVal = await _dbContext.enumerationValeurs.FirstOrDefaultAsync(u => u.Id == id);
            if (enumVal == null)
                throw new ApplicationException($"the id : {id} does not existe ");
            var enumDto = new EnumerationValeurDto
            {
                Id = enumVal.Id,
                Valeur = enumVal.Valeur,
                EnumerationId = enumVal.EnumerationId
            };
            return enumDto;
        }
        public async Task<List<EnumValDto>> GetEnumValDto(int id)
        {
            var enumVals = await _dbContext.enumerationValeurs
                                        .Where(u => u.EnumerationId == id)
                                        .ToListAsync();

            //if (enumVals == null || enumVals.Count == 0)
            //    throw new ApplicationException($"No enumeration values found for the id: {id}");

            var enumValDtos = enumVals.Select(enumVal => new EnumValDto
            {
                Id = enumVal.Id,
                Valeur = enumVal.Valeur,
            }).ToList();

            return enumValDtos;
        }

  

        public async Task Update(UpdateEnumerationValeur updateEnumerationValeur)
        {
            var existingEnumVal = await _dbContext.enumerationValeurs.FirstOrDefaultAsync(u => u.Id == updateEnumerationValeur.Id);
            if(existingEnumVal == null)
                throw new ApplicationException($"the id : {updateEnumerationValeur.Id} does not  existe  ");
            existingEnumVal.Valeur=updateEnumerationValeur.Valeur;
            await _dbContext.SaveChangesAsync();
        }

  
    }
}
