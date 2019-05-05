using Microsoft.Extensions.Configuration;
using Reactive.Example.Common.Interfaces.DAL;

namespace Reactive.Example.DAL.Repositories
{
    public class TestRepositoryDapper: ITestRepository
    {
        public readonly string _connectionString;
        public TestRepositoryDapper(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("Sqlite");
        }
        
        
    }
}