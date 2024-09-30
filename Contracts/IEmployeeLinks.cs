using Entities.LinkModels;
using Microsoft.AspNetCore.Http;
using Shared.DataTranferObjects;

namespace Contracts
{
    public interface IEmployeeLinks
    {
        LinkResponse TryGenerateLinks(
            IEnumerable<EmployeeDto> employeeDtos,
            string fields,
            Guid companyGuid,
            HttpContext httpContext);
    }
}
