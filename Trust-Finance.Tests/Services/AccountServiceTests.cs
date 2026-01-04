using FluentAssertions;
using TF.Data;
using TF.Models;
using TF.Services;
using TF.ViewModels;
using Trust_Finance.Tests.Fixtures;
using Xunit;

namespace Trust_Finance.Tests.Services;

public class AccountServiceTests
{
    [Fact]
    public async Task Register_Should_Create_User_With_Hashed_Password()
    {
        // Arrange (preparação)
        var context = DbContextFixture.CreateContext(Guid.NewGuid().ToString());
        var service = new AccountService(context);

        var model = new RegisterUserViewModel
        {
            Name = "Teste",
            Email = "teste@gmail.com",
            Password = "teste123",
            Image = "img",
            Slug = "teste"
        };

        // Act (ação)
        var user = await service.RegisterAsync(model);

        // Assert (verificação)
        user.Should().NotBeNull();
        user.Email.Should().Be("teste@gmail.com");
        user.PasswordHash.Should().NotBe("teste123");

        context.Users.Should().HaveCount(1);
    }

    [Fact]
public async Task Register_Should_Fail_When_Email_Already_Exists()
{
    // Arrange
    var context = DbContextFixture.CreateContext(Guid.NewGuid().ToString());
    var service = new AccountService(context);

    var model = new RegisterUserViewModel
    {
        Name = "Teste",
        Email = "duplicado@gmail.com",
        Password = "123456",
        Image = "img",
        Slug = "teste"
    };

    await service.RegisterAsync(model);

    // Act
    Func<Task> action = async () =>
        await service.RegisterAsync(model);

    // Assert
    await action.Should().ThrowAsync<Exception>();
}



}
