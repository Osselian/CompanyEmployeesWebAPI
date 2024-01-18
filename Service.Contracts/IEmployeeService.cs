using Shared.DataTranferObjects;

namespace Service.Contracts
{
    public interface IEmployeeService
    {
        IEnumerable<EmployeeDto> GetEmployees(Guid companyId, bool trackChange);
        
        EmployeeDto GetEmployee(Guid employeeId, Guid id, bool trackChange);

        EmployeeDto CreateEmployeeForCompany(
            Guid companyId, 
            EmployeeForCreationDto employeeForCreation, 
            bool trackChange);

        void DeleteEmployeeForCompany(
            Guid companyId, 
            Guid id, 
            bool trackChanges);

        void UpdateEmployeeForCompany(
            Guid companyId,
            Guid id,
            EmployeeForUpdateDto employeeForUpdate,
            bool compTrackChange,
            bool empTrackChanges);
    }
}
