using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface IUnitOfWork
    {
        void Commit();
        Task<int> CommitAsync();
        void Rollback();
    }
}