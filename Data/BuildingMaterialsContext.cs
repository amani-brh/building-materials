using building_materials.Models;
using Microsoft.EntityFrameworkCore;

namespace building_materials.Data
{
    public class BuildingMaterialsContext : DbContext
    {
        public BuildingMaterialsContext(DbContextOptions<BuildingMaterialsContext> options) : base(options)
        {
        }

        public DbSet<FacteurTransport> FacteurTransports { get; set; }
        public DbSet<Materiau> Materiaux { get; set; }
        public DbSet<Famille> Familles { get; set; }
        public DbSet<MaterialType> Types { get; set; }
        public DbSet<CaracteristiqueEnvironnementale> CaracteristiqueEnvironnementales { get; set; }
        public DbSet<Provenance> Provenances { get; set; }
        public DbSet<Producteur> Producteurs { get; set; }
        public DbSet<Transport> Transports { get; set; }
    }
}
