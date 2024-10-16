using AutoMapper;
using Contracts;
using Entities.Responses;
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

        public ApiBaseResponse GetAllCompanies(bool trackChanges)
        {
                var companies = _repository.Company.GetAllCompanies(trackChanges);
                var companiesDtos = _mapper.Map<IEnumerable<CompanyDto>>(companies);
                return new ApiOkResponse<IEnumerable<CompanyDto>>(companiesDtos);
        }

        public ApiBaseResponse GetCompany(Guid companyId, bool trackChanges)
        {
            var company = _repository.Company.GetCompany(companyId, trackChanges);
            if (company is null)
                return new CompanyNotFoundResponse(companyId);

            var companyDto = _mapper.Map<CompanyDto>(company);
            return new ApiOkResponse<CompanyDto>(companyDto);
        }
    }
}
