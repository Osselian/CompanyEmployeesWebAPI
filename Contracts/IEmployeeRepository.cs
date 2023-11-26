using Entities.Models;

namespace Contracts
{
    public interface IEmployeeRepository
    {
        IEnumerable<Employee> GetEmployees(Guid companyId, bool trackChange);
        Employee GetEmployee(Guid companyId, Guid id, bool trackChange);
    }
}
