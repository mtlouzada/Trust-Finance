using TF.Controllers;
using TF.Data;
using TF.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using TF.Models;

namespace TF.Tests
{
    [TestClass]
    public class AccountControllerTests
    {
        public object Assert { get; private set; }

        private TFDataContext GetInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<TFDataContext>()
                .UseInMemoryDatabase(databaseName: "TFTestDb") // This requires the Microsoft.EntityFrameworkCore.InMemory package  
                .Options;

            return new TFDataContext(options);
        }

        [TestMethod]
        public async Task Register_ShouldReturnCreated_WhenModelIsValid()
        {
            // Arrange  
            var context = GetInMemoryDbContext();
            var controller = new AccountController();

            var model = new RegisterUserViewModel
            {
                Name = "Test User",
                Email = "test@example.com",
                Password = "Password123!",
                Slug = "test-user",
                Image = "https://example.com/image.png"
            };

            // Act  
            var result = await controller.Register(model, context) as CreatedResult;

            // Assert  
            Assert.IsNotNull(result);
            Assert.AreEqual(201, result.StatusCode);

            var resultValue = result.Value as ResultViewModel<User>;
            Assert.IsNotNull(resultValue);
            Assert.AreEqual("test@example.com", resultValue.Data.Email);
        }
    }
}
