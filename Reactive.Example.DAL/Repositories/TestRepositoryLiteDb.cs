using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LiteDB;
using Microsoft.Extensions.Configuration;
using Reactive.Example.Common.Entities;
using Reactive.Example.Common.Interfaces.DAL;

namespace Reactive.Example.DAL.Repositories
{
    public class TestRepositoryLiteDb: ITestRepository
    {
        private readonly IConfiguration _configuration;

        public TestRepositoryLiteDb(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        
        public Task Insert(object valueX, object valueY)
        {
            using (var ctx = new LiteDatabase(_configuration.GetConnectionString("LiteDb")))
            {
                var collection = ctx.GetCollection<Catalogue>("catalogue");
                collection.Insert(new Catalogue((int) valueX, (int) valueY));
            }
            
            return Task.CompletedTask;
        }

        public async Task<List<Catalogue>> GetTopCatalogues(int limit = 10)
        {
            using (var ctx = new LiteDatabase(_configuration.GetConnectionString("LiteDb")))
            {
                var collection = ctx.GetCollection<Catalogue>("catalogue");
                var result = collection.FindAll().OrderByDescending(x => x.Timestamp).Take(10).ToList();

                Console.WriteLine($"{result[0].X}-{result[0].Y}-{result[0].Timestamp}");
                
                return result;
            }
        }
    }
}