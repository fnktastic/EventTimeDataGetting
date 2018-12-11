using GettingOffBoxData.DataAccess;
using GettingOffBoxData.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GettingOffBoxData.Repository
{
    public class ReadRepository : IReadRepository
    {
        private readonly Context _context;

        public ReadRepository(Context context)
        {
            _context = context;
        }

        public IEnumerable<Read> Reads => _context.Reads;

        public void SaveRead(Read read)
        {
            _context.Reads.Add(read);
            _context.SaveChangesAsync();
        }
    }
}
