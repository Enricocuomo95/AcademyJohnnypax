using Abbigliamento.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abbigliamento.DAL
{
    internal class OrdineDAL : Icrud<Ordine>
    {
        private static OrdineDAL istanza;

        private OrdineDAL() { }
        public static OrdineDAL getIstanza()
        {
            if(istanza == null)
                istanza = new OrdineDAL();
            return istanza;
        }
        public bool CreateInsertValue(Ordine entity)
        {
            bool risultato = false;
            
            using (NegozioAbbigliamentoContext context = new NegozioAbbigliamentoContext())
            {
                bool flagUtente = true;
                bool flagProdotto = true;
                try
                {
                    //i controlli sul'id sono fatti dal db per via dei vincoli di integrità referenziale che ho inserito, 
                    //ma questo metodo cerca di prevenire in modo da non scomodare il DBMS per ogni inserimento 
                    //inoltre il controllo sul prodotto è propedeutico alla disponibilita(controllo e aggiornamento dello store in magazzino)
                    //controllo se esiste l'utente
                    //se non esiste lo devi prima registrare
                    //ma non sono affari miei. Io sono il DAL di Ordine => non iserisco e ti do l'errore
                    var itemUtente = context.Utentes.SingleOrDefault(x => x.IdUtente == entity.UtenteRif); 
                    if(itemUtente==null)
                        flagUtente = false;

                    //faccio dei controlli se esiste il prodotto e se è disponbbile
                    foreach (var item in entity.ProdottoRifs)
                    {
                        Prodotto prodotto = (Prodotto) context.Prodottos.Where(x => x.IdProdotto == item.IdProdotto);
                        if (prodotto == null)
                            flagProdotto = false;
                        else 
                        {
                            Variazione v = (Variazione) context.Variaziones.Where(v => v.ProdottoRifNavigation.Equals(prodotto));
                            if((v.Quantita - 1) >= 0)
                            {
                                v.Quantita--;
                            }
                            else
                                flagProdotto = false;
                        }
                            
                    }

                    if (flagProdotto && flagUtente)
                    {
                        context.Ordines.Add(entity);
                        context.SaveChanges();
                        risultato = true;
                    }
                        
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            return (risultato);
        }

        public bool DeleteValue(Ordine entity)
        {
            bool risultato = false;

            using (NegozioAbbigliamentoContext context = new NegozioAbbigliamentoContext())
            {
                try
                {
                    if (context.Ordines.Contains(entity))
                    {
                        context.Ordines.Remove(entity);
                        context.SaveChanges();
                        risultato = true;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            return (risultato);
        }

        public List<Ordine> ReadGetValue()
        {
            List<Ordine> risposta = new List<Ordine>();
            using (NegozioAbbigliamentoContext context = new NegozioAbbigliamentoContext())
            {
                try
                {
                    risposta = context.Ordines.ToList();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            return (risposta);
        }

        public List<Ordine> GetValueForUtente(Utente u)
        {
            List<Ordine> risposta = new List<Ordine>();
            using (NegozioAbbigliamentoContext context = new NegozioAbbigliamentoContext())
            {
                try
                {
                    if (context.Ordines.Count() > 0)
                    {
                        risposta = (List<Ordine>)context.Ordines.Where(o => o.UtenteRif == u.IdUtente);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            return (risposta);
        }

        public bool UpdateValue(Ordine entity)
        {
            bool risultato = false;

            using (NegozioAbbigliamentoContext context = new NegozioAbbigliamentoContext())
            {
                try
                {
                    var itemToRemove = context.Ordines.SingleOrDefault(x => x.IdOrdine == entity.IdOrdine); //returns a single item.

                    if (itemToRemove != null)
                    {
                        context.Ordines.Remove(itemToRemove);
                        context.Ordines.Add(entity);
                        context.SaveChanges();
                        risultato = true;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            return (risultato);
        }
    }
}
