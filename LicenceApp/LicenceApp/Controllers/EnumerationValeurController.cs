using LicenceApp.Data;
using LicenceApp.models.EnumerationValeur;
using LicenceApp.Services.EnumerationValeur;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;

namespace LicenceApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnumerationValeurController : ControllerBase
    {
        private readonly IEnumerationValeurService _enumerationValeurService;

        public EnumerationValeurController(IEnumerationValeurService enumerationValeurService)
        {
            _enumerationValeurService = enumerationValeurService;
        }
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<EnumerationValeurDto>>> GetEnumerationValeur()
        //{
        //    try
        //    {
        //        var enumValeur = await _enumerationValeurService.GetAll();
        //        return Ok(enumValeur);
        //    }catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}
        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            try
            {
                var enumval = await _enumerationValeurService.GetById(id);
                return Ok(enumval);
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("getEnumVal/{id}")]
        public async Task<ActionResult> GetEnumVal(int id)
        {
            try
            {
                var enumval = await _enumerationValeurService.GetEnumValDto(id);
                return Ok(enumval);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        public async Task<ActionResult> PostEnumerationValeur ( NewEnumerationValeur newEnumerationValeur)
        {
            try
            {
                await _enumerationValeurService.Create(newEnumerationValeur);
                return Ok("EnumerationValeur added");
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("Update")]
        public async Task<ActionResult> updateEnumValeur ( UpdateEnumerationValeur updateEnumerationValeur)
        {
            try
            {
                await _enumerationValeurService.Update(updateEnumerationValeur);
                return Ok("enumerationValeur updated");
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete]
        public async Task<ActionResult> DeleteEnumValeur(int id)
        {
            try
            {
                var enumVal = await _enumerationValeurService.GetById(id);
                if (enumVal != null)
                    await _enumerationValeurService.Delete(id);
                return Ok("enumValeur Deleted ");
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
