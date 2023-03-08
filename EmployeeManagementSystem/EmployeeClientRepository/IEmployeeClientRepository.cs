using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeRepository
{
    public interface IEmployeeClientRepository
    {
        Task<bool> Add(Employee employee);
        Task<bool> Remove(Employee employee);
        Task<bool> Update(Employee employee);
        Task<IEnumerable<Employee>> GetEmployees();
        Task<IEnumerable<Employee>> GetEmployees(string search);

    }
}
