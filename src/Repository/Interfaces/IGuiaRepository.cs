using System.Collections.Generic;
using System.Threading.Tasks;
using Domain;

namespace Repository.Interfaces
{
    public interface IGuiaRepository
    {
        void Save(Guia guia);
        Task<IEnumerable<Guia>> GetAsync();
        Task<Guia> GetAsync(int id);
    }
}
