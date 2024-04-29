using aspWeb.Models;
using aspWeb.Repositoris;

namespace aspWeb.Services
{
    public class ImpiegatoService
    {
        private static IRepository<Impiegato> repository;

        public ImpiegatoService(IRepository<Impiegato> repo)
        {
            repository = repo;
        }

        public List<Impiegato> getAll()
        {
            return (repository.getAll());
        }

        public bool insert(Impiegato impiegato)
        {
            return repository.insert(impiegato);
        }
    }
}
