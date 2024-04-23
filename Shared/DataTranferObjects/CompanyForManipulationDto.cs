﻿using System.ComponentModel.DataAnnotations;

namespace Shared.DataTranferObjects
{
    public abstract record CompanyForManipulationDto ()
    {
        [Required(ErrorMessage = "Company name is a required field.")]
        [MaxLength(30, ErrorMessage = "Maximum length for the Name is 30 characters.")]
        public string Name { get; init; } 

        [Required(ErrorMessage = "Address is a required field.")]
        [MaxLength(300, ErrorMessage = "Maximum length for the Address is 300 characters.")]
        public string Address { get; init; } 

        [Required(ErrorMessage = "Country is a required field.")]
        [MaxLength(30, ErrorMessage = "Maximum length for the Country is 30 characters.")]
        public string Country { get; init; } 


        IEnumerable<EmployeeForCreationDto> Employees;
    }
}
