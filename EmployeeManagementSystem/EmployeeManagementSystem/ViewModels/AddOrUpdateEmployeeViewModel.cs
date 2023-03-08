using Avalonia.Controls;
using Avalonia.Data;
using Avalonia.Data.Core;
using EmployeeRepository;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.ViewModels
{
    public class AddOrUpdateEmployeeViewModel : ViewModelBase
    {
        private Employee employee;

        public AddOrUpdateEmployeeViewModel()
        {
            Save = ReactiveCommand.Create(
                () => new Employee
                {
                    Id = Id,
                    CompanyName = CompanyName,
                    Name = Name,
                    Department = Department,
                    Address = Address,
                    City = City,
                    PostalCode = PostalCode,
                    Country = Country,
                    Phone = Phone,
                    Email = Email,
                    DOB = DOB
                });

            Cancel = ReactiveCommand.Create(() => { });

        }

        public AddOrUpdateEmployeeViewModel(Employee employee) : this()
        {
            Id = employee.Id;
            CompanyName = employee.CompanyName;
            Name = employee.Name;
            Department = employee.Department;
            Address = employee.Address;
            City = employee.City;
            PostalCode = employee.PostalCode;
            Country = employee.Country;
            Phone = employee.Phone;
            Email = employee.Email;
            DOB = employee.DOB;
        }

        public int Id { get; set; }
        public string CompanyName { get; set; }
        public string Name { get; set; }
        public string Department { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public DateTime DOB { get; set; }

        public ReactiveCommand<Unit, Employee> Save { get; set; }
        public ReactiveCommand<Unit, Unit> Cancel { get; set; }

        
    }
}
