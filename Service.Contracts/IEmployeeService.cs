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
    }
}
