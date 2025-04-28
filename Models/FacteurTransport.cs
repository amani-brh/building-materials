using System.ComponentModel.DataAnnotations;

namespace building_materials.Models
{
    public class FacteurTransport
    {
        [Key]
        public int IdMoyenTransport { get; set; }
        [Required]
        [StringLength(100)]
        public string Nom { get; set; } = string.Empty;
        [Required]
        public double FacteurCO2 { get; set; }
    }
}
