using Microsoft.EntityFrameworkCore;
using TF.Data;

namespace Trust_Finance.Tests.Fixtures;

public static class DbContextFixture
{
    public static TFDataContext CreateContext(string databaseName)
    {
        var options = new DbContextOptionsBuilder<TFDataContext>()
            .UseInMemoryDatabase(databaseName)
            .Options;

        return new TFDataContext(options);
    }
}
