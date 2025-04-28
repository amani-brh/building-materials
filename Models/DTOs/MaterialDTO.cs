namespace building_materials.Models.DTOs
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
}
