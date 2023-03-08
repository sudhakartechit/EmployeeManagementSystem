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


        private HttpClient GetHttpClient()
        {
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
            return new HttpClient(clientHandler);
        }

        // This method add employes in in memory database using ASP.NET MVC WEB API
        public async Task<bool> Add(Employee employee)
        {
            JsonContent jsonContent = JsonContent.Create(employee);

            var response = await GetHttpClient().PostAsync(BaseURL, jsonContent);
            if(response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
            }
            return false;
        }

        // This method get all employes in  memory database using ASP.NET MVC WEB API

        public async Task<IEnumerable<Employee>> GetEmployees()
        {

            var resonse = await GetHttpClient().GetAsync(BaseURL);

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

            var resonse = await GetHttpClient().GetAsync(BaseURL + "/" + search);

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
            var resonse = await GetHttpClient().DeleteAsync(BaseURL +"/"+ employee.Id);

            if (resonse.IsSuccessStatusCode)
            {
                var content = await resonse.Content.ReadAsStringAsync();
            }
            return false;
        }

        // This method get Update employes in  memory database using ASP.NET MVC WEB API
        public async Task<bool> Update(Employee employee)
        {
            JsonContent jsonContent = JsonContent.Create(employee);

            var response = await GetHttpClient().PutAsync(BaseURL + "/" + employee.Id, jsonContent);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
            }
            return false;
        }
    }
}
