using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.EF;

namespace DAL.Repos
{
    public class Repository
    {
        protected NewsContext _context;
        public Repository()
        {
            _context = new NewsContext();
        }
    }
}
