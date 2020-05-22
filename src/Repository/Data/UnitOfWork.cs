using System.Threading.Tasks;
using Repository.Interfaces;

namespace Repository.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _context;

        public UnitOfWork(DataContext context)
        {
            _context = context;
        }
        public void Commit(){
            _context.SaveChanges();
        }

        public async Task<int> CommitAsync(){
           return await _context.SaveChangesAsync();
        }
        public void Rollback(){}
    }
}