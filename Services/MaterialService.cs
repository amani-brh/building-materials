using building_materials.Data;
using building_materials.Models;
using Microsoft.EntityFrameworkCore;


namespace building_materials.Services
{
    public interface IMaterialService
    {
        Task<IEnumerable<MaterialDTO>> GetAllMaterialsAsync();
        Task<MaterialDTO> GetMaterialByIdAsync(int id);
        Task<IEnumerable<MaterialDTO>> SearchMaterialsByNameAsync(string name);
        Task<Dictionary<string, double>> GetMaterialStatisticsAsync(int id);
    }

    public class MaterialService(BuildingMaterialsContext context) : IMaterialService
    {
        private readonly BuildingMaterialsContext _context = context;

        public async Task<IEnumerable<MaterialDTO>> GetAllMaterialsAsync()
        {
            var materials = await _context.Materiaux
                .Include(m => m.Famille)
                    .ThenInclude(f => f.Type)
                .Include(m => m.Provenance)
                .Include(m => m.Producteur)
                //.Include(m => m.CaracteristiqueEnvironnementales)
                .Include(m => m.Transports)
                    .ThenInclude(t => t.MoyenTransport)
                .ToListAsync();

            return materials.Select(m => MapToDTO(m)).ToList();
        }

        public async Task<MaterialDTO> GetMaterialByIdAsync(int id)
        {
            var material = await _context.Materiaux
                .Include(m => m.Famille)
                    .ThenInclude(f => f.Type)
                .Include(m => m.Provenance)
                .Include(m => m.Producteur)
                .Include(m => m.CaracteristiqueEnvironnementales)
                .Include(m => m.Transports)
                    .ThenInclude(t => t.MoyenTransport)
                .FirstOrDefaultAsync(m => m.IdMateriau == id);

            return material != null ? MapToDTO(material) : null;
        }

        public async Task<IEnumerable<MaterialDTO>> SearchMaterialsByNameAsync(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return await GetAllMaterialsAsync();

            var materials = await _context.Materiaux
                .Include(m => m.Famille)
                    .ThenInclude(f => f.Type)
                .Include(m => m.Provenance)
                .Include(m => m.Producteur)
                .Include(m => m.CaracteristiqueEnvironnementales)
                .Include(m => m.Transports)
                    .ThenInclude(t => t.MoyenTransport)
                .Where(m => m.Nom.Contains(name))
                .ToListAsync();

            return materials.Select(m => MapToDTO(m)).ToList();
        }

        public async Task<Dictionary<string, double>> GetMaterialStatisticsAsync(int id)
        {
            var material = await _context.Materiaux
                .Include(m => m.CaracteristiqueEnvironnementales)
                .Include(m => m.Transports)
                .FirstOrDefaultAsync(m => m.IdMateriau == id);

            if (material == null)
                return null;

            var stats = new Dictionary<string, double>
            {
                { "EmissionCO2Production", material.CaracteristiqueEnvironnementales.FirstOrDefault()?.EmissionCO2 ?? 0 },
                { "EmissionCO2Transport", material.Transports.Sum(t => t.EmissionCO2) },
                { "EmissionCO2Totale", (material.CaracteristiqueEnvironnementales.FirstOrDefault()?.EmissionCO2 ?? 0) + material.Transports.Sum(t => t.EmissionCO2) },
                { "PollutionEau", material.CaracteristiqueEnvironnementales.FirstOrDefault()?.PollutionEau ?? 0 },
                { "PollutionAir", material.CaracteristiqueEnvironnementales.FirstOrDefault()?.PollutionAir ?? 0 },
                { "ConsommationEau", material.CaracteristiqueEnvironnementales.FirstOrDefault()?.ConsommationEau ?? 0 },
                { "DistanceTotale", material.Transports.Sum(t => t.DistanceKm) }
            };

            return stats;
        }

        private MaterialDTO MapToDTO(Materiau material)
        {
            var caracteristique = material.CaracteristiqueEnvironnementales?.FirstOrDefault();
            var transportEmissions = material.Transports?.Sum(t => t.EmissionCO2) ?? 0;

            return new MaterialDTO
            {
                IdMateriau = material.IdMateriau,
                Nom = material.Nom,
                Origine = material.Origine,
                FamilleNom = material.Famille?.NomFamille,
                TypeNom = material.Famille?.Type?.NomType,

                Provenance = material.Provenance != null ? new ProvenanceDTO
                {
                    IdProvenance = material.Provenance.IdProvenance,
                    Pays = material.Provenance.Pays,
                    Region = material.Provenance.Region,
                    DistanceKm = material.Provenance.DistanceKm
                } : null,

                Producteur = material.Producteur != null ? new ProducteurDTO
                {
                    IdProducteur = material.Producteur.IdProducteur,
                    Nom = material.Producteur.Nom,
                    LieuProduction = material.Producteur.LieuProduction
                } : null,

                Caracteristiques = caracteristique != null ? new CaracteristiqueEnvironnementaleDTO
                {
                    IdCaract = caracteristique.IdCaract,
                    EmissionCO2 = caracteristique.EmissionCO2,
                    PollutionEau = caracteristique.PollutionEau,
                    PollutionAir = caracteristique.PollutionAir,
                    ConsommationEau = caracteristique.ConsommationEau,
                    UniteFonctionnelle = caracteristique.UniteFonctionnelle,
                    UtilisationNetteEauDouce = caracteristique.UtilisationNetteEauDouce
                } : null,

                Transports = material.Transports?.Select(t => new TransportDTO
                {
                    IdTransport = t.IdTransport,
                    MoyenTransportNom = t.MoyenTransport?.Nom,
                    DistanceKm = t.DistanceKm,
                    EmissionCO2 = t.EmissionCO2,
                    FacteurCO2 = t.MoyenTransport?.FacteurCO2 ?? 0
                }).ToList() ?? new List<TransportDTO>(),

                EmissionCO2Totale = (caracteristique?.EmissionCO2 ?? 0) + transportEmissions,
                DistanceTotale = material.Transports?.Sum(t => t.DistanceKm) ?? 0
            };
        }


    }
}