using AutoMapper;
using Contracts;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Service.Contracts;

namespace Service
{
    public sealed class ServiceManager : IServiceManager
    {
        private readonly Lazy<ICompanyService> _companyService;
        private readonly Lazy<IEmployeeService> _employeeService;
        private readonly Lazy<IAutheticationService> _autheticationService;

        public ServiceManager(
            IRepositoryManager repositoryManager, 
            ILoggerManager logger, 
            IMapper mapper,
            IEmployeeLinks employeeLinks,
            UserManager<User> userManager,
            IConfiguration configuration)
        {
            _companyService = new Lazy<ICompanyService>(() =>
                new CompanyService(repositoryManager, logger, mapper));
            
            _employeeService = new Lazy<IEmployeeService>(() =>
                new EmployeeService(repositoryManager, logger, mapper, employeeLinks));

            _autheticationService = new Lazy<IAutheticationService>(() =>
                new AutheticationService(logger, mapper, userManager, configuration));
        }

        public ICompanyService CompanyService => _companyService.Value;

        public IEmployeeService EmployeeService => _employeeService.Value;

        public IAutheticationService AutheticationService => _autheticationService.Value;
    }
}
