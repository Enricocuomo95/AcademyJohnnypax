using aspWeb.Models;
using aspWeb.Repositoris;

namespace aspWeb.Services
{
    public class CittaService : Iservice<Cittum>
    {
        private static CittaRepo repository;

        public CittaService(CittaRepo repo)
        {
            repository = repo;
        }

        public List<Cittum> getAll()
        {
            return (repository.getAll());
        }

        public List<Cittum> getCittaForProvincia(String Provincia)
        {
            return (repository.getCittaForProvincia(Provincia));
        }
    }
}
