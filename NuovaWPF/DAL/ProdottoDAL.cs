using Abbigliamento.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abbigliamento.DAL
{
    internal class ProdottoDAL : Icrud<Prodotto>
    {
        private static ProdottoDAL istanza;

        private ProdottoDAL() { }
        public static ProdottoDAL getIstanza()
        {
            if(istanza == null)
                istanza = new ProdottoDAL();
            return istanza;
        }
        public bool CreateInsertValue(Prodotto entity)
        {
            bool risultato = false;

            using (NegozioAbbigliamentoContext context = new NegozioAbbigliamentoContext())
            {
                try
                {
                    context.Prodottos.Add(entity);
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

        public bool DeleteValue(Prodotto entity)
        {
            bool risultato = false;

            using (NegozioAbbigliamentoContext context = new NegozioAbbigliamentoContext())
            {
                try
                {
                    if (context.Prodottos.Contains(entity))
                    {
                        context.Prodottos.Remove(entity);
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

        public List<Prodotto> ReadGetValue()
        {
            List<Prodotto> risposta = new List<Prodotto>();
            using (NegozioAbbigliamentoContext context = new NegozioAbbigliamentoContext())
            {
                try
                {
                    risposta = context.Prodottos.ToList();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            return (risposta);
        }
        public Prodotto getProdotto(Prodotto x)
        {
            Prodotto? risposta = new Prodotto();
            using (NegozioAbbigliamentoContext context = new NegozioAbbigliamentoContext())
            {
                try
                {
                    List<Prodotto> lista = ReadGetValue();
                    int i = 0;

                    while ((i<lista.Count()) && (!lista[i].UrlImg.Equals(x.UrlImg))) i++;

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

        public Prodotto getProdottoForId(int id)
        {
            Prodotto? risposta = new Prodotto();
            using (NegozioAbbigliamentoContext context = new NegozioAbbigliamentoContext())
            {
                try
                {
                    List<Prodotto> lista = ReadGetValue();
                    int i = 0;

                    while ((i < lista.Count()) && (lista[i].IdProdotto != id)) i++;

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

        public List<Prodotto> getValueForCategoria(string categoria)
        {
            List<Prodotto> risultato = new List<Prodotto> ();

            using (NegozioAbbigliamentoContext context = new NegozioAbbigliamentoContext())
            {
                try
                {
                    var query = (from p in context.Prodottos
                                 where p.CategoriaProdotto == categoria
                                 select new Prodotto()
                                 {
                                    CategoriaProdotto = categoria,
                                    Descrizione = p.Descrizione,
                                    Marca = p.Marca,
                                    UrlImg = p.UrlImg,
                                    Prezzo = p.Prezzo
                                 });

                    risultato = query.ToList();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            return (risultato);
        }
    

        public bool UpdateValue(Prodotto entity)
        {
            bool risultato = false;

            using (NegozioAbbigliamentoContext context = new NegozioAbbigliamentoContext())
            {
                try
                {
                    var itemToRemove = context.Prodottos.SingleOrDefault(x => x.IdProdotto == entity.IdProdotto); //returns a single item.

                    if (itemToRemove != null)
                    {
                        context.Prodottos.Remove(itemToRemove);
                        context.Prodottos.Add(entity);
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
