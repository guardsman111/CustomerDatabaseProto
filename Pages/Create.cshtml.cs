using CustomerDatabaseProto.Controllers;
using CustomerDatabaseProto.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CustomerDatabaseProto.Pages
{
    public class CreateModel : PageModel
    {
        private readonly ILogger<CreateModel> _logger;

        public CreateModel(ILogger<CreateModel> logger)
        {
            _logger = logger;
        }

        public string responseText;

        public async Task OnPostAsync(CustomerItem newItem)
        {
            using (var httpClient = new HttpClient())
            {
                using (HttpResponseMessage response = await httpClient.PostAsJsonAsync("https://localhost:7225/api/Values", newItem))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        responseText = "Successfully created new customer!";
                    }
                    else 
                    { 
                        responseText = "Unable to create new customer for some reason.";
                    }
                };
            }

            return;
        }
    }
}
