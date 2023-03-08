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

        // IEmployeeClientRepository Injected in Constrctor ,
        // IEmployeeClientRepository registered in App.xaml.cs using Microsoft.Extensions.DependencyInjection

        public MainWindowViewModel(IEmployeeClientRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
            Content = Detail = new DetailViewModel(employeeRepository);
        }


        public DetailViewModel Detail;

        // Content binded with MainWindow , it will navigate to corresponding VIEW

        public ViewModelBase Content
        {
            get => content;
            private set => this.RaiseAndSetIfChanged(ref content, value);
        }

        // When click Added button below method get executed
        // It method will navigate to AddorUpdateView VIEW 
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


        // When click Edit button binded this method to DetailView
        // It method will update employee to Database using ASP.NET IN MEMORY DATABSE 

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

        // When click Delete button binded this method to DetailView
        // It method will Delete employee from Database using ASP.NET IN MEMORY DATABSE 

        public async void DeleteItem(Employee employee)
        {
            await _employeeRepository.Remove(employee);
            Detail.GetEmployees();
        }
    }
}