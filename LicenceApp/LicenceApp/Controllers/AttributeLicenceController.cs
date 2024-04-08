using LicenceApp.models.AttributeLicence;
using LicenceApp.Services.AttributeLicenceService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.VisualBasic;
using System.Collections;
namespace LicenceApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttributeLicenceController : ControllerBase
    {
        private readonly IAttributeLicenceService _attributeLicenceService;

        public AttributeLicenceController(IAttributeLicenceService attributeLicenceService)
        {
            _attributeLicenceService = attributeLicenceService;
        }
        [HttpPost]
        public async Task<ActionResult> PostAttributeLicence ( NewAttributeLicence newAttributeLicence)
        {
            try
            {
                await _attributeLicenceService.Create(newAttributeLicence);
                return Ok("attribute licence added");
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AttributeLicenceDto>>> GetAttributes()
        {
            try
            {
                var attributes = await _attributeLicenceService.GetAll();
                return Ok(attributes);
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("getAttributeValue/{id}")]
        public async Task<ActionResult> GetAttributeValues ( int id)
        {
            try
            {
                var attributeVal = await _attributeLicenceService.GetAttributeValue(id);
                return Ok(attributeVal);
            }catch( Exception ex )
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("{id}")]
        public async Task<ActionResult> GetAttributeById (int id)
        {
            try
            {
                var attribute = await _attributeLicenceService.GetById(id);
                return Ok(attribute);
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("Type")]
        public async Task<ActionResult> GetAttributeType()
        {
            try
            {

                var user = await _attributeLicenceService.GetAttributeType();
                return Ok(user);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("update")]
        public async Task<ActionResult> updateAttribute (UpdateAttributeLicence updateAttributeLicence)
        {
            try
            {
                await _attributeLicenceService.Update(updateAttributeLicence);
                return Ok("Attribute updated ");
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete]
        public async Task<ActionResult> DeleteAttribute(int id)
        {
            try
            {
                var attribute = await _attributeLicenceService.GetById(id);
                if(attribute != null)
                    await _attributeLicenceService.Delete(id);
                return Ok("Attribute Deleted");
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
