using VideoClub.Persistence.EF;
using Xunit;

namespace VideoClub.Test.Tools.Infrastructure.DatabaseConfig.IntegrationTest
{
    [Collection(nameof(ConfigurationFixture))]
    public class EFDataContextDatabaseFixture : DatabaseFixture
    {
        protected static EFDataContext CreateDataContext()
        {
            var connectionString =
                new ConfigurationFixture().Value.DbConnectionString;

            return new EFDataContext(connectionString);
        }
    }
}
