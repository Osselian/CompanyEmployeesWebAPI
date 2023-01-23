using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    public class Company
    {
        [Column("CompanyId")]
        public Guid Id { get; set; }

        [Required(ErrorMessage ="Company name is a required field")]
        [MaxLength(60, ErrorMessage ="Max length for the Name is 60 characters")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Company addres is a required field")]
        [MaxLength(60, ErrorMessage = "Max length for the Addres is 60 characters")]
        public string? Addres { get; set; }

        public string? Country { get; set; }

        public ICollection<Employee>? Employees { get; set; }
    }
}
