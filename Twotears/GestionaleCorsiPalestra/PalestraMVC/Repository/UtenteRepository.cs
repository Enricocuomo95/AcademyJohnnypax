using Microsoft.IdentityModel.Tokens;
using PalestraMVC.Models;

namespace PalestraMVC.Repository
{
    public class UtenteRepository : IRepository<Utente>
    {
        private static PalestraContext _context;

        public UtenteRepository(PalestraContext context)
        {
            _context = context;
        }

        public List<Utente> findAll()
        {
            return (_context.Utentes.ToList());
        }

        public bool insert(Utente entity)
        {
            bool ris = false;
            try
            {
                _context.Utentes.Add(entity);
                _context.SaveChanges();
                ris = true;

            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return ris;
        }

        public bool update(Utente entity)
        {
            bool risultato = false;
            try
            {
                _context.Utentes.Update(entity);
                _context.SaveChanges();
                risultato = true;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return (risultato);

        }
    }
}
