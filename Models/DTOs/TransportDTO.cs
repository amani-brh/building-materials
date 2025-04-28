namespace building_materials.Models.DTOs
{
    public class TransportDTO
    {
        public int IdTransport { get; set; }
        public string MoyenTransportNom { get; set; }
        public int DistanceKm { get; set; }
        public double EmissionCO2 { get; set; }
        public double FacteurCO2 { get; set; }
    }
}
