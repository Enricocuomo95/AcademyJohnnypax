
using MarioKart.Models;

namespace MarioKart.Repositoris
{
    public class PersonaggioRepo : IRepository<Personaggio>
    {
        private readonly MarioKartContext _context;

        public PersonaggioRepo(MarioKartContext context)
        {
            this._context = context;

        }
        public bool Delete(Personaggio entity)
        {
            bool risposta = false;
            try
            {
                _context.personaggi.Remove(entity);
                _context.SaveChanges();
                risposta = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return risposta;
        }

        public IEnumerable<Personaggio> getAll()
        {
            return (_context.personaggi.ToList());
        }

        public Personaggio? getForName(string nome)
        {
            return (_context.personaggi.SingleOrDefault(p => p.Nome == nome));
        }

        public bool Insert(Personaggio entity)
        {
            bool risposta = false;
            try
            {
                _context.personaggi.Add(entity);
                _context.SaveChanges();
                risposta = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return risposta;
        }

        public bool Update(Personaggio entity)
        {
            bool risposta = false;
            try
            {
                _context.personaggi.Update(entity);
                _context.SaveChanges();
                risposta = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return risposta;
        }
    }
}
