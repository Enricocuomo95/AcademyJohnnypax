using aspWeb.Models;
using aspWeb.Repositoris;

namespace aspWeb.Services
{
    public class RepartoService : Iservice<Reparto>
    {
        private static IRepository<Reparto> repository;

        public RepartoService(IRepository<Reparto> repo)
        {
            repository = repo;
        }

        public List<Reparto> getAll()
        {
            return (repository.getAll());
        }
    }
}
