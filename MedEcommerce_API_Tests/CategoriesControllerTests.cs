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
    public class CategoriesControllerTests
    {
        [Fact]
        public async Task GetCategories_ReturnsOkResult()
        {
            // Arrange
            var mockCategoryService = new Mock<ICategoriesService>();
            mockCategoryService.Setup(s => s.GetCategoriesAsync())
                .ReturnsAsync(new List<Category>
                {
                new Category { Id = 1, MedicineType = "Category1" },
                new Category { Id = 2, MedicineType = "Category2" }
                });

            var controller = new CategoriesController(mockCategoryService.Object);

            // Act
            var result = await controller.GetCategories();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var categories = Assert.IsAssignableFrom<IEnumerable<Category>>(okResult.Value);
            Assert.Equal(2, categories.Count());
        }

        [Fact]
        public async Task GetCategory_ExistingId_ReturnsOkResult()
        {
            // Arrange
            var mockCategoryService = new Mock<ICategoriesService>();
            var existingCategory = new Category { Id = 1, MedicineType = "Category1" };
            mockCategoryService.Setup(s => s.GetCategoryByIdAsync(1))
                .ReturnsAsync(existingCategory);

            var controller = new CategoriesController(mockCategoryService.Object);

            // Act
            var result = await controller.GetCategory(1);
            Console.WriteLine(result);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var category = Assert.IsType<Category>(okResult.Value);
            Assert.Equal(1, category.Id);
        }

        [Fact]
        public async Task GetCategory_NonExistingId_ReturnsNotFound()
        {
            // Arrange
            var mockCategoryService = new Mock<ICategoriesService>();
            mockCategoryService.Setup(s => s.GetCategoryByIdAsync(1))
                .ReturnsAsync((Category)null);

            var controller = new CategoriesController(mockCategoryService.Object);

            // Act
            var result = await controller.GetCategory(1);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task PutCategory_ExistingCategory_ReturnsUpdatedCategory()
        {
            // Arrange
            var mockCategoryService = new Mock<ICategoriesService>();
            var existingCategory = new Category { Id = 1, MedicineType = "Category1" };
            var updatedCategory = new Category { Id = 1, MedicineType = "UpdatedCategory" };
            mockCategoryService.Setup(s => s.UpdateCategoryAsync(1, updatedCategory))
                .ReturnsAsync(updatedCategory);

            var controller = new CategoriesController(mockCategoryService.Object);

            // Act
            var result = await controller.PutCategory(1, updatedCategory);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var category = Assert.IsType<Category>(okResult.Value);
            Assert.Equal("UpdatedCategory", category.MedicineType);
        }

        [Fact]
        public async Task PostCategory_ValidCategory_ReturnsCreatedCategory()
        {
            // Arrange
            var mockCategoryService = new Mock<ICategoriesService>();
            var newCategory = new Category { MedicineType = "NewCategory" };
            mockCategoryService.Setup(s => s.CreateCategoryAsync(newCategory))
                .ReturnsAsync(newCategory);

            var controller = new CategoriesController(mockCategoryService.Object);

            // Act
            var result = await controller.PostCategory(newCategory);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var category = Assert.IsType<Category>(createdAtActionResult.Value);
            Assert.Equal("NewCategory", category.MedicineType);
        }

        [Fact]
        public async Task DeleteCategory_ExistingId_ReturnsNoContent()
        {
            // Arrange
            var mockCategoryService = new Mock<ICategoriesService>();
            mockCategoryService.Setup(s => s.DeleteCategoryAsync(1))
                .ReturnsAsync(true);
            var controller = new CategoriesController(mockCategoryService.Object);

            //Act
            var result = await controller.DeleteCategory(1);

            //Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task DeleteCategory_NonExistingId_ReturnsNotFound()
        {
            // Arrange
            var mockCategoryService = new Mock<ICategoriesService>();
            mockCategoryService.Setup(s => s.DeleteCategoryAsync(1))
                .ReturnsAsync(false);

            var controller = new CategoriesController(mockCategoryService.Object);

            // Act
            var result = await controller.DeleteCategory(1);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

    }
}
