using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bio.io.DAL
{
    public class BioioRepository
    {
        public BioioContext Context { get; set; }
        public BioioRepository(BioioContext _context)
        {
            Context = _context;
        }

        public BioioRepository()
        {
            Context = new BioioContext();
        }
    }
}