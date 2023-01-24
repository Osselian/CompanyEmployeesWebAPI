using Contracts;
using Service.Contracts;
using Shared.DataTranferObjects;

namespace Service
{
    internal sealed class CompanyService : ICompanyService
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;

        public CompanyService(IRepositoryManager repository, ILoggerManager logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public IEnumerable<CompanyDto> GetAllCompanies(bool trackChanges)
        {
            try
            {
                var companies = _repository.Company.GetAllCompanies(trackChanges);
                var companiesDtos = companies
                    .Select(c => new CompanyDto(c.Id, c.Name ?? "", string.Join(c.Country, c.Address)))
                    .ToList();
                return companiesDtos;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in the {nameof(GetAllCompanies)} method - {ex.Message}");
                throw;
            }
        }
    }
}
