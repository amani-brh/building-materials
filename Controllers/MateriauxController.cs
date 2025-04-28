using building_materials.Services;
using Microsoft.AspNetCore.Mvc;

namespace building_materials.Controllers
{
    public class MateriauxController : Controller
    {
        private readonly IMaterialService _materialService;

        public MateriauxController(IMaterialService materialService)
        {
            _materialService = materialService;
        }

        // GET: Materiaux - Main view
        public IActionResult Index()
        {
            return View();
        }

        // API endpoints

        // GET: Materiaux/List - Get all materials (API)
        [HttpGet("Materiaux/List")]
        public async Task<IActionResult> GetAll()
        {
            var materials = await _materialService.GetAllMaterialsAsync();
            return Ok(materials);
        }

        // GET: Materiaux/Details/5 - Get material by ID (API)
        [HttpGet("Materiaux/Details/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var material = await _materialService.GetMaterialByIdAsync(id);
            if (material == null) return NotFound();
            return Ok(material);
        }

        // GET: Materiaux/Search?name=value - Search materials by name (API)
        [HttpGet("Materiaux/Search")]
        public async Task<IActionResult> Search([FromQuery] string name)
        {
            var materials = await _materialService.SearchMaterialsByNameAsync(name);
            return Ok(materials);
        }

        // GET: Materiaux/Statistics/5 - Get material statistics (API)
        [HttpGet("Materiaux/Statistics/{id}")]
        public async Task<IActionResult> GetStatistics(int id)
        {
            var stats = await _materialService.GetMaterialStatisticsAsync(id);
            if (stats == null) return NotFound();
            return Ok(stats);
        }

        // GET: Materiaux/Compare?ids=1,2,3 - Compare multiple materials (New API)
        [HttpGet("Materiaux/Compare")]
        public async Task<IActionResult> CompareMaterials([FromQuery] string ids)
        {
            if (string.IsNullOrEmpty(ids))
                return BadRequest("No material IDs provided");

            try
            {
                var idList = ids.Split(',').Select(int.Parse).ToList();
                var materials = await _materialService.GetMaterialsComparisonAsync(idList);
                return Ok(materials);
            }
            catch (FormatException)
            {
                return BadRequest("Invalid ID format");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
 
    }
}