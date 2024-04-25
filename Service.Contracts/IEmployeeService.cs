using Entities.Models;
using Shared.DataTranferObjects;
using Shared.RequestFeatures;
using System.Dynamic;

namespace Service.Contracts
{
    public interface IEmployeeService
    {
        Task<(IEnumerable<ExpandoObject> employees, MetaData metaData)> 
            GetEmployeesAsync(
            Guid companyId, EmployeeParameters employeeParameters, bool trackChange);
        
        Task<EmployeeDto> GetEmployeeAsync(Guid employeeId, Guid id, bool trackChange);

        Task<EmployeeDto> CreateEmployeeForCompanyAsync(
            Guid companyId, 
            EmployeeForCreationDto employeeForCreation, 
            bool trackChange);

        Task DeleteEmployeeForCompanyAsync(
            Guid companyId, 
            Guid id, 
            bool trackChanges);

        Task UpdateEmployeeForCompanyAsync(
            Guid companyId,
            Guid id,
            EmployeeForUpdateDto employeeForUpdate,
            bool compTrackChange,
            bool empTrackChanges);

        Task<(EmployeeForUpdateDto employeeToPatch, Employee employeeEntity)> 
            GetEmployeeForPatchAsync(
                Guid companyId,
                Guid id,
                bool compTrackChanges,
                bool empTrackChanges);

        Task SaveChangesForPatchAsync(
            EmployeeForUpdateDto employeeToPatch,
            Employee employeeEntity);
    }
}
