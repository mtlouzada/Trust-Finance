using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TF.Models;
using Trust_Finance.Tests.Fixtures;

namespace Trust_Finance.Tests.Services
{
    public class TransactionServiceTests
    {
        [Fact]
        public async Task Create_Should_Add_Transaction_For_User()
        {
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

            var transaction = await service.CreateAsync(
                user.Id,
                category.Id,
                "Almoço",
                50,
                DateTime.UtcNow
            );

            transaction.Should().NotBeNull();
            transaction.Amount.Should().Be(50);
            context.Transactions.Should().HaveCount(1);
        }

    }
}
