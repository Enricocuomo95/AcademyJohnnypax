using aspWeb.Models;
using aspWeb.Repositoris;

namespace aspWeb.Services
{
    public class ProvinciaService : Iservice<Provincium>
    {
        private static IRepository<Provincium> repository;

        public ProvinciaService(IRepository<Provincium> repo)
        {
            repository = repo;
        }

        public List<Provincium> getAll()
        {
            return (repository.getAll());
        }

        public Provincium getForName(string name)
        {
            return(repository.getAll().Single(p => p.Nome == name));
        }
    }
}
