using EmployeeManagementSystem.Controllers.Api;
using EmployeeManagementSystem.Dtos;
using EmployeeManagementSystem.Repositories;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using Xunit;

namespace EmployeeManagementSystem.Tests.Api
{
    public class EmployeesControllerTests
    {
        private readonly EmployeesController _controller;
        private readonly Mock<IEmployeeRepository> _mockRepository = new Mock<IEmployeeRepository>();
        public EmployeesControllerTests()
        {
            _controller = new EmployeesController(_mockRepository.Object);
        }

        [Fact]
        public void GetAll_ReturnsOkResult_WithData()
        {
            //Arrange
            var employees = new List<EmployeeReadDto>
            {
                new EmployeeReadDto { Id = Guid.NewGuid(), Name = "Employee 1", Email = "employee1@gmail.com", Department = "HR", Dob = DateTime.Now.AddYears(-40) },
                new EmployeeReadDto { Id = Guid.NewGuid(), Name = "Employee 2", Email = "employee2@gmail.com", Department = "Finance", Dob = DateTime.Now.AddYears(-35) }
            };

            _mockRepository.Setup(repo => repo.GetAll()).Returns(employees);
            //Act
            var result = _controller.GetAll();
            //Assert
            var okResult = result.Result as OkObjectResult;
            Assert.Equal(200, okResult.StatusCode);

            var json = JsonConvert.SerializeObject(okResult.Value);
            var deserialized = JsonConvert.DeserializeObject<Dictionary<string, List<EmployeeReadDto>>>(json);
            var _employees = deserialized["data"];
            Assert.NotNull(_employees);
            Assert.Equal(employees.Count, _employees.Count);
        }

        [Fact]
        public void GetEmployee_RetursOkResult_WithData()
        {
            //Arrange
            var id = Guid.NewGuid();
            var employee = new EmployeeReadDto
            {
                Id = id,
                Name = "Employee 1",
                Department = "HR",
                Email = "employee1@gmail.com",
                Dob = DateTime.Now.AddYears(-40)
            };
            _mockRepository.Setup(repo => repo.GetById(id)).Returns(employee);

            //Act
            var result = _controller.GetEmployee(id);
            //Assert
            Assert.IsType<ActionResult<EmployeeReadDto>>(result);

            var _employee = result.Value as EmployeeReadDto;
            Assert.Equal(employee, _employee);
        }

        [Fact]
        public void GetEmployee_ReturnsNotFound_WhenEmployeeNotFound()
        {
            //Arrange
            var id = Guid.NewGuid();
            _mockRepository.Setup(repo => repo.GetById(id)).Returns((EmployeeReadDto)null);
            //Act
            var result = _controller.GetEmployee(id);
            //Assert
            Assert.IsType<ActionResult<EmployeeReadDto>>(result);
            Assert.Null(result.Value);
            var notFoundResult = result.Result as NotFoundResult;
            Assert.Equal(404, notFoundResult.StatusCode);
        }

        [Fact]
        public void Create_ReturnsCreatedWithActionResult()
        {
            //Arrange
            var employee = new EmployeeCreateDto
            {
                Name = "Employee 1",
                Department = "HR",
                Email = "employee1@gmail.com",
                Dob = DateTime.Now.AddYears(-40)
            };
            _mockRepository.Setup(repo => repo.Create(employee)).Returns(
                new EmployeeReadDto
                {
                    Id = Guid.NewGuid(),
                    Email = employee.Email,
                    Name = employee.Name,
                    Dob = employee.Dob,
                    Department = employee.Department
                });
            //Act
            var result = _controller.Create(employee);

            //Assert
            Assert.IsType<CreatedAtActionResult>(result);
            var createdAtAction = result as CreatedAtActionResult;
            Assert.Equal("GetEmployee", createdAtAction.ActionName);
            Assert.NotNull(createdAtAction.RouteValues["id"]);
        }

        [Fact]
        public void Update_ReturnsNoContentResult()
        {
            //Arrange
            var employeeUpdateDto = new EmployeeUpdateDto
            {
                Id = Guid.NewGuid(),
                Name = "Employee 1",
                Department = "HR",
                Email = "employee1@gmail.com",
                Dob = DateTime.Now.AddYears(-40)
            };

            _mockRepository.Setup(repo => repo.EmployeeExists(employeeUpdateDto.Id)).Returns(true);
            //Act
            var result = _controller.Update(employeeUpdateDto.Id, employeeUpdateDto);
            //Assert
            Assert.IsType<NoContentResult>(result);
            var noContent = result as NoContentResult;
            Assert.Equal(204, noContent.StatusCode);
        }

        [Fact]
        public void Update_Returns_NotFound()
        {
            //Arrange
            //Arrange
            var employeeUpdateDto = new EmployeeUpdateDto
            {
                Id = Guid.NewGuid(),
                Name = "Employee 1",
                Department = "HR",
                Email = "employee1@gmail.com",
                Dob = DateTime.Now.AddYears(-40)
            };
            _mockRepository.Setup(repo => repo.EmployeeExists(employeeUpdateDto.Id)).Returns(false);
            //Act
            var result = _controller.Update(employeeUpdateDto.Id, employeeUpdateDto);
            //Assert
            Assert.IsType<NotFoundResult>(result);
            var notFound = result as NotFoundResult;
            Assert.Equal(404, notFound.StatusCode);
        }

        [Fact]
        public void Delete_ReturnNoContentResult()
        {
            var employeeId = Guid.NewGuid();
            _mockRepository.Setup(repo => repo.EmployeeExists(employeeId)).Returns(true);
            //Act
            var result = _controller.Delete(employeeId);
            //Assert
            Assert.IsType<NoContentResult>(result);
            var noContent = result as NoContentResult;
            Assert.Equal(204, noContent.StatusCode);
        }

        [Fact]
        public void Delete_Returns_NotFound()
        {
            //Arrange
            var employeeId = Guid.NewGuid();
            _mockRepository.Setup(repo => repo.EmployeeExists(employeeId)).Returns(false);
            //Act
            var result = _controller.Delete(employeeId);
            //Assert
            Assert.IsType<NotFoundResult>(result);
            var notFound = result as NotFoundResult;
            Assert.Equal(404, notFound.StatusCode);
        }
    }
}
