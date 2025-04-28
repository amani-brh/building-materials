using System.ComponentModel.DataAnnotations;

namespace building_materials.Models
{
    public class Provenance
    {
        [Key]
        public int IdProvenance { get; set; }
        [Required]
        [StringLength(100)]
        public string Pays { get; set; } = string.Empty;
        [Required]
        [StringLength(100)]
        public string Region { get; set; } = string.Empty;
        [Required]
        public int DistanceKm { get; set; }
    }

}
