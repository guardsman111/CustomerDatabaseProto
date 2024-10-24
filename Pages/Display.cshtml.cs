using CustomerDatabaseProto.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Storage.Json;
using Newtonsoft.Json;

namespace CustomerDatabaseProto.Pages
{
    public class DisplayModel : PageModel
    {
        private readonly ILogger<DisplayModel> _logger;

        public DisplayModel(ILogger<DisplayModel> logger)
        {
            _logger = logger;
        }

        public List<CustomerItem> data = new List<CustomerItem>();

        public async Task OnGet()
        {
            using (var httpClient = new HttpClient())
            {
                using (HttpResponseMessage response = await httpClient.GetAsync("https://localhost:7225/api/Values"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    data = JsonConvert.DeserializeObject<List<CustomerItem>>(apiResponse);
                };
            }

            return;
        }
    }
}
