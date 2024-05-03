using LicenceApp.Data;
using LicenceApp.models.AttributeLicenceValeur;
using LicenceApp.models.GlobalDao;
using Microsoft.EntityFrameworkCore;

namespace LicenceApp.Services.AttributeLicenceValuerService
{
    public class AttributeLicenceValeurService : IAttributeLicenceValeurService
    {
        private readonly LicenceDBContext _dbContext;

        public AttributeLicenceValeurService(LicenceDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Create(NewAttributeLicenceValeur newAttributeLicenceValeur)
        {
            var attId = await _dbContext.attributeLicences.SingleOrDefaultAsync(x => x.Id == newAttributeLicenceValeur.AttributeId);
            if (attId == null)
                throw new ApplicationException("eeror id ");
            var data = new AttributeLicenceValeurDao
            {
           
                AttributeId = newAttributeLicenceValeur.AttributeId,
                LicenceId = 0,
                valeur = newAttributeLicenceValeur.Valeur,
            };
            _dbContext.attributeLicenceValeurs.Add(data);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
           var attLicenceVal = await _dbContext.attributeLicenceValeurs.SingleOrDefaultAsync(u => u.Id==id);
            if (attLicenceVal == null)
                throw new ApplicationException($"the id : {id} n'existe pas ");
            _dbContext.attributeLicenceValeurs.Remove(attLicenceVal);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<AttributeLicenceValeurDto>> GetAll()
        {
            List<AttributeLicenceValeurDto> res = await _dbContext.attributeLicenceValeurs
                .Select(u => new AttributeLicenceValeurDto 
                { 
                    Id = u.Id,
                    valeur = u.valeur,
                    LicenceId = u.LicenceId,
                    AttributeId=u.AttributeId
                }).ToListAsync();
            return res;
        }

        public async Task<AttributeLicenceValeurDto> GetById(int id)
        {
           var attLicVal = await _dbContext.attributeLicenceValeurs.SingleOrDefaultAsync( x => x.Id == id);
            if (attLicVal == null)
                throw new ApplicationException($"the id : {id} n'exsite pas ");
            return new AttributeLicenceValeurDto
            {
                Id = attLicVal.Id,
                LicenceId = attLicVal.LicenceId,
                valeur=attLicVal.valeur,
                   AttributeId = attLicVal.AttributeId
            };

        }

        public async Task Update(UpdateAttributeLicenceValeur updateAttributeLicenceValeur)
        {
            var existingAttLicVal = await _dbContext.attributeLicenceValeurs.SingleOrDefaultAsync(x => x.Id == updateAttributeLicenceValeur.Id);
            if (existingAttLicVal == null)
                throw new ApplicationException($"the id : {updateAttributeLicenceValeur.Id} n'existe pas ");
            existingAttLicVal.valeur = updateAttributeLicenceValeur.valeur;
            existingAttLicVal.LicenceId = updateAttributeLicenceValeur.LicenceId;
            existingAttLicVal.AttributeId = updateAttributeLicenceValeur.AttributeId;
        }
    }
}
