using EmployeeManagementSystem.Dtos;
using System;
using System.Collections.Generic;

namespace EmployeeManagementSystem.Repositories
{
    public interface IEmployeeRepository
    {
        List<EmployeeReadDto> GetAll();
        EmployeeReadDto GetById(Guid employeeId);
        List<EmployeeReadDto> Search(string query);
        EmployeeReadDto Create(EmployeeCreateDto employee);
        void Update(EmployeeUpdateDto employee);
        void Delete(Guid employeeId);
        bool EmployeeExists(Guid employeeId);
    }
}
