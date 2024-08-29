using LicenceApp.Data;
using LicenceApp.models.GlobalDao;
using LicenceApp.models.Partenaires;
using LicenceApp.Services.Security;
using Microsoft.EntityFrameworkCore;

namespace LicenceApp.Services.PartenaireService
{
    public class PartenaireService : IpartenaireService
    {
       private readonly LicenceDBContext _dbContext;
        private readonly IEmailValidator _emailValidator;

        public PartenaireService(LicenceDBContext dbContext, IEmailValidator emailValidator)
        {
            _dbContext = dbContext;
            _emailValidator = emailValidator;
        }
        public async Task<List<PartenaireDto>> GetAll()
        {
            List<PartenaireDto> result = await _dbContext.Partenaires
                .Select(u => new PartenaireDto
                {
                    Id = u.Id,
                    RaisonSocial = u.RaisonSocial,
                    Telephone = u.Telephone,
                    Email = u.Email,
                    Ville = u.Ville,
                    Adresse = u.Adresse,
                    Responsable = u.Responsable,
                    Pays = u.Pays,
                }).ToListAsync();
            return result;
        }

        public async Task<PartenaireDto> GetPartenaireById(int id)
        {
            var partenaireId = await _dbContext.Partenaires.SingleOrDefaultAsync( u =>  u.Id == id );
            if (partenaireId == null)
                throw new ApplicationException($"the id :{id} does not existe ");
            var partenaireResponse = new PartenaireDto
            {
                Id = partenaireId.Id,
                RaisonSocial = partenaireId.RaisonSocial,
                Telephone = partenaireId.Telephone,
                Email = partenaireId.Email,
                Ville = partenaireId.Ville,
                Adresse = partenaireId.Adresse,
                Responsable = partenaireId.Responsable,
                Pays = partenaireId.Pays,
            };
            return partenaireResponse;

        }

        public async Task Create(NewPartenaire newPartenaire)
        {
            var isEmailValid = await _emailValidator.IsEmailValid(newPartenaire.Email);
            if (!isEmailValid)
                throw new ApplicationException("email invalid ");
            var data = new PartenaireDao
            {
                RaisonSocial = newPartenaire.RaisonSocial,
                Telephone = newPartenaire.Telephone,
                Email = newPartenaire.Email,
                Ville = newPartenaire.Ville,
                Adresse = newPartenaire.Adresse,
                Responsable = newPartenaire.Responsable,
                Pays = newPartenaire.Pays,

            };
            _dbContext.Partenaires.Add(data);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var partenaireExistInLicence = await _dbContext.licences.FirstOrDefaultAsync(u => u.PartenaireId == id);
            if (partenaireExistInLicence != null)
                throw new ApplicationException("partenaire effecter pour un licence ");
            var partenaireId = await _dbContext.Partenaires.SingleOrDefaultAsync(x => x.Id == id);
            if (partenaireId == null)
                throw new ApplicationException("Partenaire n'existe pas ");
            _dbContext.Partenaires.Remove(partenaireId);
            await _dbContext.SaveChangesAsync();    

        }

        public async Task Update(UpdatePartenaire updatePartenaire)
        {
            var existingPartenaire = await _dbContext.Partenaires.SingleOrDefaultAsync(u => u.Id == updatePartenaire.Id);
            if (existingPartenaire == null)
                throw new ApplicationException($"the id : {updatePartenaire.Id} does not existe ");
            var isEmailValid = await _emailValidator.IsEmailValid(updatePartenaire.Email);
            if (!isEmailValid)
                throw new ApplicationException("email invalid ");
            existingPartenaire.Telephone = updatePartenaire.Telephone;
            existingPartenaire.Adresse = updatePartenaire.Adresse;
            existingPartenaire.Ville = updatePartenaire.Ville;
            existingPartenaire.Email = updatePartenaire.Email;
            existingPartenaire.Pays = updatePartenaire.Pays;
            existingPartenaire.Responsable = updatePartenaire.Responsable;
            existingPartenaire.RaisonSocial = updatePartenaire.RaisonSocial;
            await _dbContext.SaveChangesAsync();
        }
    }
}
