using LicenceApp.Data;
using LicenceApp.models.AttributeLicence;
using LicenceApp.models.AttributeLicenceValeur;
using LicenceApp.models.GlobalDao;
using LicenceApp.models.Licences;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace LicenceApp.Services.LicenceService
{
    public class LicenceService : ILicenceService
    {
        private readonly LicenceDBContext _dbContext;

        public LicenceService(LicenceDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Create(NewLicence newLicence)
        {
            var appId =  _dbContext.Applications.SingleOrDefault(u => u.Id == newLicence.ApplicationId);
            if (appId == null)
                throw new ApplicationException($"ApplicationId : {newLicence.ApplicationId} n'existe pas  ");
            var ClientId = _dbContext.Clients.SingleOrDefault(x => x.Id == newLicence.ClientId);
            if (ClientId == null)
                throw new ApplicationException($"ClientId : {newLicence.ClientId} n'existe pas ");
            using (var transaction = await _dbContext.Database.BeginTransactionAsync())
            {
                try
                {
                    // Create LicenceDao
                    var licence = new LicenceDao
                    {
                        DateExp = newLicence.DateExp,
                        UserId = newLicence.UserId,
                        ApplicationId = newLicence.ApplicationId,
                        ClientId = newLicence.ClientId,
                        PartenaireId = newLicence.PartenaireId
                    };
                    _dbContext.licences.Add(licence);
                    await _dbContext.SaveChangesAsync();

                    // Create AttributeLicenceValeurDao objects
                    foreach (var attribute in newLicence.AttributesValues)
                    {
                        var attributeValeur = new AttributeLicenceValeurDao
                        {
                            valeur = attribute.Valeur,
                            AttributeId = attribute.AttributeId,
                            LicenceId = licence.Id // Set LicenceId to the newly created Licence's Id
                        };
                        _dbContext.attributeLicenceValeurs.Add(attributeValeur);
                    }
                    await _dbContext.SaveChangesAsync();

                    // Commit transaction
                    await transaction.CommitAsync();
                }
                catch (Exception)
                {
                    // Rollback transaction if an exception occurs
                    await transaction.RollbackAsync();
                    throw;
                }
            }
        }

        public async Task Delete(int id)
        {
            var licenceId = await _dbContext.licences.SingleOrDefaultAsync(x => x.Id==id);
            if (licenceId == null)
                throw new ApplicationException($"Licence Id : {id} n'existe pas ");
            _dbContext.licences.Remove(licenceId);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<LicenceDto>> GetAll()
        {
            List<LicenceDto> res = await _dbContext.licences
                .Include(u => u.Application)
                .Include( u => u.Client)
                .Select(u => new LicenceDto
                {
                    Id = u.Id,
                    User = GetUserFullName(u.UserId , _dbContext),
                    DateExp = u.DateExp,
                    ApplicationNom = u.Application.Nom,
                    ApplicationId = u.Application.Id,
                    ClientRaison = u.Client.RaisonSocial,
                    ClientId = u.Client.Id,
                    AttributesValues = u.AttributeValeurs,
                    PartenaireId = u.PartenaireId
                }).ToListAsync();
            return res; 
        }
   
        public async Task<LicenceDto> GetById(int id)
        {
            
            var res = await _dbContext.licences
                
                .Include( u => u.AttributeValeurs)
                .Include(u => u.Application)
                .Include(u => u.Client)
                .SingleOrDefaultAsync(x => x.Id==id);
            if (res == null)
                throw new ApplicationException($"Licence Id {id} n'existe pas ");
            var licenceDto = new LicenceDto
            {
                Id = res.Id,
                User = GetUserFullName(res.UserId, _dbContext),
                DateExp = res.DateExp,
                ApplicationNom = res.Application.Nom,
                ApplicationId = res.Application.Id,
                ClientRaison = res.Client.RaisonSocial,     
                ClientId = res.Client.Id,
                PartenaireId = res.PartenaireId,
                AttributesValues = res.AttributeValeurs.Select(attVal => new AttributeLicenceValeurDao
                {
                    Id = attVal.Id,
                    valeur = attVal.valeur,
                    LicenceId = attVal.LicenceId,
                    AttributeId = attVal.AttributeId,
                    AttributeLicenceDto = _dbContext.attributeLicences.Where(x => x.Id == attVal.AttributeId)
                     .Select(x => new AttributeLicenceDao
                     {
                         Id = x.Id, 
                         Description = x.Description,
                         Intutile = x.Intutile,
                         Type = x.Type,
                         Obligations = x.Obligations,
                         ApplicationId = x.ApplicationId,
                         EnumurationValue = x.EnumurationValue
                     })
                  .SingleOrDefault()
                }).ToList<AttributeLicenceValeurDao>()
            };
            return licenceDto;
        }
        private static string GetUserFullName(int userId, LicenceDBContext dbContext)
        {
            var user = dbContext.Users.FirstOrDefault(u => u.Id == userId);
            //   if (user == null)
            //     throw new ApplicationException("utilisateur wrong ");
            return user?.FirstName ?? "";
        }
        public async Task Update(UpdateLicence updateLicence)
        {
            var existingLicence = await _dbContext.licences.SingleOrDefaultAsync(x => x.Id == updateLicence.id);
            if (existingLicence == null)
                throw new ApplicationException($"Licence Id : {updateLicence.id} n'existe pas ");
            existingLicence.DateExp = updateLicence.DateExp;
            existingLicence.ClientId = updateLicence.ClientId;
            existingLicence.PartenaireId = updateLicence?.PartenaireId;
            await _dbContext.SaveChangesAsync(); 
        }
    }
}
