using CustomerDatabaseProto.Data;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Storage.Json;
using Newtonsoft.Json;
using System.Security.Principal;

namespace CustomerDatabaseProto.Pages
{
    public class EditModel : PageModel
    {
        private readonly ILogger<EditModel> _logger;

        public EditModel(ILogger<EditModel> logger)
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

        public async Task OnPostAsync(CustomerItem item)
        {
            using (var httpClient = new HttpClient())
            {
                using (HttpResponseMessage response = await httpClient.PutAsJsonAsync("https://localhost:7225/api/Values/" + item.Id.ToString(), item))
                {
                };
            }

            Response.Redirect(Request.GetDisplayUrl());

            return;
        }
    }
}
