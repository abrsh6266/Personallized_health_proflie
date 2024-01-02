using Microsoft.AspNetCore.Mvc;
using PersonalizedHealthCenter.Models;
using PersonalizedHealthCenter.Services;
using System.Threading.Tasks;

namespace PersonalizedHealthCenter.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SymptomController : Controller
    {
        private readonly SymptomService _symptomService;

        public SymptomController(SymptomService symptomService)
        {
            _symptomService = symptomService;
        }

        [HttpGet]
        public async Task<List<Symptom>> Get()
        {
            return await _symptomService.GetAsync();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var symptom = await _symptomService.GetByIdAsync(id);

            if (symptom == null)
            {
                return NotFound();
            }

            return Ok(symptom);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Symptom symptom)
        {
            await _symptomService.CreateAsync(symptom);
            return CreatedAtAction(nameof(Get), new { id = symptom.Id }, symptom);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody] Symptom updatedSymptom)
        {
            var existingSymptom = await _symptomService.GetByIdAsync(id);

            if (existingSymptom == null)
            {
                return NotFound();
            }

            await _symptomService.UpdateAsync(id, updatedSymptom);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var symptom = await _symptomService.GetByIdAsync(id);

            if (symptom == null)
            {
                return NotFound();
            }

            await _symptomService.DeleteAsync(id);

            return NoContent();
        }
    }
}
