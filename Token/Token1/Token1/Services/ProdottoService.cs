using Token1.Model;
using Token1.Repository;

namespace Token1.Services
{
    public class ProdottoService
    {
        private readonly IRepo<Prodotto> repository;

        public ProdottoService(IRepo<Prodotto> repository)
        {
            this.repository = repository;
        }

        public List<Prodotto> getAll()
        {
            return (this.repository.GetAll());
        }
    }
}
