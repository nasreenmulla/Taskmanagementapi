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
    public class UserControllerTests
    {
        [Fact]
        public async Task GetAllUsers_ShouldReturnUsers()
        {
            // Arrange
            var mockRepo = new Mock<IRepository<User>>();
            mockRepo.Setup(repo => repo.GetAll()).ReturnsAsync(new List<User>
            {
                new User { Id = 1, Username = "Admin", Role = "Admin" },
                new User { Id = 2, Username = "User", Role = "User" }
            });

            var controller = new UserController(mockRepo.Object);

            // Act
            var result = await controller.GetAllUsers();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<List<User>>(okResult.Value);
            Assert.Equal(2, returnValue.Count);
        }

        [Fact]
        public async Task CreateUser_ShouldReturnCreatedUser()
        {
            // Arrange
            var mockRepo = new Mock<IRepository<User>>();
            var newUser = new User { Id = 3, Username = "TestUser", Role = "User" };

            mockRepo.Setup(repo => repo.Add(It.IsAny<User>())).Returns(Task.CompletedTask);

            var controller = new UserController(mockRepo.Object);

            // Act
            var result = await controller.CreateUser(newUser);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<User>(okResult.Value);
            Assert.Equal("TestUser", returnValue.Username);
        }
    }
}
