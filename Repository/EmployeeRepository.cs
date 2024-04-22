using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class EmployeeRepository : RepositoryBase<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public async Task<Employee> GetEmployeeAsync(Guid companyId, Guid id, bool trackChange) =>
             await FindByCondition(e => e.CompanyId.Equals(companyId) && e.Id.Equals(id),
                trackChange)
            .SingleOrDefaultAsync(); 

        public async Task<IEnumerable<Employee>> GetEmployeesAsync(Guid companyId, bool trackChange) =>
            await FindByCondition(e => e.CompanyId.Equals(companyId), trackChange)
            .OrderBy(e => e.Name)
            .ToListAsync();

        public void CreateEmployeeForCompany(Guid companyId, Employee employee)
        {
            employee.CompanyId = companyId;
            Create(employee);
        }

        public void DeleteEmployee(Employee employee) => Delete(employee);
    }
}
