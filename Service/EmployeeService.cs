using AutoMapper;
using Contracts;
using Entities.Exceptions;
using Entities.Models;
using Service.Contracts;
using Shared.DataTranferObjects;
using Shared.RequestFeatures;
using System.Dynamic;

namespace Service
{
    internal sealed class EmployeeService : IEmployeeService
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        private readonly IDataShaper<EmployeeDto> _dataShaper;

        public EmployeeService(
            IRepositoryManager repository, 
            ILoggerManager logger, 
            IMapper mapper,
            IDataShaper<EmployeeDto> dataShaper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
            _dataShaper = dataShaper;
        }
       
        public async Task<EmployeeDto> GetEmployeeAsync(
            Guid companyId, Guid id, bool trackChange)
        {
            await CheckIfCompanyExists(companyId, trackChange);

            var employee = await GetEmployeeForCompanyAndCheckIfItExists(
                companyId, id, trackChange);

            var employeeDto = _mapper.Map<EmployeeDto>(employee);
            return employeeDto;
        }

        public async Task<(IEnumerable<Entity> employees, MetaData metaData)> 
            GetEmployeesAsync(
            Guid companyId, 
            EmployeeParameters employeeParameters, 
            bool trackChange)
        {
            if (!employeeParameters.ValidAgeRange)
                throw new MaxAgeRangeBadRequestException();
            await CheckIfCompanyExists(companyId, trackChange);

            var employeesFromDb = await _repository.Employee
                .GetEmployeesAsync(companyId, employeeParameters,trackChange);
            var employeesDto = _mapper.Map<IEnumerable<EmployeeDto>>(employeesFromDb);
            var shapedData = _dataShaper
                .ShapeData(employeesDto, employeeParameters.Fields);

            return (employees: shapedData, metaData: employeesFromDb.MetaData);
        }

        public async Task<EmployeeDto> CreateEmployeeForCompanyAsync(
            Guid companyId, 
            EmployeeForCreationDto employeeForCreation, 
            bool trackChange)
        {

            await CheckIfCompanyExists(companyId, trackChange);

            var employeeEntity = _mapper.Map<Employee>(employeeForCreation);

            _repository.Employee
                .CreateEmployeeForCompany(companyId, employeeEntity);
            await _repository.SaveAsync();

            var employeeToReturn = _mapper.Map<EmployeeDto>(employeeEntity);
            return employeeToReturn;
        }

        public async Task DeleteEmployeeForCompanyAsync(
            Guid companyId, 
            Guid id, 
            bool trackChanges)
        {
            await CheckIfCompanyExists(companyId, trackChanges);

            var employeeForCompany =
                await GetEmployeeForCompanyAndCheckIfItExists(
                    companyId, id, trackChanges);
            _repository.Employee.DeleteEmployee(employeeForCompany);
            await _repository.SaveAsync();
        }

        public async Task UpdateEmployeeForCompanyAsync(
            Guid companyId, 
            Guid id, 
            EmployeeForUpdateDto employeeForUpdate, 
            bool compTrackChange, 
            bool empTrackChanges)
        {
            await CheckIfCompanyExists(companyId, compTrackChange);

            var employeeForCompany =
                await GetEmployeeForCompanyAndCheckIfItExists(
                    companyId, id, empTrackChanges);

            _mapper.Map(employeeForUpdate, employeeForCompany);
            await _repository.SaveAsync();
        }

        public async Task<(EmployeeForUpdateDto employeeToPatch, 
            Employee employeeEntity)> 
            GetEmployeeForPatchAsync(
                Guid companyId, 
                Guid id, 
                bool compTrackChanges, 
                bool empTrackChanges)
        {
            await CheckIfCompanyExists(companyId, compTrackChanges);

            var employeeEntity =
                await GetEmployeeForCompanyAndCheckIfItExists(
                    companyId, id, empTrackChanges);

            var employeeToPatch =
                _mapper.Map<EmployeeForUpdateDto>(employeeEntity);

            return (employeeToPatch, employeeEntity);
        }

        public async Task SaveChangesForPatchAsync(
            EmployeeForUpdateDto employeeToPatch, 
            Employee employeeEntity)
        {
            _mapper.Map(employeeToPatch, employeeEntity);
            await _repository.SaveAsync();
        }

        private async Task CheckIfCompanyExists(Guid companyId, bool trackChanges)
        {
            var company = 
                await _repository.Company.GetCompanyAsync(companyId, trackChanges);
            if (company is null)
                throw new CompanyNotFoundException(companyId);
        }

        private async Task<Employee> GetEmployeeForCompanyAndCheckIfItExists(
            Guid companyId, Guid id, bool trackChanges)
        {
            var employeeEntity = 
                await _repository.Employee
                .GetEmployeeAsync(companyId, id, trackChanges);
            if (employeeEntity is null)
                throw new EmployeeNotFoundException(id);
            return employeeEntity;
        }

    }
}
