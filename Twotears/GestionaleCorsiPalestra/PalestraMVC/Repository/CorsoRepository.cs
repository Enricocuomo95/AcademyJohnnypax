using PalestraMVC.Models;

namespace PalestraMVC.Repository
{
    public class CorsoRepository : IRepository<Corso>
    {
        private static PalestraContext _context;

        public CorsoRepository(PalestraContext context)
        {
            _context = context;
        }
        public List<Corso> findAll()
        {
            return(_context.Corsos.ToList());
        }

        public bool insert(Corso entity)
        {
            return (true);
        }

        public bool update(Corso entity)
        {
            bool risultato = false;
            try
            {
                _context.Corsos.Update(entity);
                _context.SaveChanges();
                risultato = true;

            }catch(Exception ex) 
            {
                Console.WriteLine(ex.Message);
            }

            return(risultato);

        }
    }
}
