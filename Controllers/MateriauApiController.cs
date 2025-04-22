using building_materials.Data;
using building_materials.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace building_materials.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class MateriauApiController(BuildingMaterialsContext context) : ControllerBase
    {
        private readonly BuildingMaterialsContext _context = context;

        // GET: api/MateriauApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<object>> GetMaterialWithDetails(int id)
        {
            var material = await _context.Materiaux
                .Include(m => m.Famille)
                    .ThenInclude(f => f.Type)
                .Include(m => m.Producteur)
                .Include(m => m.Provenance)
                .FirstOrDefaultAsync(m => m.IdMateriau == id);

            if (material == null)
            {
                return NotFound();
            }

            // Get environmental characteristics
            var characteristics = await _context.CaracteristiqueEnvironnementales
                .Where(c => c.IdMateriau == id)
                .ToListAsync();

            // Get transport information
            var transportInfo = await _context.Transports
                .Include(t => t.MoyenTransport)
                .Where(t => t.IdMateriau == id)
                .ToListAsync();

            // Construct comprehensive material data
            var materialData = new
            {
                Material = material,
                EnvironmentalCharacteristics = characteristics,
                TransportInformation = transportInfo
            };

            return materialData;
        }

        // GET: api/MateriauApi/search?name={name}
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<Materiau>>> SearchMaterialsByName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return BadRequest("Search name parameter is required");
            }

            var materials = await _context.Materiaux
                .Include(m => m.Famille)
                .Include(m => m.Producteur)
                .Include(m => m.Provenance)
                .Where(m => m.Nom.Contains(name))
                .ToListAsync();

            if (!materials.Any())
            {
                return NotFound("No materials found with the specified name");
            }

            return materials;
        }

        // GET: api/MateriauApi/all-materials
        [HttpGet("all-materials")]
        public async Task<ActionResult<IEnumerable<Materiau>>> GetAllMaterials()
        {
            return await _context.Materiaux
                .Include(m => m.Famille)
                .Include(m => m.Producteur)
                .Include(m => m.Provenance)
                .ToListAsync();
        }

        // GET: api/MateriauApi/environmental/5
        [HttpGet("environmental/{id}")]
        public async Task<ActionResult<IEnumerable<CaracteristiqueEnvironnementale>>> GetEnvironmentalCharacteristics(int id)
        {
            var characteristics = await _context.CaracteristiqueEnvironnementales
                .Where(c => c.IdMateriau == id)
                .ToListAsync();

            if (!characteristics.Any())
            {
                return NotFound("No environmental characteristics found for this material");
            }

            return characteristics;
        }

        // GET: api/MateriauApi/transport/5
        [HttpGet("transport/{id}")]
        public async Task<ActionResult<IEnumerable<object>>> GetTransportInformation(int id)
        {
            var transportInfo = await _context.Transports
                .Include(t => t.MoyenTransport)
                .Where(t => t.IdMateriau == id)
                .Select(t => new
                {
                    Transport = t,
                    MoyenTransport = t.MoyenTransport
                })
                .ToListAsync();

            if (!transportInfo.Any())
            {
                return NotFound("No transport information found for this material");
            }

            return transportInfo;
        }

        // GET: api/MateriauApi/by-family/{familyId}
        [HttpGet("by-family/{familyId}")]
        public async Task<ActionResult<IEnumerable<Materiau>>> GetMaterialsByFamily(int familyId)
        {
            var materials = await _context.Materiaux
                .Include(m => m.Famille)
                .Include(m => m.Producteur)
                .Include(m => m.Provenance)
                .Where(m => m.IdFamille == familyId)
                .ToListAsync();

            if (!materials.Any())
            {
                return NotFound("No materials found for this family");
            }

            return materials;
        }

        // GET: api/MateriauApi/by-country/{country}
        [HttpGet("by-country/{country}")]
        public async Task<ActionResult<IEnumerable<Materiau>>> GetMaterialsByCountry(string country)
        {
            var materials = await _context.Materiaux
                .Include(m => m.Famille)
                .Include(m => m.Producteur)
                .Include(m => m.Provenance)
                .Where(m => m.Provenance.Pays == country)
                .ToListAsync();

            if (!materials.Any())
            {
                return NotFound($"No materials found from {country}");
            }

            return materials;
        }
    }
}
