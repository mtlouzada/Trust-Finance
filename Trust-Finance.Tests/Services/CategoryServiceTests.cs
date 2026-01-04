using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trust_Finance.Tests.Fixtures;

namespace Trust_Finance.Tests.Services
{
    public class CategoryServiceTests
    {

        [Fact]
        public async Task Create_Should_Add_Category()
        {
            var context = DbContextFixture.CreateContext(Guid.NewGuid().ToString());
            var service = new CategoryService(context);

            var category = await service.CreateAsync("Alimentação", "alimentacao");

            category.Should().NotBeNull();
            category.Name.Should().Be("Alimentação");

            context.Categories.Should().HaveCount(1);
        }

        [Fact]
        public async Task Create_Should_Fail_When_Slug_Is_Duplicated()
        {
            var context = DbContextFixture.CreateContext(Guid.NewGuid().ToString());
            var service = new CategoryService(context);

            await service.CreateAsync("Alimentação", "alimentacao");

            Func<Task> action = async () =>
                await service.CreateAsync("Outra", "alimentacao");

            await action.Should().ThrowAsync<Exception>();
        }


    }
}
