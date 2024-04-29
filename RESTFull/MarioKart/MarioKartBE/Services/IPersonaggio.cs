using MarioKart.DTO;

namespace MarioKart.Services
{
    public interface IPersonaggio
    {
        //il mio player una volta sottoscritto al gioco sceglie i personaggi
        //i personaggi devono essere tre per ogni giocatore
        //metto un contatore statico che a tre ferma l'inserimento per ogni singolo player
        //inoltre la somma del costo dei tre deve essere <=10
        //di conseguenza mi servono nel mio PersonaggioService un dictionary<username,crediti>
        //il seguente metodo restituisce: -1 errore di connessione
        //il seguente metodo restituisce: 0 crediti non sufficienti
        //il seguente metodo restituisce: 1 la tua squadraè completa
        //il seguente metodo restituisce: 2 successo
        //non gestisco i controlli sui campi costo e categoria
        //mi sto preoccupando solo della gestione del gioco. Quelli li inserisco dal db
        public int addPersonaggio(PersonaggioDTO personaggio,GiocatoreDTO giocatore);
        public List<PersonaggioDTO> getPersonaggi();
        //per le raggioni sopra citate l'update e la delete non esiste
        //Quando aggiungo il personaggio al mio player devo settare disponibile a false!!!!!
        //devo permettere al mio giocatore di ps di tornare indietro e deselezionae quindi un personaggio
        //il seguente metodo fa esattamente l'inverso di addPersonaggio
        public bool removePersonaggio(PersonaggioDTO personaggio, GiocatoreDTO giocatore);

    }
}
