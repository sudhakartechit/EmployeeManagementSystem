using EmployeeManagementSystem.ViewModels;
using EmployeeRepository;
using FakeItEasy;
using FluentAssertions;
using Newtonsoft.Json;
using RichardSzalay.MockHttp;

namespace EmployeeUnitTest
{
    [TestClass]
    public class ViewModelsTest
    {
        [TestMethod]
        public void DetailViewModel_GetEmployees_ShouldReturnMatch()
        {
            var emp = new List<Employee>()
            {
                new Employee{Name= "Mike",Department="Development"},
                new Employee{Name= "John",Department="Testing"}
            };
            var repo = A.Fake<IEmployeeClientRepository>();
            A.CallTo(() => repo.GetEmployees()).Returns(emp);
            var vm = new DetailViewModel(repo);
            vm.GetEmployees();
            vm.Employees.Count().Should().Be(2);
        }

        [TestMethod]
        public async Task EmployeeClientRepository_GetEmployees()
        {
            var mockHttp = new MockHttpMessageHandler();
            EmployeeClientRepository employeeClientRepository = new();
            // Setup a respond for the user api (including a wildcard in the URL)
            mockHttp.When(EmployeeClientRepository.BaseURL)
                    .Respond("application/json", "[{\"id\":1,\"companyName\":\"Alfreds Futterkiste\",\"name\":\"Maria Anders\",\"department\":\"Sales Representative\",\"address\":\"Obere Str. 57\",\"city\":\"Berlin\",\"postalCode\":\"12209\",\"country\":\"Germany\",\"phone\":\"030-0074321\",\"email\":\"test@gmail.com\",\"dob\":\"2019-01-06T17:16:40\"},{\"id\":6,\"companyName\":\"Blauer See Delikatessen\",\"name\":\"Hanna Moos\",\"department\":\"Sales Representative\",\"address\":\"Forsterstr. 57\",\"city\":\"Mannheim\",\"postalCode\":\"68306\",\"country\":\"Germany\",\"phone\":\"0621-08460\",\"email\":\"test@gmail.com\",\"dob\":\"2019-01-06T17:16:40\"}]\r\n"); // Respond with JSON

            employeeClientRepository.HttpClient = new HttpClient(mockHttp);
            var employees = await employeeClientRepository.GetEmployees();
            employees.Should().NotContainNulls();
        }

        [TestMethod]
        public async Task EmployeeClientRepository_AddEmployee()
        {
            var mockHttp = new MockHttpMessageHandler();
            EmployeeClientRepository employeeClientRepository = new();
            // Setup a respond for the user api (including a wildcard in the URL)
            Employee employee = new Employee { Name = "John", CompanyName = "Alfreds Futterkiste", Department = "Sales Representative" };

            var content = JsonConvert.SerializeObject(employee);
            mockHttp.When(EmployeeClientRepository.BaseURL)
                    .Respond("application/json", content);

            employeeClientRepository.HttpClient = new HttpClient(mockHttp);
            var employees = await employeeClientRepository.Add(employee);
            employees.Should();
        }

        [TestMethod]
        public async Task EmployeeClientRepository_UpdateEmployee()
        {
            var mockHttp = new MockHttpMessageHandler();
            EmployeeClientRepository employeeClientRepository = new();
            // Setup a respond for the user api (including a wildcard in the URL)
            Employee employee = new Employee { Name = "John", CompanyName = "Alfreds Futterkiste", Department = "Sales Representative" };

            var content = JsonConvert.SerializeObject(employee);
            mockHttp.When(EmployeeClientRepository.BaseURL)
                    .Respond("application/json", content);

            employeeClientRepository.HttpClient = new HttpClient(mockHttp);
            var employees = await employeeClientRepository.Update(employee);
            employees.Should();
        }

        [TestMethod]
        public async Task EmployeeClientRepository_DeleteEmployee()
        {
            var mockHttp = new MockHttpMessageHandler();
            EmployeeClientRepository employeeClientRepository = new();
            // Setup a respond for the user api (including a wildcard in the URL)
            Employee employee = new Employee { Name = "John", CompanyName = "Alfreds Futterkiste", Department = "Sales Representative" };

            var content = JsonConvert.SerializeObject(employee);
            mockHttp.When(EmployeeClientRepository.BaseURL)
                    .Respond("application/json", content);

            employeeClientRepository.HttpClient = new HttpClient(mockHttp);
            var employees = await employeeClientRepository.Remove(employee);
            employees.Should();
        }



    }
}