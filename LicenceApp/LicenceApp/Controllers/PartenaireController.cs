using LicenceApp.models.GlobalDao;
using LicenceApp.models.Partenaires;
using LicenceApp.Services.PartenaireService;
using Microsoft.AspNetCore.Mvc;

namespace LicenceApp.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class PartenaireController : ControllerBase
    {
        private readonly IpartenaireService ipartenaireService;

        public PartenaireController(IpartenaireService ipartenaireService)
        {
            this.ipartenaireService = ipartenaireService;
        }
        [HttpGet]
        public  async Task<ActionResult<IEnumerable<PartenaireDao>>> GetPartenaires() 
        {
            try
            {
                var partenaires = await ipartenaireService.GetAll();
                return Ok(partenaires);
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("{id}")]
        public async Task<ActionResult> GetPatenaire(int id)
        {
            try
            {
                var partenaire = await ipartenaireService.GetPartenaireById(id);
                return Ok(partenaire);
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        public async Task<ActionResult> PostPartenaire(NewPartenaire newPartenaire)
        {
            try
            {
                await ipartenaireService.Create(newPartenaire);
                return Ok("Partenaire ajouter");
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("update")]
        public async Task<ActionResult> ModifierPartenaire (UpdatePartenaire updatePartenaire)
        {
            try
            {
                await ipartenaireService.Update(updatePartenaire);
                return Ok("Partenaire Modifier");
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete]
        public async Task<ActionResult> supprimerPartenaire(int id)
        {
            try
            {
                var partenaireId = await ipartenaireService.GetPartenaireById(id);
                if (partenaireId != null)
                    await ipartenaireService.Delete(id);
                return Ok("Partenaire supprimer");
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
