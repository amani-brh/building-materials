using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace building_materials.Models
{
    public class Materiau
    {
        [Key]
        public int IdMateriau { get; set; }

        [Required]
        [StringLength(100)]
        public string? Nom { get; set; }

        [Required]
        [StringLength(100)]
        public string? Origine { get; set; }

        [Required]
        public int IdFamille { get; set; }

        [Required]
        public int IdProvenance { get; set; }

        [Required]
        public int IdProducteur { get; set; }

        [ForeignKey("IdFamille")]
        public Famille Famille { get; set; }

        [ForeignKey("IdProvenance")]
        public Provenance Provenance { get; set; }

        [ForeignKey("IdProducteur")]
        public Producteur Producteur { get; set; }

        public ICollection<Transport> Transports { get; set; } = [];
        public ICollection<CaracteristiqueEnvironnementale> CaracteristiqueEnvironnementales { get; set; } = [];

    }

}
