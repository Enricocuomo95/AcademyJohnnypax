using GestionaleOfficina.Models;
using Microsoft.IdentityModel.Tokens;

namespace GestionaleOfficina.Repository
{
    public class ProdottoRepo : ICrud<Prodotto>
    {
        private static ProdottoRepo istanza;

        private ProdottoRepo() { }

        public static ProdottoRepo getIstanza()
        {
            if(istanza == null) 
                istanza = new ProdottoRepo();
            return istanza;
        }
        public bool delete(Prodotto entity)
        {
            bool risultato = false;
            using (FerramentaContext ctx = new FerramentaContext())
            {
                try
                {
                    Prodotto prodotto = ctx.Prodottos.Single(p => p.Codice.Equals(entity.Codice));
                    if (prodotto != null)
                    {
                        ctx.Prodottos.Remove(prodotto);
                        ctx.SaveChanges();
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

        public List<Prodotto> findAll()
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

        public bool insert(Prodotto entity)
        {
            bool risultato = false;
            using (FerramentaContext ctx = new FerramentaContext())
            {
                try
                {
                    if ((entity != null) && (!entity.Nome.IsNullOrEmpty()) && (!entity.Categoria.IsNullOrEmpty()))
                    {
                        ctx.Prodottos.Add(entity);
                        ctx.SaveChanges();
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

        public bool update(Prodotto entity)
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
                        if (entity.Quantita != null)
                            prodotto.Quantita = entity.Quantita >= 0 ? entity.Quantita : prodotto.Quantita;

                        ctx.Prodottos.Entry(ctx.Prodottos.SingleOrDefault(p => p.Codice.Equals(entity.Codice))).CurrentValues.SetValues(prodotto);
                        ctx.SaveChanges();

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
