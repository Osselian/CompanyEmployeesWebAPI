using Entities.Models;
using Shared.RequestFeatures;

namespace Contracts
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<Employee>> GetEmployeesAsync(Guid companyId, 
            EmployeeParameters employeeParameters, bool trackChange);
        Task<Employee> GetEmployeeAsync(Guid companyId, Guid id, bool trackChange);
        void CreateEmployeeForCompany(Guid companyId, Employee employee);
        void DeleteEmployee(Employee employee);
    }
}
