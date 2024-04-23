﻿using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Shared.RequestFeatures;

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

        public async Task<PagedList<Employee>> GetEmployeesAsync(Guid companyId,
            EmployeeParameters employeeParameters, bool trackChange)
        {
            var employees = await 
                FindByCondition(e => e.CompanyId.Equals(companyId), trackChange)
            .OrderBy(e => e.Name)
            .Skip((employeeParameters.PageNumber - 1) * employeeParameters.PageSize)
            .Take(employeeParameters.PageSize)
            .ToListAsync();

            var count = await FindByCondition(
                e => e.CompanyId.Equals(companyId), trackChange).CountAsync();

            return new PagedList<Employee>(employees, count, 
                employeeParameters.PageNumber, employeeParameters.PageSize);
        }

        public void CreateEmployeeForCompany(Guid companyId, Employee employee)
        {
            employee.CompanyId = companyId;
            Create(employee);
        }

        public void DeleteEmployee(Employee employee) => Delete(employee);
    }
}
