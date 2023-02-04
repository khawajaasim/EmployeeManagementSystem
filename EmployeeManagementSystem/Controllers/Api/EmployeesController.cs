using EmployeeManagementSystem.Dtos;
using EmployeeManagementSystem.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace EmployeeManagementSystem.Controllers.Api
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeRepository employeeRepository;
        public EmployeesController(IEmployeeRepository repository)
        {
            employeeRepository = repository;
        }

        [HttpGet]
        public ActionResult<List<EmployeeReadDto>> GetAll()
        {
            var employees = employeeRepository.GetAll();
            return Ok(new { data = employees });
        }

        [HttpGet("{id}")]
        public ActionResult<EmployeeReadDto> GetEmployee(Guid id)
        {
            var employee = employeeRepository.GetById(id);
            if (employee == null)
                return NotFound();

            return employee;
        }

        [HttpPost]
        public ActionResult Create([FromForm] EmployeeCreateDto employee)
        {
            var _employee = employeeRepository.Create(employee);
            return CreatedAtAction("GetEmployee", new { id = _employee.Id }, _employee);
        }

        [HttpPut("{id}")]
        public ActionResult Update(Guid id, [FromForm] EmployeeUpdateDto employeeUpdateDto)
        {
            if (id != employeeUpdateDto.Id)
                return BadRequest();
            var exist = employeeRepository.EmployeeExists(id);
            if (!exist)
                return NotFound();

            employeeRepository.Update(employeeUpdateDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(Guid id)
        {
            if (!employeeRepository.EmployeeExists(id))
                return NotFound();

            employeeRepository.Delete(id);
            return NoContent();
        }

        [HttpGet("search")]
        public ActionResult<List<EmployeeReadDto>> Search([FromQuery] string query)
        {
            return employeeRepository.Search(query);
        }
    }
}
