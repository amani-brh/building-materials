using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace building_materials.Models
{
    public class Transport
    {
        [Key]
        public int IdTransport { get; set; }

        [Required]
        public int IdMateriau { get; set; }

        [Required]
        public int IdMoyenTransport { get; set; }

        [Required]
        public int DistanceKm { get; set; }

        [Required]
        public double EmissionCO2 { get; set; }

        [ForeignKey("IdMateriau")]
        public Materiau Materiau { get; set; }

        [ForeignKey("IdMoyenTransport")]
        public FacteurTransport MoyenTransport { get; set; }
    }

}
