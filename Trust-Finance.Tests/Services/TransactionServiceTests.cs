using System;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using TF.Models;
using Trust_Finance.Services;
using Trust_Finance.Tests.Fixtures;
using Xunit;

namespace Trust_Finance.Tests.Services;

public class TransactionServiceTests
{
    [Fact]
    public async Task Create_Should_Add_Transaction_For_User()
    {
        // Arrange
        var context = DbContextFixture.CreateContext(Guid.NewGuid().ToString());

        var user = new User
        {
            Name = "Teste",
            Email = "teste@gmail.com",
            PasswordHash = "hash",
            Role = "admin"
        };

        var category = new Category
        {
            Name = "Alimentação",
            Slug = "alimentacao"
        };

        context.Users.Add(user);
        context.Categories.Add(category);
        await context.SaveChangesAsync();

        var service = new TransactionService(context);

        // Act
        var transaction = await service.CreateAsync(
            description: "Almoço",
            amount: 50,
            date: DateTime.UtcNow,
            categoryId: category.Id,
            userId: user.Id
        );

        // Assert
        transaction.Should().NotBeNull();
        transaction.Description.Should().Be("Almoço");
        transaction.Amount.Should().Be(50);
        transaction.UserId.Should().Be(user.Id);

        context.Transactions.Count().Should().Be(1);
    }
}
