using System.Collections.Generic;
using System.Threading.Tasks;
using Backend.Models;

namespace Backend.Interfaces
{
    public interface IGuiaRepository
    {
        void Save(Guia guia);
        Task<List<Guia>> GetAsync();
        Task<Guia> GetAsync(int id);
    }
}