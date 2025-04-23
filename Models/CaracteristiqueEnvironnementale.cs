using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace building_materials.Models
{
    public class CaracteristiqueEnvironnementale
    {
        [Key]
        public int IdCaract { get; set; }

        [Required]
        public int IdMateriau { get; set; }

        [Required]
        public double EmissionCO2 { get; set; }

        [Required]
        public double PollutionEau { get; set; }

        [Required]
        public double PollutionAir { get; set; }

        [Required]
        public double ConsommationEau { get; set; }

        [Required]
        [StringLength(200)]
        public string UniteFonctionnelle { get; set; }

        [Required]
        [StringLength(200)]
        public double UtilisationNetteEauDouce { get; set; }

        [ForeignKey("IdMateriau")]
        public Materiau Materiau { get; set; }
    }

}
