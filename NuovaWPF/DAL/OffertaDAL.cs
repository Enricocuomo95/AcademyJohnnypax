using Abbigliamento.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abbigliamento.DAL
{
    internal class OffertaDAL : Icrud<Offertum>
    {
        private static OffertaDAL istanza;

        private OffertaDAL() { }
        public static OffertaDAL getIstanza()
        {
            if (istanza == null)
                istanza = new OffertaDAL();
            return(istanza);
        }
        public bool CreateInsertValue(Offertum entity)
        {
            bool risultato = false;

            using (NegozioAbbigliamentoContext context = new NegozioAbbigliamentoContext())
            {
                try
                {
                    context.Offerta.Add(entity);
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

        public bool DeleteValue(Offertum entity)
        {
            bool risultato = false;

            using (NegozioAbbigliamentoContext context = new NegozioAbbigliamentoContext())
            {
                try
                {
                    if (context.Offerta.Contains(entity))
                    {
                        context.Offerta.Remove(entity);
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
                    var itemToRemove = context.Offerta.SingleOrDefault(x => x.IdOfferta == id); //returns a single item.

                    if (itemToRemove != null)
                    {
                        context.Offerta.Remove(itemToRemove);
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

        public List<Offertum> ReadGetValue()
        {
            List<Offertum> risposta = new List<Offertum>();
            using (NegozioAbbigliamentoContext context = new NegozioAbbigliamentoContext())
            {
                try
                {
                    risposta = context.Offerta.ToList();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            return (risposta);
        }


        public Offertum getOffertaForVariazione(Variazione x)
        {
            Offertum? risposta = null;
            using (NegozioAbbigliamentoContext context = new NegozioAbbigliamentoContext())
            {
                try
                {
                    foreach(Offertum offerta in context.Offerta.ToList())
                    {
                        if (offerta.VariazioneRif == x.IdVariazione)
                            if (ValidaData(offerta))
                                risposta = offerta;
                    }
                        
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            return (risposta);
        }

        public Offertum getOffertaForId(int id)
        {
            Offertum? risposta = null;
            using (NegozioAbbigliamentoContext context = new NegozioAbbigliamentoContext())
            {
                try
                {
                    List<Offertum> lista = ReadGetValue();
                    int i = 0;

                    while ((i < lista.Count()) && (lista[i].IdOfferta != id)) i++;

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

        private bool ValidaData(Offertum offerta)
        {
            if ((offerta.DataInizio < offerta.DataFine) && (DateOnly.FromDateTime(DateTime.Now) < offerta.DataFine))
                return (true);
            return (false);
        }

        public bool UpdateValue(Offertum entity)
        {
            bool risultato = false;

            using (NegozioAbbigliamentoContext context = new NegozioAbbigliamentoContext())
            {
                try
                {
                    var itemToRemove = context.Offerta.SingleOrDefault(x => x.IdOfferta == entity.IdOfferta); //returns a single item.

                    if (itemToRemove != null)
                    {
                        context.Offerta.Remove(itemToRemove);
                        context.Offerta.Add(entity);
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
