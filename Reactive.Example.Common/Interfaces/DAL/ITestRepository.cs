using System.Threading.Tasks;

namespace Reactive.Example.Common.Interfaces.DAL
{
    public interface ITestRepository
    {
        Task Insert(object valueX, object valueY);
    }
}