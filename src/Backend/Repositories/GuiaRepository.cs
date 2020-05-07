using Backend.Data;
using Backend.Interfaces;
using Backend.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Repositories
{
    public class GuiaRepository : IGuiaRepository
    {
        private readonly DataContext _context;

        public GuiaRepository(DataContext context)
        {
            _context = context;
        }
        public void Save(Guia guia){
            guia.GuiaStatus = _context.GuiaStatus.First(x => x.Id == guia.GuiaStatusId);  
            guia.GuiaTipo = _context.GuiaTipos.First(x => x.Id == guia.GuiaTipoId);  
            guia.StatusCheckIn = _context.GuiaStatusCheckIns.First(x => x.Id == guia.StatusCheckInId);  
            
            _context.Guias.Add(guia);
        }

        public async Task<List<Guia>> GetAsync()
        {
            return await _context.Guias
                .AsNoTracking()
                .Include(m => m.GuiaStatus)
                .Include(m => m.GuiaTipo)
                .Include(m => m.StatusCheckIn)
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