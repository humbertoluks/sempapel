using System.Collections.Generic;
using System.Linq;
using Backend.Data;
using Backend.Models;

namespace Backend.Repositories
{
    public class GuiaStatusRepository
    {
        private readonly DataContext _context;

        public GuiaStatusRepository(DataContext context)
        {
            _context = context;
        }
        public List<GuiaStatus> Get()
        {
            return _context.GuiaStatus.ToList(); 
        }
    }
} 