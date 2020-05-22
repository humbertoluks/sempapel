using System.Threading.Tasks;

namespace ConsoleApp.Helpers.Interfaces
{
    public interface IHelperService
    {
        Task PerformService(string schedule);
    }
}