using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MedEcommerce_API.Controllers;
using MedEcommerce_Core.CustomExceptions;
using MedEcommerce_Core.DTO;
using MedEcommerce_Core;
using MedEcommerce_DB;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace MedEcommerce_API_Tests
{
    public class AuthenticationControllerTests
    {
        [Fact]
        public async Task SignUp_ValidUser_ReturnsCreatedResult()
        {
            // Arrange
            var userServiceMock = new Mock<IUserService>();
            var user = new User { Id=1,UserEmail="test@gmail.com",Password="password",RoleId=1 };
            var expectedResult = new AuthenticatedUser { Token="random",Id=1,Role="Admin",Useremail="test@gmail.com" };
            userServiceMock.Setup(service => service.SignUp(user))
                .ReturnsAsync(expectedResult);

            var controller = new AuthenticationController(userServiceMock.Object);

            // Act
            var result = await controller.SignUp(user);

            // Assert
            var createdResult = Assert.IsType<CreatedResult>(result);
            //Assert.Equal("", createdResult); // Check the action name as needed
            var model = Assert.IsType<AuthenticatedUser>(createdResult.Value);
            Assert.Equal(expectedResult, model);
        }

        [Fact]
        public async Task SignUp_UseremailAlreadyExists_ReturnsConflictResult()
        {
            // Arrange
            var userServiceMock = new Mock<IUserService>();
            var user = new User { Id = 1, UserEmail = "test@gmail.com", Password = "password", RoleId = 1 };
            userServiceMock.Setup(service => service.SignUp(user))
                .ThrowsAsync(new UseremailAlreadyExistsException("Email already exists"));

            var controller = new AuthenticationController(userServiceMock.Object);

            // Act
            var result = await controller.SignUp(user);

            // Assert
            var conflictResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(409, conflictResult.StatusCode);
            var message = Assert.IsType<string>(conflictResult.Value);
            Assert.Equal("Email already exists", message);
        }

        [Fact]
        public async Task SignIn_ValidUser_ReturnsOkResult()
        {
            // Arrange
            var userServiceMock = new Mock<IUserService>();
            var user = new User { Id = 1, UserEmail = "test@gmail.com", Password = "password", RoleId = 1 };
            var expectedResult = new AuthenticatedUser { Token = "random", Id = 1, Role = "Admin", Useremail = "test@gmail.com" };
            userServiceMock.Setup(service => service.SignIn(user))
                .ReturnsAsync(expectedResult);

            var controller = new AuthenticationController(userServiceMock.Object);

            // Act
            var result = await controller.SignIn(user);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var model = Assert.IsType<AuthenticatedUser>(okResult.Value);
            Assert.Equal(expectedResult, model);
        }

        [Fact]
        public async Task SignIn_InvalidUser_ReturnsUnauthorizedResult()
        {
            // Arrange
            var userServiceMock = new Mock<IUserService>();
            var user = new User { Id = 1, UserEmail = "test@gmail.com", Password = "password", RoleId = 1 };
            userServiceMock.Setup(service => service.SignIn(user))
                .ThrowsAsync(new InvalidUseremailPasswordException("Invalid email or password"));

            var controller = new AuthenticationController(userServiceMock.Object);

            // Act
            var result = await controller.SignIn(user);

            // Assert
            var unauthorizedResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(401, unauthorizedResult.StatusCode);
            var message = Assert.IsType<string>(unauthorizedResult.Value);
            Assert.Equal("Invalid email or password", message);
        }
    }
}
