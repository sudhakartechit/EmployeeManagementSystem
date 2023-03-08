using EmployeeManagementSystem.ViewModels;
using EmployeeRepository;
using FakeItEasy;
using FluentAssertions;

namespace EmployeeUnitTest
{
    [TestClass]
    public class ViewModelsTest
    {
        [TestMethod]
        public void Constructor_NullRepository_ShouldThrow()
        {
            Action act = () => new MainWindowViewModel(null);
            act.Should().Throw<ArgumentNullException>()
                .Where(e => e.Message.Contains("EmployeeClientRepository"));
        }

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
    }
}