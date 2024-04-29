using Abbigliamento.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abbigliamento.DAL
{
    internal class UtenteDAL : Icrud<Utente>
    {
        private static UtenteDAL istanza;

        private UtenteDAL() { }

        public static UtenteDAL getIstanza()
        {
            if(istanza == null)
                istanza = new UtenteDAL();
            return istanza;
        }
        public bool CreateInsertValue(Utente entity)
        {
            bool risultato = false;

            using (NegozioAbbigliamentoContext context = new NegozioAbbigliamentoContext())
            {
                try
                {
                    context.Utentes.Add(entity);
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

        public bool DeleteValue(Utente entity)
        {
            bool risultato = false;

            using (NegozioAbbigliamentoContext context = new NegozioAbbigliamentoContext())
            {
                try
                {
                    if (context.Utentes.Contains(entity))
                    {
                        context.Utentes.Remove(entity);
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

        public List<Utente> ReadGetValue()
        {
            List<Utente> risposta = new List<Utente>();
            using (NegozioAbbigliamentoContext context = new NegozioAbbigliamentoContext())
            {
                try
                {
                    risposta = context.Utentes.ToList();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            return (risposta);
        }

        public bool UpdateValue(Utente entity)
        {
            bool risultato = false;

            using (NegozioAbbigliamentoContext context = new NegozioAbbigliamentoContext())
            {
                try
                {
                    var itemToRemove = context.Utentes.SingleOrDefault(x => x.IdUtente == entity.IdUtente); //returns a single item.

                    if (itemToRemove != null)
                    {
                        context.Utentes.Remove(itemToRemove);
                        context.Utentes.Add(entity);
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
