using Microsoft.AspNetCore.Http;
using Shared.RequestFeatures;

namespace Entities.Models
{
    public record LinkParameters(
        EmployeeParameters EmployeeParameters, HttpContext Context);
}
