using EmployeeRepository;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Concurrency;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.ViewModels
{
    public class DetailViewModel : ViewModelBase
    {
        private readonly IEmployeeClientRepository _employeeClientRepository;

        private Employee selectedEmployee;

        public DetailViewModel(IEmployeeClientRepository employeeClientRepository)
        {
            _employeeClientRepository = employeeClientRepository;
            RxApp.MainThreadScheduler.Schedule(GetEmployees);
        }

        public async void GetEmployees()
        {
            _employees =await _employeeClientRepository.GetEmployees();
            this.RaisePropertyChanged(nameof(Employees));
        }

        //It's set SelectedEmployee from DetailView
        public Employee SelectedEmployee
        {
            get => selectedEmployee;
            set => this.RaiseAndSetIfChanged(ref selectedEmployee, value);
        }

        // When click Search button binded this method to DetailView
        // It method will search employees in the Database using ASP.NET IN MEMORY DATABSE 

        public async void Search(string searchTerm)
        {
            _employees = await _employeeClientRepository.GetEmployees(searchTerm);
            this.RaisePropertyChanged(nameof(Employees));
        }


        private IEnumerable<Employee> _employees;

        public IEnumerable<Employee> Employees => _employees;
    }
}
