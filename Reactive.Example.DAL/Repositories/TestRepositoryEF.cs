using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Reactive.Example.Common.Entities;
using Reactive.Example.Common.Interfaces.DAL;

namespace Reactive.Example.DAL.Repositories
{
    public class TestRepositoryEF: ITestRepository
    {
        private readonly IConfiguration _configuration;

        public TestRepositoryEF(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        
        public async Task Insert(object valueX, object valueY)
        {
            try
            {
                using (var ctx = TestContext.CreateContext(_configuration.GetConnectionString("Sqlite")))
                {
                    ctx.Catalogues.Add(new Catalogue((int)valueX, (int)valueY));
                    await ctx.SaveChangesAsync();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<List<Catalogue>> GetTopCatalogues(int limit = 10)
        {
            try
            {
                using (var ctx = TestContext.CreateContext(_configuration.GetConnectionString("Sqlite")))
                {
                    var result = await ctx.Catalogues.OrderByDescending(x => x.Timestamp).Take(limit).ToListAsync();
                    Console.WriteLine($"{result[0].X}-{result[0].Y}-{result[0].Timestamp}");
                    return result;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }

    public class TestContext : DbContext
    {
        private readonly string _connectionString;
        
        public DbSet<Catalogue> Catalogues { get; set; }

        public TestContext(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("Sqlite");
        }

        public TestContext(string connectionString)
        {
            _connectionString = connectionString;
        }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
//            optionsBuilder.UseSqlite(_connectionString, options =>
//            {
////                options.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName);
//                
//            });

            optionsBuilder.UseSqlite(_connectionString);

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Map table names
            modelBuilder.Entity<Catalogue>(entity =>
            {
                entity.HasKey(e => e.Id);
            });

            base.OnModelCreating(modelBuilder);
        }

        public static TestContext CreateContext(string connectionString)
        {
            return new TestContext(connectionString);
        }
    }
}