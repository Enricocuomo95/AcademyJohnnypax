using MarioKart.DTO;

namespace MarioKart.Services
{
    public interface IGiocatore
    {
        //devo aggiungere al mio s.o. 3 player. Fino ad allora il gioco non inizia
        //mi dichiaro un indice statico che una volta a tre non accetta più utenti 
        //e passa a inserisci personaggi
        //AggiungiPlayer sarà int ritorna: -1 'errore', 0 'non puoi più inserire', 1 'ok'
        public int addPlayer(GiocatoreDTO giocatore);
        //devo permettere ad un player di vedere il profilo ps dei suoi avversari
        public List<GiocatoreDTO> getAvversari(GiocatoreDTO ioPlayer);
        //update me ne sbatte il cazzo
        //delete se il player vuole uscire dalla partita
        //il metodo sarà bool per gestire gli eventuali errori
        public bool delete(GiocatoreDTO ioPlayer); 
    }
}
