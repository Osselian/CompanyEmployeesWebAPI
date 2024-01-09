﻿using Contracts;
using Entities.Models;

namespace Repository
{
    public class EmployeeRepository : RepositoryBase<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }


        public Employee GetEmployee(Guid companyId, Guid id, bool trackChange) =>
            FindByCondition(e => e.CompanyId.Equals(companyId) && e.Id.Equals(id),
                trackChange)
            .SingleOrDefault(); 

        public IEnumerable<Employee> GetEmployees(Guid companyId, bool trackChange) =>
            FindByCondition(e => e.CompanyId.Equals(companyId), trackChange)
            .OrderBy(e => e.Name)
            .ToList();

        public void CreateEmployeeForCompany(Guid companyId, Employee employee)
        {
            employee.CompanyId = companyId;
            Create(employee);
        }

        public void DeleteEmployee(Employee employee) => Delete(employee);
    }
}
