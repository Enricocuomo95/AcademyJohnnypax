using Ferramenta.Models;
using Microsoft.IdentityModel.Tokens;

namespace Ferramenta.Repositori
{
    public class RepositoriProdotto : ICrud<Prodotto>
    {
        private static RepositoriProdotto istanza;

        private RepositoriProdotto() { }
        public static RepositoriProdotto getIstanza()
        {
            if(istanza == null)
                istanza = new RepositoriProdotto();
            return istanza;
        }
        public bool DeleteValue(Prodotto entity)
        {
            bool risultato = false;
            using(FerramentaContext ctx = new FerramentaContext())
            {
                try
                {
                    Prodotto prodotto = ctx.Prodottos.Single( p => p.Codice.Equals(entity.Codice));
                    if (prodotto != null)
                    {
                        ctx.Prodottos.Remove(prodotto);
                        risultato = true;
                    }
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            return(risultato);
        }

        public List<Prodotto> getAll()
        {
            List<Prodotto> risultato = new List<Prodotto>();
            using (FerramentaContext ctx = new FerramentaContext())
            {
                try
                {
                    risultato = ctx.Prodottos.ToList();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            return (risultato);
        }

        public bool Insert(Prodotto entity)
        {
            bool risultato = false;
            using (FerramentaContext ctx = new FerramentaContext())
            {
                try
                {
                    if ((entity != null) && (!entity.Nome.IsNullOrEmpty()) && (!entity.Categoria.IsNullOrEmpty()))
                    {
                        ctx.Prodottos.Add(entity);
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

        public bool UpdateValue(Prodotto entity)
        {
            bool risultato = false;
            Prodotto prodotto;

            using (FerramentaContext ctx = new FerramentaContext())
            {
                try
                {
                    if ((entity != null) && (!entity.Nome.IsNullOrEmpty()) && (!entity.Categoria.IsNullOrEmpty()))
                    {
                        prodotto = ctx.Prodottos.SingleOrDefault(p => p.Codice.Equals(entity.Codice));
                        prodotto.Descrizione = entity.Descrizione.IsNullOrEmpty() ? entity.Descrizione : prodotto.Descrizione;

                        if (entity.Prezzo != null)
                            prodotto.Prezzo = entity.Prezzo >= 0 ? entity.Prezzo : prodotto.Prezzo;
                        if (entity.Quantità != null)
                            prodotto.Quantità = entity.Quantità >= 0 ? entity.Quantità : prodotto.Quantità;

                        ctx.Prodottos.Entry(ctx.Prodottos.SingleOrDefault(p => p.Codice.Equals(entity.Codice))).CurrentValues.SetValues(prodotto);
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
