using System;
using System.ComponentModel.DataAnnotations;

namespace EmployeeManagementSystem.Dtos
{
    public class EmployeeReadDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        [Display(Name = "Date of birth")]
        public DateTime Dob { get; set; }
        public string Department { get; set; }
    }
}
