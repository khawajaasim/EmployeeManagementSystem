using AutoMapper;
using EmployeeManagementSystem.Dtos;
using EmployeeManagementSystem.Models;

namespace EmployeeManagementSystem.AutoMapper
{
    public class Profiles :Profile
    {

        public Profiles()
        {
            CreateMap<EmployeeCreateDto, Employee>();
            CreateMap<EmployeeReadDto, Employee>();
            CreateMap<EmployeeUpdateDto, Employee>();
        }
    }
}
