using LicenceApp.Data;
using LicenceApp.models;
using LicenceApp.models.clients;
using LicenceApp.models.GlobalDao;
using LicenceApp.Services.Security;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace LicenceApp.Services.ClientService
{
    public class ClientService : IClientService
    {
        private readonly LicenceDBContext _dbContext;
        private readonly IEmailValidator _emailValidator;
        public ClientService(LicenceDBContext dbContext,IEmailValidator emailValidator)
        {
            _dbContext = dbContext;
            _emailValidator = emailValidator;
        }

        public async Task Create(NewClient newClient)
        {
            var isEmailValid = await _emailValidator.IsEmailValid(newClient.Email);
            if (!isEmailValid)
            {
                throw new ApplicationException("Email invalid");
            }
            var Telvalidation = await _dbContext.Clients.FirstOrDefaultAsync(x => x.Telephone == newClient.Telephone);
            var data = new ClientDao
            {
                
                RaisonSocial =newClient.RaisonSocial,
                Identifiant = newClient.Identifiant,
                Telephone = newClient.Telephone,
                Email = newClient.Email,
                Ville = newClient.Ville,
                Adresse = newClient.Adresse,
                CodePostal = newClient.CodePostal,
                Pays = newClient.Pays,

            };
             _dbContext.Clients.Add(data);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
           var client = await _dbContext.Clients.SingleOrDefaultAsync(x => x.Id == id);
            if (client == null)
                throw new ApplicationException("client n'existe pas ");
            _dbContext.Clients.Remove(client);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<ClientDto>> GetAll()
        {
            List<ClientDto> result = await _dbContext.Clients
                .Select(u => new ClientDto
                {
                    Id = u.Id,
                    RaisonSocial = u.RaisonSocial,
                    Identifiant = u.Identifiant,
                    Telephone = u.Telephone,
                    Email = u.Email,
                    Ville = u.Ville,
                    Adresse = u.Adresse,
                    CodePostal = u.CodePostal,
                    Pays = u.Pays,
                }).ToListAsync();
            return result;
        }

        public async Task<ClientDto> GetClientbyId(int id)
        {
            var client =await _dbContext.Clients.FirstOrDefaultAsync(u => u.Id == id);
            if (client == null)
            {
                throw new ApplicationException($"the id : {id} does not  existe  ");
            }
          
            var clientdto = new ClientDto
            {
                Id = client.Id,
                RaisonSocial = client.RaisonSocial,
                Identifiant = client.Identifiant,
                Telephone = client.Telephone,
                Email = client.Email,
                Ville = client.Ville,
                Adresse = client.Adresse,
                CodePostal = client.CodePostal,
                Pays = client.Pays,
            };
            return clientdto;
        }

        public async Task Update(UpdateClient updateClient)
        {
            var existingClient = await _dbContext.Clients.FirstOrDefaultAsync(x => x.Id == updateClient.Id);
            if(existingClient == null)
            {
                throw new ApplicationException($"The Id : {updateClient.Id} you have been inserted  is  invalid ");
            }
            var isEmailValid = await _emailValidator.IsEmailValid(updateClient.Email);
            if (!isEmailValid)
            {
                throw new ApplicationException("Email invalid");
            }
            existingClient.RaisonSocial = updateClient.RaisonSocial;
            existingClient.Identifiant= updateClient.Identifiant;
            existingClient.Email = updateClient.Email;
            existingClient.Telephone= updateClient.Telephone;
            existingClient.Ville= updateClient.Ville;
            existingClient.Pays= updateClient.Pays;
            existingClient.Adresse= updateClient.Adresse;
            existingClient.CodePostal= updateClient.CodePostal;
            await _dbContext.SaveChangesAsync();
        }
    }
}
