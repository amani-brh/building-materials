using System.ComponentModel.DataAnnotations;

namespace building_materials.Models
{
    public class Producteur
    {
        [Key]
        public int IdProducteur { get; set; }

        [Required]
        [StringLength(100)]
        public string? Nom { get; set; }

        [Required]
        [StringLength(200)]
        public string? LieuProduction { get; set; }
    }

}
