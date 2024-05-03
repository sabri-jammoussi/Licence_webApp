using LicenceApp.models.AttributeLicenceValeur;
using LicenceApp.Services.AttributeLicenceValuerService;
using Microsoft.AspNetCore.Mvc;

namespace LicenceApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttributeLicenceValeurController : ControllerBase
    {
        private readonly IAttributeLicenceValeurService _attributeLicenceValeurService;

        public AttributeLicenceValeurController(IAttributeLicenceValeurService attributeLicenceValeurService)
        {
            _attributeLicenceValeurService = attributeLicenceValeurService;
        }
        [HttpPost]
        public async Task<ActionResult> postAttributeLicenceValeur ( NewAttributeLicenceValeur newAttributeLicenceValeur)
        {
            try
            {
                await _attributeLicenceValeurService.Create(newAttributeLicenceValeur);
                return Ok("attribute Valeur added ");

            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AttributeLicenceValeurDto>>> GetAttributeValeurs()
        {
            try
            {
                
                var attributeValeurs = await _attributeLicenceValeurService.GetAll();
                return Ok(attributeValeurs); 
            }catch(Exception ex) 
            { 
            return BadRequest(ex.Message);
            }

        }
        [HttpGet("{id}")]
        public async Task<ActionResult> GetAttributevaleurbyId (int id )
        {
            try
            {
                var attributeLicValeur = await _attributeLicenceValeurService.GetById(id);
                return Ok(attributeLicValeur);
            }catch( Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("update")]
        public async Task<ActionResult> updateAttributeLicValeur (UpdateAttributeLicenceValeur updateAttributeLicenceValeur)
        {
            try
            {
                await _attributeLicenceValeurService.Update(updateAttributeLicenceValeur);
                return Ok("attribute Licence  Valeur Modifier  "); 
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete]
        public async  Task<ActionResult> DeleteAttributeLicValeur (int id)
        {
            try
            {
                var attributeLicVal = await _attributeLicenceValeurService.GetById(id);
                if (attributeLicVal != null)
                    await _attributeLicenceValeurService.Delete(id);
                return Ok(attributeLicVal);
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
