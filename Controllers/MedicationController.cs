using Microsoft.AspNetCore.Mvc;
using PersonalizedHealthCenter.Models;
using PersonalizedHealthCenter.Services;
using System.Threading.Tasks;

namespace PersonalizedHealthCenter.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MedicationController : Controller
    {
        private readonly MedicationService _medicationService;

        public MedicationController(MedicationService medicationService)
        {
            _medicationService = medicationService;
        }

        [HttpGet]
        public async Task<List<Medication>> Get()
        {
            return await _medicationService.GetAsync();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var medication = await _medicationService.GetByIdAsync(id);

            if (medication == null)
            {
                return NotFound();
            }

            return Ok(medication);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Medication medication)
        {
            await _medicationService.CreateAsync(medication);
            return CreatedAtAction(nameof(Get), new { id = medication.Id }, medication);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody] Medication updatedMedication)
        {
            var existingMedication = await _medicationService.GetByIdAsync(id);

            if (existingMedication == null)
            {
                return NotFound();
            }

            await _medicationService.UpdateAsync(id, updatedMedication);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var medication = await _medicationService.GetByIdAsync(id);

            if (medication == null)
            {
                return NotFound();
            }

            await _medicationService.DeleteAsync(id);

            return NoContent();
        }
    }
}
