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
        /// <summary>
        ///  This method will return list of all employees stored in memory.
        /// </summary>
        /// <returns></returns>

        [HttpGet]
        public ActionResult<List<EmployeeReadDto>> GetAll()
        {
            var employees = employeeRepository.GetAll();
            return Ok(new { data = employees });
        }
        /// <summary>
        /// This will return specific employee
        /// </summary>
        /// <param name="id">Employee Id</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public ActionResult<EmployeeReadDto> GetEmployee(Guid id)
        {
            var employee = employeeRepository.GetById(id);
            if (employee == null)
                return NotFound();

            return employee;
        }
        /// <summary>
        ///  This will create a new employee
        /// </summary>
        /// <param name="employee">Employee Object</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Create([FromForm] EmployeeCreateDto employee)
        {
            var _employee = employeeRepository.Create(employee);
            return CreatedAtAction("GetEmployee", new { id = _employee.Id }, _employee);
        }
        /// <summary>
        /// This will update an employee
        /// </summary>
        /// <param name="id">Employee Id</param>
        /// <param name="employeeUpdateDto">Employee Object</param>
        /// <returns></returns>
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
        /// <summary>
        /// This will soft delete an employee
        /// </summary>
        /// <param name="id">Employee Id</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public ActionResult Delete(Guid id)
        {
            if (!employeeRepository.EmployeeExists(id))
                return NotFound();

            employeeRepository.Delete(id);
            return NoContent();
        }
        /// <summary>
        /// Will return an employee based on a query
        /// </summary>
        /// <param name="query">Name or Department or Email</param>
        /// <returns></returns>
        [HttpGet("search")]
        public ActionResult<List<EmployeeReadDto>> Search([FromQuery] string query)
        {
            return employeeRepository.Search(query);
        }
    }
}
