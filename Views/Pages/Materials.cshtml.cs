using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using building_materials.Models;

public class MaterialsModel : PageModel
{
    private readonly HttpClient _http;

    public MaterialsModel(IHttpClientFactory clientFactory)
    {
        _http = clientFactory.CreateClient("ApiClient"); // Configure ApiClient in Program.cs or Startup.cs
    }

    public List<Materiau> Materials { get; set; } = new();
    [BindProperty(SupportsGet = true)]
    public string SearchTerm { get; set; }
    [BindProperty]
    public int MaterialId { get; set; }

    public Materiau SelectedMaterial { get; set; }
    public string ErrorMessage { get; set; }

    public async Task OnGetAsync()
    {
        try
        {
            if (!string.IsNullOrWhiteSpace(SearchTerm))
            {
                Materials = await _http.GetFromJsonAsync<List<Materiau>>($"api/MateriauApi/search?name={Uri.EscapeDataString(SearchTerm)}");
            }
            else
            {
                Materials = await _http.GetFromJsonAsync<List<Materiau>>("api/MateriauApi/all-materials");
            }
        }
        catch (Exception ex)
        {
            ErrorMessage = $"Failed to load materials: {ex.Message}";
        }
    }

    public async Task<IActionResult> OnPostAsync()
    {
        try
        {
            SelectedMaterial = await _http.GetFromJsonAsync<Materiau>($"api/MateriauApi/{MaterialId}");
            await OnGetAsync(); // Reload list
        }
        catch (Exception ex)
        {
            ErrorMessage = $"Failed to load details: {ex.Message}";
        }

        return Page();
    }
}
