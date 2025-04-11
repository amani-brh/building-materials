namespace building_materials.Models
{
    public class Famille
    {
        public int IdFamille { get; set; }
        public string NomFamille { get; set; }
        public int IdType { get; set; }

        public Type Type { get; set; }
    }

}
