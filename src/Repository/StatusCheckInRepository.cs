using Repository.Data;
using Domain;
using System.Collections.Generic;
using System.Linq;


namespace Repository
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