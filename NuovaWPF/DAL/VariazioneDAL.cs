using Abbigliamento.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abbigliamento.DAL
{
    internal class VariazioneDAL : Icrud<Variazione>
    {
        private static VariazioneDAL istanza;

        private VariazioneDAL() { }
        public static VariazioneDAL getIstanza()
        {
            if(istanza == null)
                istanza = new VariazioneDAL();
            return(istanza);
        }

        public bool CreateInsertValue(Variazione entity)
        {
            bool risultato = false;

            using (NegozioAbbigliamentoContext context = new NegozioAbbigliamentoContext())
            {
                try
                {
                    context.Variaziones.Add(entity);
                    context.SaveChanges();
                    risultato = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            return (risultato);
        }

        public bool DeleteValue(Variazione entity)
        {
            bool risultato = false;

            using (NegozioAbbigliamentoContext context = new NegozioAbbigliamentoContext())
            {
                try
                {
                    if (context.Variaziones.Contains(entity))
                    {
                        context.Variaziones.Remove(entity);
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

        public bool DeleteValueForId(int id)
        {
            bool risultato = false;

            using (NegozioAbbigliamentoContext context = new NegozioAbbigliamentoContext())
            {
                try
                {
                    var itemToRemove = context.Variaziones.SingleOrDefault(x => x.IdVariazione == id); //returns a single item.

                    if (itemToRemove != null)
                    {
                        context.Variaziones.Remove(itemToRemove);
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

        public List<Variazione> ReadGetValue()
        {
            List<Variazione> risposta = new List<Variazione>();
            using (NegozioAbbigliamentoContext context = new NegozioAbbigliamentoContext())
            {
                try
                {
                    risposta = context.Variaziones.ToList();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            return (risposta);
        }

        public Variazione getVariazione(Variazione x)
        {
            Variazione risposta = new Variazione();
            using (NegozioAbbigliamentoContext context = new NegozioAbbigliamentoContext())
            {
                try
                {
                    List<Variazione> lista = ReadGetValue();
                    int i = 0;

                    while ((i < lista.Count()) && (!SieteUguali(lista[i], x))) i++;

                    if (i == lista.Count())
                        risposta = null;
                    else
                        risposta = lista[i];
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            return (risposta);
        }
        public Variazione getVariazioneForId(int id)
        {
            Variazione? risposta = new Variazione();
            using (NegozioAbbigliamentoContext context = new NegozioAbbigliamentoContext())
            {
                try
                {
                    List<Variazione> lista = ReadGetValue();
                    int i = 0;


                    //potrei usare una arrow function e poi convertire il var
                    //è più veloce la conversione per n elementi o la ricerca con while?
                    //la ricerca con while è più efficiente di un for/foreach perchè so esattamente che una 
                    //volta trovato l'id mi devo fermare. NON scorro tutta la lista 
                    //se le mie t-uple crescono potrebbe essere meglio la arrow function 
                    // da valutare...
                    while ((i < lista.Count()) && (lista[i].IdVariazione != id)) i++;

                    if (i == lista.Count())
                        risposta = null;
                    else
                        risposta = lista[i];
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            return (risposta);
        }

        private bool SieteUguali(Variazione x, Variazione y)
        {
            if ((x.Quantita == y.Quantita) && (x.Taglia.Equals(y.Taglia)) && (x.Colore.Equals(y.Colore)) && (x.IdVariazione == y.IdVariazione))
                return (true);
            return (false);
        }

        public List<Variazione> getVariazioneForProdotto(Prodotto x)
        {
            List<Variazione> risposta = new List<Variazione>();
            using (NegozioAbbigliamentoContext context = new NegozioAbbigliamentoContext())
            {
                try
                {
                    foreach (Variazione v in context.Variaziones.ToList())
                        if (v.ProdottoRif == x.IdProdotto)
                            risposta.Add(v);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            return (risposta);
        }

        public bool UpdateValue(Variazione entity)
        {
            bool risultato = false;

            using (NegozioAbbigliamentoContext context = new NegozioAbbigliamentoContext())
            {
                try
                {
                    var itemToRemove = context.Variaziones.SingleOrDefault(x => x.IdVariazione == entity.IdVariazione); //returns a single item.

                    if (itemToRemove != null)
                    {
                        context.Variaziones.Remove(itemToRemove);
                        context.Variaziones.Add(entity);
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
