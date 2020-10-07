using System.Collections.Generic;
using System.Threading.Tasks;
using Domain;

namespace Repository.Interfaces
{
    public interface IGuiaRepository
    {
        void Delete(int IdGuiaExterno);
        void Save(Guia guia);
        void Update(Guia guia);
        Task<IEnumerable<Guia>> GetAsync();
        Task<Guia> GetAsync(int id);
        Task<Guia> GetByIdExternoAsync (int IdExterno);
    }
}
