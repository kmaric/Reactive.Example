using System.Collections.Generic;
using System.Threading.Tasks;
using Reactive.Example.Common.Entities;

namespace Reactive.Example.Common.Interfaces.DAL
{
    public interface ITestRepository
    {
        Task Insert(object valueX, object valueY);
        Task<List<Catalogue>> GetTopCatalogues(int limit = 10);
    }
}