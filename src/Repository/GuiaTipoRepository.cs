using Repository.Data;
using Domain;
using System.Collections.Generic;
using System.Linq;


namespace Repository
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