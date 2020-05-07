using Repository.Data;
using Domain;
using System.Collections.Generic;
using System.Linq;

namespace Repository
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