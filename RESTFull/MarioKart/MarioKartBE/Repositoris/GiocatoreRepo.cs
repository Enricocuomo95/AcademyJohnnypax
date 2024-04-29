using MarioKart.Models;

namespace MarioKart.Repositoris
{
    public class GiocatoreRepo: IRepository<Giocatore>
    {
        private readonly MarioKartContext _context;
        Random rnd;
        public GiocatoreRepo(MarioKartContext context) {
            this._context = context;
            this.rnd = new Random();
        }

        public bool Delete(Giocatore entity)
        {
            bool risposta = false;
            try
            {
                _context.giocatori.Remove(entity);
                _context.SaveChanges();
                risposta = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return risposta;
        }

        public IEnumerable<Giocatore> getAll()
        {
            return (_context.giocatori);
        }

        public Giocatore? getForName(string username)
        {
            return (_context.giocatori.SingleOrDefault(g => g.Username == username));
        }

        public bool Insert(Giocatore entity)
        {
            bool risposta = false;
            try
            {
                Giocatore player = _context.giocatori.Single(g => g.GiocatoreID == 1);
                player.Username = entity.Username;
                player.Passward = entity.Passward;
                player.Nominativo = entity.Nominativo;
                player.GiocatoreID = this.rnd.Next();
                _context.giocatori.Add(player);
                _context.SaveChanges();
                risposta = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return risposta;
        }

        public bool Update(Giocatore entity)
        {
            bool risposta = false;
            try
            {
                _context.giocatori.Update(entity);
                _context.SaveChanges();
                risposta = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return risposta;
        }

        /*public static GiocatoreRepo getIstanza()
        {
            if(istanza == null) 
                istanza = new GiocatoreRepo();
            return istanza;
        }
        
        lo lascio gestire al mio framework, non ho bisogno di singleton
        quando posso definire i valori di skope
         
         */


    }
}
