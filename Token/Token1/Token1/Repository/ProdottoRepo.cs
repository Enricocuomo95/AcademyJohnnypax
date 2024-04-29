using Token1.Model;

namespace Token1.Repository
{
    public class ProdottoRepo : IRepo<Prodotto>
    {
        private readonly FerramentaContext _context;
        public ProdottoRepo(FerramentaContext context) 
        { 
            _context = context;
        }
        public bool deleteValue(int id)
        {
            throw new NotImplementedException();
        }

        public List<Prodotto> GetAll()
        {
            return (_context.Prodottos.ToList());
        }

        public Prodotto GetValue(int id)
        {
            throw new NotImplementedException();
        }

        public bool insertValue(Prodotto value)
        {
            throw new NotImplementedException();
        }

        public bool updateValue(Prodotto value)
        {
            throw new NotImplementedException();
        }
    }
}
