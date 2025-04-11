namespace building_materials.Models
{
    public class CaracteristiqueEnvironnementale
    {
        public int IdCaract { get; set; }
        public int IdMateriau { get; set; }
        public double EmissionCO2 { get; set; }
        public double PollutionEau { get; set; }
        public double PollutionAir { get; set; }
        public double ConsommationEau { get; set; }
        public string UniteFonctionnelle { get; set; }
        public string UtilisationNetteEauDouce { get; set; }

        public Materiau Materiau { get; set; }
    }

}
