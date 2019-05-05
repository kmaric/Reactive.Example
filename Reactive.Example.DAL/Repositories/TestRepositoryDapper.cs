using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Data.Sqlite;
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
        
        private IDbConnection Connection => new SqliteConnection(_connectionString);

        public async Task Insert(object valueX, object valueY)
        {
            try
            {
                using (IDbConnection conn = Connection)
                {
                    string query = "INSERT INTO catalogue (id, x_value, y_value) VALUES (@id, @X, @Y)";
                    conn.Open();
                    await conn.ExecuteAsync(query, new { id = Guid.NewGuid(), X = valueX, Y = valueY } );
//                    Console.WriteLine(Thread.CurrentThread.ManagedThreadId);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}