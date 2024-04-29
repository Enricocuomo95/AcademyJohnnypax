using aspWeb.Models;

namespace aspWeb.Repositoris
{
    public class ImpiegatoRepo : IRepository<Impiegato>
    {
        private static ImpiegatoContext _context;

        public ImpiegatoRepo( ImpiegatoContext context )
        {
            _context = context;
        }
        public bool delete(Impiegato t)
        {
            throw new NotImplementedException();
        }

        public List<Impiegato> getAll()
        {
           return(_context.Impiegatos.ToList());   

        }

        public Impiegato? getById(int id)
        {
            return( _context.Impiegatos.FirstOrDefault(x => x.ImpiegatoId == id));
        }

        public bool insert(Impiegato t)
        {
            bool risposta = false;
            try
            {
                _context.Impiegatos.Add(t);
                _context.SaveChanges();
                risposta = true;

            }catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return risposta;
        }

        public bool update(Impiegato t)
        {
            bool risposta = false;
            try
            {
                _context.Impiegatos.Update(t);
                _context.SaveChanges();
                risposta = true;

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return risposta;
        }
    }
}
