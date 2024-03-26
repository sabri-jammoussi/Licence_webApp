using LicenceApp.models.Enumeration;
using LicenceApp.models.GlobalDao;
using LicenceApp.Services.Enumeration;
using Microsoft.AspNetCore.Mvc;

namespace LicenceApp.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class EnumerationController : ControllerBase
    {
        private readonly IEnumerationService _enumerationService;

        public EnumerationController(IEnumerationService enumerationService)
        {
            _enumerationService = enumerationService;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EnumerationDao>>> GetEnumerations()
        {
            try
            {
                var enumerations = await _enumerationService.GetAll();
                return Ok(enumerations);
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("{id}")]
        public async Task<ActionResult> GetEnumerationByID ( int id)
        {
            try
            {
                var enumeration = await _enumerationService.GetEnumerationtbyId(id);
                return Ok(enumeration);
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        public async Task<ActionResult> PostEnumeration( NewEnumeration newEnumeration)
        {
            try
            {
                await _enumerationService.Create(newEnumeration);
                return Ok("Enumeration added");
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("Update")]
        public async Task<ActionResult> UpdateEnumeration ( UpdateEnumeration updateEnumeration)
        {
            try
            {
                await _enumerationService.Update(updateEnumeration);
                return Ok("enumeration updated "); 
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteEnumeration (int id)
        {
            try
            {
                var enumeartion = await _enumerationService.GetEnumerationtbyId(id);
                if(enumeartion!= null)
                await _enumerationService.Delete(id);
                return Ok("enumeration deleted ");
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
