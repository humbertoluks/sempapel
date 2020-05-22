using Repository.Data;
using Repository.Interfaces;
using Domain;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace Repository
{
    public class GuiaRepository : IGuiaRepository
    {
        private readonly DataContext _context;

        public GuiaRepository(DataContext context)
        {
            _context = context;
        }
        public void Save(Guia guia){
            _context.Guias.Add(guia);
        }

        public async Task<IEnumerable<Guia>> GetAsync()
        {
            return await _context.Guias
                .AsNoTracking()
                .Include(m => m.GuiaStatus)
                .Include(m => m.GuiaTipo)
                .Include(m => m.StatusCheckIn)
                .OrderByDescending(o => o.Data).Take(100)
                .ToListAsync();
        }

        public async Task<Guia> GetAsync(int id)
        {
            return await _context.Guias
                .AsNoTracking()
                .Include(m => m.GuiaStatus)
                .Include(m => m.GuiaTipo)
                .Include(m => m.StatusCheckIn)
                .FirstOrDefaultAsync(x => x.Id == id);
        }
    }
} 