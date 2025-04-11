namespace building_materials.Models
{
    public class Materiau
    {
        public int IdMateriau { get; set; }
        public string Nom { get; set; }
        public string Origine { get; set; }
        public int IdFamille { get; set; }
        public int IdProvenance { get; set; }
        public int IdProducteur { get; set; }

        public Famille Famille { get; set; }
        public Provenance Provenance { get; set; }
        public Producteur Producteur { get; set; }
    }

}
