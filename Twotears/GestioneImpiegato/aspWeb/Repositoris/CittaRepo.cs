using aspWeb.Models;

namespace aspWeb.Repositoris
{
    public class CittaRepo : IRepository<Cittum>
    {
        private static ImpiegatoContext _context;

        public CittaRepo(ImpiegatoContext context)
        {
            _context = context;
        }

        public bool delete(Cittum t)
        {
            throw new NotImplementedException();
        }

        public List<Cittum> getAll()
        {
            return(_context.Citta.ToList());
        }

        public Cittum? getById(int id)
        {
            throw new NotImplementedException();
        }

        public bool insert(Cittum t)
        {
            throw new NotImplementedException();
        }

        public bool update(Cittum t)
        {
            throw new NotImplementedException();
        }

        public List<Cittum> getCittaForProvincia(String Provincia)
        {
            return(_context.Citta.Where(c => c.ProvinciaRifNavigation.Nome.Equals(Provincia)).ToList());           
        }
    }
}
