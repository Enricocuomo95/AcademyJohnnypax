using aspWeb.Models;

namespace aspWeb.Repositoris
{
    public class ProvinciaRepo : IRepository<Provincium>
    {
        private static ImpiegatoContext _context;

        public ProvinciaRepo(ImpiegatoContext context)
        {
            _context = context;
        }

        public bool delete(Provincium t)
        {
            throw new NotImplementedException();
        }

        public List<Provincium> getAll()
        {
            return _context.Provincia.ToList();
        }

        public Provincium? getById(int id)
        {
            throw new NotImplementedException();
        }

        public bool insert(Provincium t)
        {
            throw new NotImplementedException();
        }

        public bool update(Provincium t)
        {
            throw new NotImplementedException();
        }
    }
}
