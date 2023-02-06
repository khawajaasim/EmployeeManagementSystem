using AutoMapper;
using EmployeeManagementSystem.Data;
using EmployeeManagementSystem.Dtos;
using EmployeeManagementSystem.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EmployeeManagementSystem.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;
        public EmployeeRepository(ApplicationDbContext applicationDbContext, IMapper mapper)
        {
            _dbContext = applicationDbContext;
            _mapper = mapper;
        }

        public EmployeeReadDto Create(EmployeeCreateDto employee)
        {
            var model = _mapper.Map<Employee>(employee);
            model.CreatedAt = DateTime.Now;
            _dbContext.Employees.Add(model);
            _dbContext.SaveChanges();
            return _mapper.Map<EmployeeReadDto>(model);
        }

        public void Delete(Guid employeeId)
        {
            var employee = _dbContext.Employees.FirstOrDefault(x => x.Id == employeeId);
            if (employee != null)
            {
                employee.IsDeleted = true;
                _dbContext.SaveChanges();
            }
        }

        public bool EmployeeExists(Guid employeeId)
        {
            return _dbContext.Employees.Any(x => x.Id == employeeId && !x.IsDeleted);
        }

        public List<EmployeeReadDto> GetAll()
        {
            var employees = _dbContext.Employees.Where(x => !x.IsDeleted).ToList();
            return _mapper.Map<List<EmployeeReadDto>>(employees);

        }

        public EmployeeReadDto GetById(Guid employeeId)
        {
            var employee = _dbContext.Employees.FirstOrDefault(x => x.Id == employeeId && !x.IsDeleted);
            return _mapper.Map<EmployeeReadDto>(employee);

        }

        public List<EmployeeReadDto> Search(string query)
        {
            var result = _dbContext.Employees.Where(x => x.Name.ToLower().Contains(query.ToLower()) || x.Email.ToLower().Contains(query.ToLower()) || x.Department.ToLower().Contains(query.ToLower()) && !x.IsDeleted).ToList();
            return _mapper.Map<List<EmployeeReadDto>>(result);
        }

        public void Update(EmployeeUpdateDto employee)
        {
            var model = _mapper.Map<Employee>(employee);
            model.UpdatedAt = DateTime.Now;
            _dbContext.Entry(model).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }
    }
}
