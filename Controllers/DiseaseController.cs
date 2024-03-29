using Microsoft.AspNetCore.Mvc;
using PersonalizedHealthCenter.Models;
using PersonalizedHealthCenter.Services;

namespace PersonalizedHealthCenter.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DiseaseController : Controller
    {
        private readonly DiseaseService _diseaseService;

        public DiseaseController(DiseaseService diseaseService)
        {
            _diseaseService = diseaseService;
        }

        [HttpGet]
        public async Task<List<Disease>> Get()
        {
            return await _diseaseService.GetAsync();
        }

        [HttpGet("{name}")]
        public async Task<IActionResult> GetByName(string name)
        {
            var diseases = await _diseaseService.GetByNameAsync(name);

            if (diseases == null || diseases.Count == 0)
            {
                return NotFound();
            }

            return Ok(diseases);
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Disease disease)
        {
            await _diseaseService.CreateAsync(disease);
            return CreatedAtAction(nameof(Get), new { id = disease.Id }, disease);
        }
    }
}
