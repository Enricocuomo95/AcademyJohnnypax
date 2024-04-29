using MarioKart.DTO;
using MarioKart.Models;
using MarioKart.Repositoris;
using System.Numerics;

namespace MarioKart.Services
{
    public class GiocatoreService : IGiocatore
    {
        private static int numeroPlayers = 3;
        private IRepository<Giocatore> repository;

        public GiocatoreService(IRepository<Giocatore> repository)
        {
            this.repository = repository;
        }
        public int addPlayer(GiocatoreDTO player)
        {
            if(numeroPlayers == 0)
                return 0;
            //se il mio player ha gia giocato in passato sul mio server avrò gia sul db le sue credenziali
            Giocatore g = repository.getForName(player.Username);
            if (g == null)
                g = new Giocatore() {Nominativo = player.Nominativo, Passward = player.Passward, Username = player.Username };
            
            repository.Insert(g);
            numeroPlayers--;
            return 1;
        }

        public bool delete(GiocatoreDTO ioPlayer)
        {
            Giocatore g = repository.getForName(ioPlayer.Username);
            if (g == null) return false;
            repository.Delete(g);
            numeroPlayers++;
            return true;
        }

        public List<GiocatoreDTO> getAvversari(GiocatoreDTO ioPlayer)
        {
            List<Giocatore> listaAvversari = (List<Giocatore>)repository.getAll().Select(g => g.Username != ioPlayer.Username);
            List<GiocatoreDTO> risultato = new List<GiocatoreDTO>();

            foreach(Giocatore g in listaAvversari)
                risultato.Add(new GiocatoreDTO() { Nominativo = g.Nominativo, Passward = g.Passward, Username = g.Username });

            return risultato;
        }

        public GiocatoreDTO? getGiocatoreForName(String username)
        {
            Giocatore? g = repository.getForName(username);
            if (g == null) return null;
            GiocatoreDTO risultato = new GiocatoreDTO() { Nominativo = g.Nominativo, Passward = g.Passward, Username = username };
            return risultato;
        }
    }
}
