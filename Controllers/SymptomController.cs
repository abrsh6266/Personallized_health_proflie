using Microsoft.AspNetCore.Mvc;
using PersonalizedHealthCenter.Models;
using PersonalizedHealthCenter.Services;
using System.Collections.Generic;
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
            return await _symptomService.GetAllAsync();
        }

        [HttpGet("{name}")]
        public async Task<IActionResult> GetByName(string name)
        {
            var symptoms = await _symptomService.GetByNameAsync(name);

            if (symptoms == null || symptoms.Count == 0)
            {
                return NotFound();
            }

            return Ok(symptoms);
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Symptom symptom)
        {
            await _symptomService.CreateAsync(symptom);
            return CreatedAtAction(nameof(Get), new { id = symptom.Id }, symptom);
        }
    }
}
