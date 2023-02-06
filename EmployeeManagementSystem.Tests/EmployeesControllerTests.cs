using EmployeeManagementSystem.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using Xunit;

namespace EmployeeManagementSystem.Tests
{
    public class EmployeesControllerTests
    {
        private readonly EmployeesController employeesController;
        public EmployeesControllerTests()
        {
            employeesController = new EmployeesController();
        }

        [Fact]
        public void Index_ReturnsViewResult()
        {
            //Act
            var result = employeesController.Index();
            //Assert
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void Details_WithNullId_ReturnsNotFound()
        {
            //Act
            var result = employeesController.Details(null);
            //Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void Details_WithValidId_ReturnsViewResult()
        {
            //Act
            var result = employeesController.Details(Guid.NewGuid());
            //Assert
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void Create_ReturnsViewResult()
        {
            //Act
            var result = employeesController.Create();
            //Assert
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void Edit_WithId_ReturnsNotFoundResult()
        {
            //Act
            var result = employeesController.Edit(null);
            //Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void Edit_WithValidId_ReturnsViewResult()
        {
            //Act
            var result = employeesController.Edit(Guid.NewGuid());
            //Assert
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void Delete_WithId_ReturnsNotFoundResult()
        {
            //Act
            var result = employeesController.Delete(null);
            //Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void Delete_WithValidId_ReturnsViewResult()
        {
            //Act
            var result = employeesController.Delete(Guid.NewGuid());
            //Assert
            Assert.IsType<ViewResult>(result);
        }
    }
}
