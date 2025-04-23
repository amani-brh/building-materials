using building_materials.Services;
using Microsoft.AspNetCore.Mvc;

namespace building_materials.Controllers
{
    [ApiController]
    [Route("Materiaux")]
    public class MateriauxController(IMaterialService materialService) : ControllerBase
    {
        private readonly IMaterialService _materialService = materialService;

     
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var materials = await _materialService.GetAllMaterialsAsync();
            return Ok(materials);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var material = await _materialService.GetMaterialByIdAsync(id);
            if (material == null) return NotFound();
            return Ok(material);
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] string name)
        {
            var materials = await _materialService.SearchMaterialsByNameAsync(name);
            return Ok(materials);
        }

        [HttpGet("{id}/statistics")]
        public async Task<IActionResult> GetStatistics(int id)
        {
            var stats = await _materialService.GetMaterialStatisticsAsync(id);
            if (stats == null) return NotFound();
            return Ok(stats);
        }
    }
}
