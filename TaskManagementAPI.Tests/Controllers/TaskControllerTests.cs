using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaskManagementAPI.Controllers;
using TaskManagementAPI.Models;
using TaskManagementAPI.Repositories;

namespace TaskManagementAPI.Tests.Controllers
{
    public class TaskControllerTests
    {
        [Fact]
        public async Task GetAllTasks_AdminRole_ShouldReturnTasks()
        {
            // Arrange
            var mockRepo = new Mock<IRepository<TaskItem>>();
            mockRepo.Setup(repo => repo.GetAll()).ReturnsAsync(new List<TaskItem>
            {
                new TaskItem { Id = 1, Title = "Task 1", Status = "Pending", AssignedToUserId = 2 },
                new TaskItem { Id = 2, Title = "Task 2", Status = "Completed", AssignedToUserId = 1 }
            });

            var controller = new TaskController(mockRepo.Object);

            // Simulate Admin role
            controller.ControllerContext.HttpContext = new DefaultHttpContext();
            controller.HttpContext.Items["UserRole"] = "Admin";

            // Act
            var result = await controller.GetAllTasks();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<List<TaskItem>>(okResult.Value);
            Assert.Equal(2, returnValue.Count);
        }

        [Fact]
        public async Task CreateTask_NonAdmin_ShouldReturnForbidden()
        {
            // Arrange
            var mockRepo = new Mock<IRepository<TaskItem>>();
            var newTask = new TaskItem { Id = 3, Title = "Test Task", Status = "Pending", AssignedToUserId = 2 };

            var controller = new TaskController(mockRepo.Object);

            // Simulate User role
            controller.ControllerContext.HttpContext = new DefaultHttpContext();
            controller.HttpContext.Items["UserRole"] = "User";

            // Act
            var result = await controller.CreateTask(newTask);

            // Assert
            Assert.IsType<ForbidResult>(result);
        }
    }
}
