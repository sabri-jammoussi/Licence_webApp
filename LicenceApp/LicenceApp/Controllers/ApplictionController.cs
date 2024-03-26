using LicenceApp.models.Applications;
using LicenceApp.Services.ApplicationService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LicenceApp.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class ApplictionController:ControllerBase
    {
        private readonly IApplicationService _applicationService;

        public ApplictionController(IApplicationService applicationRepository)
        {
            _applicationService = applicationRepository;
        }
        [Authorize(Roles = "Manager , Admin")]

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ApplicationDto>>> GetApplications()
        {
            try
            {
                var applictions = await _applicationService.GetAll();
                return Ok(applictions);
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Authorize(Roles = "Admin")]

        [HttpGet("{id}")]
        public async Task<ActionResult> GetApplicaiton(int id)
        {
            try
            {
                var apps = await _applicationService.GetApplicationtbyId(id);
                return Ok(apps);
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Authorize(Roles = "Admin")]

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteApplication(int id )
        {
            try
            {
                var app = await _applicationService.GetApplicationtbyId(id);
                if(app != null)
                    await _applicationService.Delete(id);
                return Ok("Appliction Deleted ");
                
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult> CreateApp(NewApplication newApplication)
        {
            try
            {
                await _applicationService.Create(newApplication);
                return Ok("Application Added");

            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("modifierApplication")]
        public async Task<ActionResult> updateApp(UpdateApplication updateApplication)
        {
            try
            {
                await _applicationService.Update(updateApplication);
                return Ok("application updated ");
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
