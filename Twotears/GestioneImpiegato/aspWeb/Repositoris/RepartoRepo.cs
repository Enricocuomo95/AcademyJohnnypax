
using aspWeb.Models;

namespace aspWeb.Repositoris
{
    public class RepartoRepo : IRepository<Reparto>
    {
        private static ImpiegatoContext _context;

        public RepartoRepo(ImpiegatoContext context)
        {
            _context = context;
        }
        public bool delete(Reparto t)
        {
            throw new NotImplementedException();
        }

        public List<Reparto> getAll()
        {
            return(_context.Repartos.ToList());
        }

        public Reparto? getById(int id)
        {
            throw new NotImplementedException();
        }

        public bool insert(Reparto t)
        {
            throw new NotImplementedException();
        }

        public bool update(Reparto t)
        {
            throw new NotImplementedException();
        }
    }
}
