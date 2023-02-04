﻿using System;
using System.ComponentModel.DataAnnotations;

namespace EmployeeManagementSystem.Dtos
{
    public class EmployeeCreateDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Display(Name ="Date of birth")]
        [DataType(DataType.Date)]
        public DateTime Dob { get; set; }
        public string Department { get; set; }
    }
}
