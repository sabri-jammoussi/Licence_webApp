using LicenceApp.models.clients;
using LicenceApp.models.GlobalDao;
using LicenceApp.Services.ClientService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LicenceApp.Controllers
{
    
    [ApiController]
    [Route("/api/[controller]")]
    public class ClientController :ControllerBase
    {
        private readonly IClientService _clientService;

        public ClientController(IClientService clientRepository )
        {
            _clientService = clientRepository ;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClientDao>>> GetClients()
        {
            try
            {
                var clients = await _clientService.GetAll();
                return Ok(clients);
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        //[Authorize(Roles = "Manager")]
        [HttpGet("{id}")]
        public async Task<ActionResult> GetClient(int id)
        {
            try
            {
                var client = await _clientService.GetClientbyId(id);
                return Ok(client);  
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
       // [Authorize(Roles = "Manager")]
        [HttpPost]
        public async Task<ActionResult> PostClient(NewClient newClient)
        {
            try
            {
                await _clientService.Create(newClient);
                return Ok("Client added");
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
      //  [Authorize(Roles = "Manager")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteClient(int id)
        {
            try
            {
                var client = await _clientService.GetClientbyId(id);
                if(client != null)
                    await _clientService.Delete(id);
                return Ok("user deleted");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
     //   [Authorize(Roles = "Manager")]
        [HttpPost("update")]
        public async Task<ActionResult> ModiferClient(UpdateClient updateClient)
        {
            try
            {
                await _clientService.Update(updateClient);
                return Ok("Client updated");
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
