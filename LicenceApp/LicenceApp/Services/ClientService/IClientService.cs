using LicenceApp.models;
using LicenceApp.models.clients;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;

namespace LicenceApp.Services.ClientService
{
    public interface IClientService
    {
        Task<List<ClientDto>> GetAll();
        Task<ClientDto> GetClientbyId(int id);
        Task Create(NewClient newClient);
        Task Delete (int id);   
        Task Update(UpdateClient updateClient);

    }
}
