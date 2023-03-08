using ReactiveUI;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Reactive;
using Avalonia;
using EmployeeRepository;

namespace EmployeeManagementSystem.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        ViewModelBase content;
        private readonly IEmployeeClientRepository _employeeRepository;

        public MainWindowViewModel(IEmployeeClientRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
            Content = Detail = new DetailViewModel(employeeRepository);
        }


        public DetailViewModel Detail;

        public ViewModelBase Content
        {
            get => content;
            private set => this.RaiseAndSetIfChanged(ref content, value);
        }
        public async void AddItem()
        {
            var vm = new AddOrUpdateEmployeeViewModel();

            vm.Save.Subscribe(async empolyee =>
            {
                if (empolyee != null)
                {
                    await _employeeRepository.Add(empolyee);
                    Detail.GetEmployees();
                    Content = Detail;
                }
            });

            vm.Cancel.Subscribe(empolyee =>
            {
                Content = Detail;
            });

            Content = vm;

        }



        public async void EditItem(Employee employee)
        {
            var vm = new AddOrUpdateEmployeeViewModel(employee);

            vm.Save.Subscribe(async empolyee =>
            {
                if (empolyee != null)
                {
                    await _employeeRepository.Update(empolyee);
                    Detail.GetEmployees();
                    Content = Detail;
                }
            });

            vm.Cancel.Subscribe(empolyee =>
            {
                Content = Detail;
            });

            Content = vm;

        }

        public async void DeleteItem(Employee employee)
        {
            await _employeeRepository.Remove(employee);
            Detail.GetEmployees();
        }
    }
}