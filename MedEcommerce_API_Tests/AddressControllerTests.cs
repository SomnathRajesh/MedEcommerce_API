using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MedEcommerce_API.Controllers;
using MedEcommerce_Core;
using MedEcommerce_DB;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace MedEcommerce_API_Tests
{
    public class AddressControllerTests
    {
        [Fact]
        public async Task GetAddresses_ReturnsOkResult()
        {
            // Arrange
            var mockAddressService = new Mock<IAddressService>();
            mockAddressService.Setup(s => s.GetAddressesAsync())
                .ReturnsAsync(new List<Address>
                {
                new Address { Id = 1,FirstName="Mohan",LastName="Pyare",City="Mumbai" }
                });

            var controller = new AddressesController(mockAddressService.Object);

            // Act
            var result = await controller.GetAddresses();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var addresses = Assert.IsAssignableFrom<IEnumerable<Address>>(okResult.Value);
            Assert.Equal(1, addresses.Count());
        }

        [Fact]
        public async Task GetAddress_ExistingId_ReturnsOkResult()
        {
            // Arrange
            var mockAddressService = new Mock<IAddressService>();
            var expectedAdd = new List<Address> {new Address { Id = 1, FirstName = "Mohan", LastName = "Pyare", City = "Pune", UserId = 2 } };
            mockAddressService.Setup(s => s.GetAddressesByIdAsync(1))
                .ReturnsAsync(expectedAdd);

            var controller = new AddressesController(mockAddressService.Object);

            // Act
            var result = await controller.GetAddress(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var addresses = Assert.IsAssignableFrom<IEnumerable<Address>>(okResult.Value);
            Assert.Equal(expectedAdd, addresses);
        }

        [Fact]
        public async Task GetAddress_NonExistingId_ReturnsNotFound()
        {
            // Arrange
            var mockAddressService = new Mock<IAddressService>();
            mockAddressService.Setup(s => s.GetAddressesByIdAsync(1))
                .ReturnsAsync((List<Address>)null);

            var controller = new AddressesController(mockAddressService.Object);

            // Act
            var result = await controller.GetAddress(1);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task PutAddress_ExistingAddress_ReturnsUpdatedAddress()
        {
            // Arrange
            var mockAddressService = new Mock<IAddressService>();
            var existingAdd = new Address { Id = 1, FirstName = "Mohan", LastName = "Pyare", City = "Pune", UserId = 2 };
            var updatedAdd = new Address { Id = 1, FirstName = "Mohan", LastName = "Pyare", City = "Mumbai", UserId = 2 };
            mockAddressService.Setup(s => s.UpdateAddressAsync(1, updatedAdd))
                .ReturnsAsync(updatedAdd);

            var controller = new AddressesController(mockAddressService.Object);

            // Act
            var result = await controller.PutAddress(1, updatedAdd);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var address = Assert.IsType<Address>(okResult.Value);
            Assert.Equal("Mumbai", address.City);
        }

        [Fact]
        public async Task PostAddress_ValidAddress_ReturnsCreatedAddress()
        {
            // Arrange
            var mockAddressService = new Mock<IAddressService>();
            var newAddress = new Address { Id = 1, FirstName = "Mohan", LastName = "Pyare", City = "Pune", UserId = 2 };
            mockAddressService.Setup(s => s.CreateAddressAsync(newAddress))
                .ReturnsAsync(newAddress);

            var controller = new AddressesController(mockAddressService.Object);

            // Act
            var result = await controller.PostAddress(newAddress);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var address = Assert.IsType<Address>(createdAtActionResult.Value);
            Assert.Equal(newAddress,address);
        }

        [Fact]
        public async Task DeleteAddress_ExistingId_ReturnsNoContent()
        {
            // Arrange
            var mockAddressService = new Mock<IAddressService>();
            mockAddressService.Setup(s => s.DeleteAddressAsync(1))
                .ReturnsAsync(true);
            var controller = new AddressesController(mockAddressService.Object);

            //Act
            var result = await controller.DeleteAddress(1);

            //Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task DeleteAddress_NonExistingId_ReturnsNotFound()
        {
            // Arrange
            var mockAddressService = new Mock<IAddressService>();
            mockAddressService.Setup(s => s.DeleteAddressAsync(1))
                .ReturnsAsync(false);

            var controller = new AddressesController(mockAddressService.Object);

            // Act
            var result = await controller.DeleteAddress(1);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
    }
}
