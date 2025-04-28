namespace building_materials.Models.DTOs
{
    public class ProvenanceDTO
    {
        public int IdProvenance { get; set; }
        public string Pays { get; set; }
        public string Region { get; set; }
        public int DistanceKm { get; set; }
    }
}
