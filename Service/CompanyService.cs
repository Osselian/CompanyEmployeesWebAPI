using AutoMapper;
using Contracts;
using Service.Contracts;
using Shared.DataTranferObjects;

namespace Service
{
    internal sealed class CompanyService : ICompanyService
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;

        public CompanyService(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        public IEnumerable<CompanyDto> GetAllCompanies(bool trackChanges)
        {
            try
            {
                var companies = _repository.Company.GetAllCompanies(trackChanges);
                var companiesDtos = _mapper.Map<IEnumerable<CompanyDto>>(companies);
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
