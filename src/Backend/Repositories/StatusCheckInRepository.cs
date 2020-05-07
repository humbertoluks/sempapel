using System.Collections.Generic;
using System.Linq;
using Backend.Data;
using Backend.Models;

namespace Backend.Repositories
{
    public class StatusCheckInRepository
    {
        private readonly DataContext _context;

        public StatusCheckInRepository(DataContext context)
        {
            _context = context;
        }
        public List<StatusCheckIn> Get()
        {
            return _context.GuiaStatusCheckIns.ToList(); 
        }
    }
} 