using Microsoft.AspNetCore.Mvc;
using PersonalizedHealthCenter.Models;
using PersonalizedHealthCenter.Services;
using System.Threading.Tasks;

namespace PersonalizedHealthCenter.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HealthProfileController : Controller
    {
        private readonly HealthProfileService _healthProfileService;

        public HealthProfileController(HealthProfileService healthProfileService)
        {
            _healthProfileService = healthProfileService;
        }

        [HttpGet]
        public async Task<List<HealthProfile>> Get()
        {
            return await _healthProfileService.GetAsync();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var healthProfile = await _healthProfileService.GetByIdAsync(id);

            if (healthProfile == null)
            {
                return NotFound();
            }

            return Ok(healthProfile);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] HealthProfile healthProfile)
        {
            await _healthProfileService.CreateAsync(healthProfile);
            return CreatedAtAction(nameof(Get), new { id = healthProfile.Id }, healthProfile);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody] HealthProfile updatedHealthProfile)
        {
            var existingHealthProfile = await _healthProfileService.GetByIdAsync(id);

            if (existingHealthProfile == null)
            {
                return NotFound();
            }

            await _healthProfileService.UpdateAsync(id, updatedHealthProfile);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var healthProfile = await _healthProfileService.GetByIdAsync(id);

            if (healthProfile == null)
            {
                return NotFound();
            }

            await _healthProfileService.DeleteAsync(id);

            return NoContent();
        }
    }
}
