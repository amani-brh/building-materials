using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace building_materials.Models
{
    public class Famille
    {
        [Key]
        public int IdFamille { get; set; }

        [Required]
        [StringLength(100)]
        public string? NomFamille { get; set; }

        [Required]
        public int IdType { get; set; }

        [ForeignKey("IdType")]
        public MaterialType Type { get; set; }
    }

}
