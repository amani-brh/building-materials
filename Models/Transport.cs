namespace building_materials.Models
{
    public class Transport
    {
        public int IdTransport { get; set; }
        public int IdMateriau { get; set; }
        public int IdMoyenTransport { get; set; }
        public int DistanceKm { get; set; }
        public double EmissionCO2 { get; set; }

        public Materiau Materiau { get; set; }
        public FacteurTransport MoyenTransport { get; set; }
    }

}
