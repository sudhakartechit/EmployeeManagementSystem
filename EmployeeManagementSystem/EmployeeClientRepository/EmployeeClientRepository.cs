using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeRepository
{
    public class EmployeeClientRepository : IEmployeeClientRepository
    {
        public static string BaseURI = @"https://localhost:7127/";

        public static string BaseURL = @"https://localhost:7127/api/Employee";

        public EmployeeClientRepository()
        {
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
            HttpClient = new HttpClient(clientHandler);
        }

        public HttpClient HttpClient { get; set; }

        // This method add employes in in memory database using ASP.NET MVC WEB API
        public async Task<bool> Add(Employee employee)
        {
            JsonContent jsonContent = JsonContent.Create(employee);

            var response = await HttpClient.PostAsync(BaseURL, jsonContent);
            if(response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
            }
            return response.IsSuccessStatusCode;
        }

        // This method get all employes in  memory database using ASP.NET MVC WEB API

        public async Task<IEnumerable<Employee>> GetEmployees()
        {

            var resonse = await HttpClient.GetAsync(BaseURL);

            if(resonse.IsSuccessStatusCode)
            {
                var content= await resonse.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<Employee>>(content);
            }
            return null;

        }

        // This method get all employes contains searchterm in memory database using ASP.NET MVC WEB API

        public async Task<IEnumerable<Employee>> GetEmployees(string search)
        {

            var resonse = await HttpClient.GetAsync(BaseURL + "/" + search);

            if (resonse.IsSuccessStatusCode)
            {
                var content = await resonse.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<Employee>>(content);
            }
            return null;

        }

        // This method get Delete employes in  memory database using ASP.NET MVC WEB API

        public async Task<bool> Remove(Employee employee)
        {
            var resonse = await HttpClient.DeleteAsync(BaseURL +"/"+ employee.Id);

            if (resonse.IsSuccessStatusCode)
            {
                var content = await resonse.Content.ReadAsStringAsync();
            }
            return resonse.IsSuccessStatusCode;
        }

        // This method get Update employes in  memory database using ASP.NET MVC WEB API
        public async Task<bool> Update(Employee employee)
        {
            JsonContent jsonContent = JsonContent.Create(employee);

            var response = await HttpClient.PutAsync(BaseURL + "/" + employee.Id, jsonContent);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
            }
            return response.IsSuccessStatusCode;
        }
    }
}
