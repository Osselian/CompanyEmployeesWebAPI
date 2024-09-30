using Contracts;
using Entities.LinkModels;
using Entities.Models;
using Microsoft.Net.Http.Headers;
using Shared.DataTranferObjects;

namespace CompanyEmployees.Utility
{
    public class EmployeeLinks : IEmployeeLinks
    {
        private readonly LinkGenerator _linkGenerator;
        private readonly IDataShaper<EmployeeDto> _dataShaper;

        public EmployeeLinks(
            LinkGenerator linkGenerator, IDataShaper<EmployeeDto> dataShaper)
        {
            _linkGenerator = linkGenerator;
            _dataShaper = dataShaper;
        }

        public LinkResponse TryGenerateLinks(
            IEnumerable<EmployeeDto> employeesDto, 
            string fields, 
            Guid companyId,
            HttpContext httpContext) 
        {
            var shapedEmployees = ShapeData(employeesDto, fields);

            if (ShouldGenerateLinks(httpContext))
                return ReturnLinkedEmployees(
                    employeesDto, fields, companyId, httpContext, shapedEmployees);

            return ReturnLinkedEmployees(shapedEmployees);

        }

        private List<Entity> ShapeData(IEnumerable<EmployeeDto> employeesDto, string fields) =>
            _dataShaper.ShapeData(employeesDto, fields)
                .Select(e => e.Entity)
                .ToList();
        
        private bool ShouldGenerateLinks(HttpContext httpContext)
        {
            var mediaType = 
                (MediaTypeHeaderValue)httpContext.Items["AcceptHeaderMediaType"];

            return mediaType.SubTypeWithoutSuffix.EndsWith(
                "hateoas", StringComparison.InvariantCultureIgnoreCase);
        }

        private LinkResponse ReturnLinkedEmployees(List<Entity> shapedEmployees) =>
            new LinkResponse { ShapedEntities = shapedEmployees };

        private LinkResponse ReturnLinkedEmployees(
            IEnumerable<EmployeeDto> employeesDto, 
            string fields, 
            Guid companyId, 
            HttpContext httpContext, 
            List<Entity> shapedEmployees)
        {
            var employeesDtoList = employeesDto.ToList();

            for (var index = 0; index < employeesDtoList.Count; index++)
            {
                var employeesLinks = CreateLinksForEmployee(
                    httpContext, companyId, employeesDtoList[index].Id, fields);

                shapedEmployees[index].Add("Links", employeesLinks);
            }

            var employeeCollection = new LinkCollectionWrapper<Entity>(shapedEmployees);
            var linkedEmployees =
                CreateLinksForEmployees(httpContext, employeeCollection);

            return new LinkResponse { HasLink = true, LinkedEntities = linkedEmployees };
        }


        private List<Link> CreateLinksForEmployee(
            HttpContext httpContext, Guid companyId, Guid id, string fields ="")
        {
            var links = new List<Link>()
            {
                new Link(
                    _linkGenerator.GetUriByAction(
                        httpContext, 
                        "GetEmployeeForCompany", 
                        values: new {companyId, id, fields}), 
                    "self",
                    "GET"),
                new Link(
                    _linkGenerator.GetUriByAction(
                        httpContext, 
                        "DeleteEmployeeForCompany", 
                        values: new {companyId, id, fields}), 
                    "delete_employee",
                    "DELETE"),
                new Link(
                    _linkGenerator.GetUriByAction(
                        httpContext, 
                        "UpdateEmployeeForCompany", 
                        values: new {companyId, id, fields}), 
                    "update_employee",
                    "PUT"),
                new Link(
                    _linkGenerator.GetUriByAction(
                        httpContext, 
                        "PartiallyUpdateEmployeeForCompany", 
                        values: new {companyId, id, fields}), 
                    "partially_update_employee",
                    "PATCH")
            };
            return links;
        }

        private LinkCollectionWrapper<Entity> CreateLinksForEmployees(
            HttpContext httpContext, LinkCollectionWrapper<Entity> employeeWrapper)
        {
            employeeWrapper.Links.Add(new Link(
                _linkGenerator.GetUriByAction(
                    httpContext,
                    "GetEmployeesForCompany",
                    values: new { }),
                "self",
                "GET"));

            return employeeWrapper;
        }
    }
}
