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
    public class MedicineControllerTests
    {
        [Fact]
        public async Task GetMedicines_ReturnsOkResult()
        {
            // Arrange
            var mockMedicineService = new Mock<IMedicineService>();
            mockMedicineService.Setup(s => s.GetMedicinesAsync())
                .ReturnsAsync(new List<Medicine>
                {
                new Medicine { Id = 1, Name="Medicine1",Description="Description1",Price=100,IsAvailable=true,CategoryId=1 },
                new Medicine { Id = 2, Name="Medicine2",Description="Description2",Price=150,IsAvailable=true,CategoryId=1 }
                });

            var controller = new MedicinesController(mockMedicineService.Object);

            // Act
            var result = await controller.GetMedicines();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var medicines = Assert.IsAssignableFrom<IEnumerable<Medicine>>(okResult.Value);
            Assert.Equal(2, medicines.Count());
        }

        [Fact]
        public async Task GetMedicine_ExistingId_ReturnsOkResult()
        {
            // Arrange
            var mockMedicineService = new Mock<IMedicineService>();
            var existingMedicine = new Medicine { Id = 1, Name = "Medicine1", Description = "Description1", Price = 100, IsAvailable = true, CategoryId = 1 };
            mockMedicineService.Setup(s => s.GetMedicineByIdAsync(1))
                .ReturnsAsync(existingMedicine);

            var controller = new MedicinesController(mockMedicineService.Object);

            // Act
            var result = await controller.GetMedicine(1);
            
            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var medicine = Assert.IsType<Medicine>(okResult.Value);
            Assert.Equal(1, medicine.Id);
        }

        [Fact]
        public async Task GetMedicine_NonExistingId_ReturnsNotFound()
        {
            // Arrange
            var mockMedicineService = new Mock<IMedicineService>();
            mockMedicineService.Setup(s => s.GetMedicineByIdAsync(1))
                .ReturnsAsync((Medicine)null);

            var controller = new MedicinesController(mockMedicineService.Object);

            // Act
            var result = await controller.GetMedicine(1);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task PutMedicine_ExistingMedicine_ReturnsUpdatedMedicine()
        {
            // Arrange
            var mockMedicineService = new Mock<IMedicineService>();
            var existingMed = new Medicine { Id = 1, Name = "Medicine1", Description = "Description", Price = 100, IsAvailable = true, CategoryId = 1 };
            var updatedMed = new Medicine { Id = 1, Name = "Medicine1", Description = "UpdatedDescription", Price = 100, IsAvailable = true, CategoryId = 1 };
            mockMedicineService.Setup(s => s.UpdateMedicineAsync(1, updatedMed))
                .ReturnsAsync(updatedMed);

            var controller = new MedicinesController(mockMedicineService.Object);

            // Act
            var result = await controller.PutMedicine(1, updatedMed);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var medicine = Assert.IsType<Medicine>(okResult.Value);
            Assert.Equal("UpdatedDescription", medicine.Description);
        }

        [Fact]
        public async Task PostMedicine_ValidMedicine_ReturnsCreatedMedicine()
        {
            // Arrange
            var mockMedicineService = new Mock<IMedicineService>();
            var newMedicine = new Medicine { Id = 1, Name = "Medicine1", Description = "New Medicine", Price = 100, IsAvailable = true, CategoryId = 1 };
            mockMedicineService.Setup(s => s.CreateMedicineAsync(newMedicine))
                .ReturnsAsync(newMedicine);

            var controller = new MedicinesController(mockMedicineService.Object);

            // Act
            var result = await controller.PostMedicine(newMedicine);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var medicine = Assert.IsType<Medicine>(createdAtActionResult.Value);
            Assert.Equal("New Medicine", medicine.Description);
        }

        [Fact]
        public async Task DeleteMedicine_ExistingId_ReturnsNoContent()
        {
            // Arrange
            var mockMedicineService = new Mock<IMedicineService>();
            mockMedicineService.Setup(s => s.DeleteMedicineAsync(1))
                .ReturnsAsync(true);
            var controller = new MedicinesController(mockMedicineService.Object);

            //Act
            var result = await controller.DeleteMedicine(1);

            //Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task DeleteMedicine_NonExistingId_ReturnsNotFound()
        {
            // Arrange
            var mockMedicineService = new Mock<IMedicineService>();
            mockMedicineService.Setup(s => s.DeleteMedicineAsync(1))
                .ReturnsAsync(false);

            var controller = new MedicinesController(mockMedicineService.Object);

            // Act
            var result = await controller.DeleteMedicine(1);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

    }
}
