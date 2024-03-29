using Microsoft.AspNetCore.Mvc;
using PersonalizedHealthCenter.Models;
using PersonalizedHealthCenter.Services;

namespace PersonalizedHealthCenter.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DrugController : Controller
    {
        private readonly DrugService _drugService;

        public DrugController(DrugService drugService)
        {
            _drugService = drugService;
        }

        [HttpGet]
        public async Task<List<Drug>> Get()
        {
            return await _drugService.GetAllAsync();
        }

        [HttpGet("{name}")]
        public async Task<IActionResult> GetByName(string name)
        {
            var drugs = await _drugService.GetByNameAsync(name);

            if (drugs == null || drugs.Count == 0)
            {
                return NotFound();
            }

            return Ok(drugs);
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Drug drug)
        {
            await _drugService.CreateAsync(drug);
            return CreatedAtAction(nameof(Get), new { id = drug.Id }, drug);
        }

    }
}
