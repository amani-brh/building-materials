namespace building_materials.Models.DTOs
{
    public class MaterialComparison
    {
        public MaterialDTO Material { get; set; }
        public Dictionary<string, double> Statistics { get; set; }
    }
}