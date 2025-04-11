using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace building_materials.Data
{
    public class BuildingMaterialsContextFactory : IDesignTimeDbContextFactory<BuildingMaterialsContext>
    {
        public BuildingMaterialsContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<BuildingMaterialsContext>();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));

            return new BuildingMaterialsContext(optionsBuilder.Options);
        }
    }
}
