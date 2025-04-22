using System.Text.Json;
using building_materials.Models;

namespace building_materials.Services
{
    public class MaterialService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<MaterialService> _logger;
        private readonly JsonSerializerOptions _jsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        public MaterialService(HttpClient httpClient, ILogger<MaterialService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<List<Materiau>> GetAllMaterialsAsync()
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<List<Materiau>>("api/MateriauApi/all-materials", _jsonOptions);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching all materials");
                throw;
            }
        }

        public async Task<List<Materiau>> SearchMaterialsByNameAsync(string searchTerm)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<List<Materiau>>($"api/MateriauApi/search?name={Uri.EscapeDataString(searchTerm)}", _jsonOptions);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error searching materials by name: {SearchTerm}", searchTerm);
                throw;
            }
        }

        public async Task<MaterialDetails> GetMaterialDetailsAsync(int materialId)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<MaterialDetails>($"api/MateriauApi/{materialId}", _jsonOptions);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching material details for ID: {MaterialId}", materialId);
                throw;
            }
        }

        public async Task<List<Materiau>> GetMaterialsByFamilyAsync(int familyId)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<List<Materiau>>($"api/MateriauApi/by-family/{familyId}", _jsonOptions);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching materials by family ID: {FamilyId}", familyId);
                throw;
            }
        }

        public async Task<List<Materiau>> GetMaterialsByCountryAsync(string country)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<List<Materiau>>($"api/MateriauApi/by-country/{Uri.EscapeDataString(country)}", _jsonOptions);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching materials by country: {Country}", country);
                throw;
            }
        }
    }

    public class MaterialDetails
    {
        public Materiau Material { get; set; }
        public List<CaracteristiqueEnvironnementale> EnvironmentalCharacteristics { get; set; }
        public List<Transport> TransportInformation { get; set; }
    }
}