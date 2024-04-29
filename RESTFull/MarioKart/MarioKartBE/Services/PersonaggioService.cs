using MarioKart.DTO;
using MarioKart.Models;
using MarioKart.Repositoris;
using System;
using System.Collections.Generic;
using System.Numerics;

namespace MarioKart.Services
{
    public class PersonaggioService : IPersonaggio
    {
        private IRepository<Personaggio> repositoryPersonaggio;
        private IRepository<Giocatore> repositoryPlayers;
        private Dictionary<String, int> creditiResidui;

        public PersonaggioService (IRepository<Personaggio> repository, IRepository<Giocatore> repositoryPlayers)
        {
            this.repositoryPersonaggio = repository;
            this.repositoryPlayers = repositoryPlayers;
            this.creditiResidui = new Dictionary<string, int>();
        }

        public int addPersonaggio(PersonaggioDTO personaggio, GiocatoreDTO giocatore)
        {
            Giocatore player = this.repositoryPlayers.getForName(giocatore.Username);
            if (player == null)
                //non va bene il mio player non è registrato al server
                //probabbilmente ho perso dei dati!!
                //errore
                return (-1);
            List<Personaggio> listaPersonaggiForPlayer = (List < Personaggio > )this.repositoryPersonaggio.getAll().Select(p => p.Giocatore != null && p.Giocatore.Username == giocatore.Username);
            if ((listaPersonaggiForPlayer != null) && (listaPersonaggiForPlayer.Count() == 3))
                return (1); //squadra completa

            Personaggio p = repositoryPersonaggio.getForName(personaggio.Nome);
            if (p == null) return -1; //errore
            p.Giocatore = player;
            p.Disponibile = false;

            if (!this.creditiResidui.ContainsKey(giocatore.Username)) 
            {
                //il mio giocatore non ha ancora nessuno in squadra
                if (repositoryPersonaggio.Update(p))
                {
                    this.creditiResidui.Add(giocatore.Username, 10 - Convert.ToInt32(personaggio.Costo));
                    return (2);
                }

                return -1;
            }

            if (this.creditiResidui.GetValueOrDefault(giocatore.Username) - Convert.ToInt32(personaggio.Costo) >= 0)
            {
                if (repositoryPersonaggio.Update(p))
                {
                    this.creditiResidui[giocatore.Username] = this.creditiResidui[giocatore.Username] - Convert.ToInt32(personaggio.Costo);
                    return (2);
                }

                return -1;

            }

            //hai esaurito i crediti
            return 0;
        }

        public List<PersonaggioDTO> getPersonaggi()
        {
            List<PersonaggioDTO> risultato = new List<PersonaggioDTO>();
            foreach(Personaggio p in repositoryPersonaggio.getAll())
            {
                risultato.Add(new PersonaggioDTO()
                {
                    Categoria = p.Categoria,
                    Costo = p.Costo,
                    Disponibile = p.Disponibile,
                    Giocatore = p.Giocatore,
                    Nome = p.Nome
                });
            }
            return risultato;
        }

        public bool removePersonaggio(PersonaggioDTO personaggio, GiocatoreDTO giocatore)
        {
            //caso base
            if ((!this.creditiResidui.ContainsKey(giocatore.Username)))
                return (false);

            //setto di nuovo il mio personaggio
            //possiamo usae una funzione privata (better!!!)
            Personaggio p = repositoryPersonaggio.getForName(personaggio.Nome);
            if (p == null) return false; //errore
            p.Giocatore = null;
            p.Disponibile = true;

            if (repositoryPersonaggio.Update(p))
            {
                this.creditiResidui[giocatore.Username] = this.creditiResidui[giocatore.Username] + Convert.ToInt32(personaggio.Costo);
                return (true);
            }

            return (false);
        }
    }
}
