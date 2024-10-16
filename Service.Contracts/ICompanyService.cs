using Entities.Responses;
using Shared.DataTranferObjects;

namespace Service.Contracts
{
    public interface ICompanyService
    {
        ApiBaseResponse GetAllCompanies(bool trackChanges);
        ApiBaseResponse GetCompany(Guid companyId, bool trackChanges);
    }
}
