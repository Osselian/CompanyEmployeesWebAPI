using Entities.Models;
using Shared.DataTranferObjects;

namespace Service.Contracts
{
    public interface IEmployeeService
    {
        Task<IEnumerable<EmployeeDto>> GetEmployeesAsync(Guid companyId, bool trackChange);
        
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
