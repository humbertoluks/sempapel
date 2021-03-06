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

         public virtual void Update(Guia guia)
        {
            _context.Guias.Attach(guia);
            _context.Entry(guia).State = EntityState.Modified;
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

        public async Task<Guia> GetByIdExternoAsync(int idExterno)
        {
            return await _context.Guias
                .AsNoTracking()
                .Include(m => m.GuiaStatus)
                .Include(m => m.GuiaTipo)
                .Include(m => m.StatusCheckIn)
                .FirstOrDefaultAsync(x => x.IdGuiaExterno == idExterno);
        }
        public void Delete(int IdGuiaExterno)
        {
            var guias =  _context.Guias.Where(x => x.IdGuiaExterno == IdGuiaExterno);

            _context.Guias.RemoveRange(guias);
        }
    }
} 
