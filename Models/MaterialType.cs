using System.ComponentModel.DataAnnotations;

namespace building_materials.Models
{
    public class MaterialType
    {
        [Key]
        public int IdType { get; set; }

        [Required]
        [StringLength(100)]
        public string? NomType { get; set; }
    }

}
