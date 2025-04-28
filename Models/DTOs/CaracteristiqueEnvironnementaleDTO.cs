namespace building_materials.Models.DTOs
{
    public class CaracteristiqueEnvironnementaleDTO
    {
        public int IdCaract { get; set; }
        public double EmissionCO2 { get; set; }
        public double PollutionEau { get; set; }
        public double PollutionAir { get; set; }
        public double ConsommationEau { get; set; }
        public string UniteFonctionnelle { get; set; } = string.Empty;
        public double UtilisationNetteEauDouce { get; set; }
    }
}
