namespace building_materials.Models
{
    public class MaterialDTO
    {
        // Material basic info
        public int IdMateriau { get; set; }
        public string Nom { get; set; }
        public string Origine { get; set; }

        // Related entities info
        public string FamilleNom { get; set; }
        public string TypeNom { get; set; }
        public ProvenanceDTO Provenance { get; set; }
        public ProducteurDTO Producteur { get; set; }
        public CaracteristiqueEnvironnementaleDTO Caracteristiques { get; set; }
        public List<TransportDTO> Transports { get; set; } = new List<TransportDTO>();

        // Calculated statistics
        public double EmissionCO2Totale { get; set; }
        public int DistanceTotale { get; set; } // Sum of transport distances
    }

    public class ProvenanceDTO
    {
        public int IdProvenance { get; set; }
        public string Pays { get; set; }
        public string Region { get; set; }
        public int DistanceKm { get; set; }
    }

    public class ProducteurDTO
    {
        public int IdProducteur { get; set; }
        public string Nom { get; set; }
        public string LieuProduction { get; set; }
    }

    public class CaracteristiqueEnvironnementaleDTO
    {
        public int IdCaract { get; set; }
        public double EmissionCO2 { get; set; }
        public double PollutionEau { get; set; }
        public double PollutionAir { get; set; }
        public double ConsommationEau { get; set; }
        public string UniteFonctionnelle { get; set; }
        public double UtilisationNetteEauDouce { get; set; }

        
    }

    public class TransportDTO
    {
        public int IdTransport { get; set; }
        public string MoyenTransportNom { get; set; }
        public int DistanceKm { get; set; }
        public double EmissionCO2 { get; set; }
        public double FacteurCO2 { get; set; }
    }
}
