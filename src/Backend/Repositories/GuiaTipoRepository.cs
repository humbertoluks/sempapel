using System.Collections.Generic;
using System.Linq;
using Backend.Data;
using Backend.Models;

namespace Backend.Repositories
{
    public class GuiaTipoRepository
    {
        private readonly DataContext _context;

        public GuiaTipoRepository(DataContext context)
        {
            _context = context;
        }
        public List<GuiaTipo> Get()
        {
            return _context.GuiaTipos.ToList(); 
        }
    }
} 