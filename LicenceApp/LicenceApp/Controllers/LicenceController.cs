using LicenceApp.models.Licences;
using LicenceApp.Services.LicenceService;
using Microsoft.AspNetCore.Mvc;

namespace LicenceApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LicenceController : ControllerBase
    {
        private readonly ILicenceService _licenceService ;

        public LicenceController(ILicenceService licenceService)
        {
            _licenceService = licenceService;
        }
        [HttpPost]
        public async Task<ActionResult> PostLicence (NewLicence newLicence)
        {
            try
            {
                await _licenceService.Create(newLicence);
                return Ok("Licence added ");
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LicenceDto>>> GetLicences()
        {
            try
            {
                var Licences = await _licenceService.GetAll();
                return Ok (Licences);
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("{id}")]
        public async Task<ActionResult> GetLicenceId(int id)
        {
            try
            {
                var Licence = await _licenceService.GetById(id);
                return Ok(Licence);
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete]
        public async Task<ActionResult> DeleteLicence (int id)
        {
            try
            {
                var LicenceById  = await _licenceService.GetById(id);
                if (LicenceById != null)
                    await _licenceService.Delete(id);
                return Ok("Licence Supprimer "); 
            }catch (Exception ex)
            {
                return BadRequest (ex.Message);
            }
        }
        [HttpPost("update")]
        public async Task<ActionResult> UpdateLicence(UpdateLicence updateLicence)
        {
            try
            {
                await _licenceService.Update(updateLicence);
                return Ok("Licence Modifier "); 
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
