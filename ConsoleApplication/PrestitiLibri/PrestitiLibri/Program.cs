using PrestitiLibri.DAL;
using PrestitiLibri.Entity;
using System.Threading.Channels;

namespace PrestitiLibri
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string risposta = "no";
            Utente utente = new Utente();
            Console.WriteLine("ciao benvenuto nel gestionale Libreria \n" +
                    " registrati per ottenere un libro");
            Console.WriteLine("Inserisci il tuo nome");
            utente.Nome = Console.ReadLine();
            Console.WriteLine("Inserisci il tuo cognome");
            utente.Cognome = Console.ReadLine();
            Console.WriteLine("Inserisci la tua email");
            utente.Email = Console.ReadLine();

            if (UtenteDAL.getIstanza().getUtenteForEmail(utente.Email) == null)
            {
                Console.WriteLine("Benvenuto nel nuovissimo bellissimo sistema di gestione libri da buttare");
                UtenteDAL.getIstanza().CreateInsert(utente);
            }
            else
                Console.WriteLine("Bentornato " + utente.Nome);

            List<Libro> listaLibriDisp = LibroDAL.getIstanza().getLibriDisponibili();

            foreach(Libro l in listaLibriDisp)
              Console.WriteLine("Titolo: " + l.titolo +" Anno di uscita: "+ l.AnnoPub);

            do
            {
                Console.WriteLine("Vuoi un libro? y/n");
                risposta = Console.ReadLine();
                if(risposta == "y")
                {
                    Prestito p = new Prestito();
                    p.utente = utente;
                    int i = 0;
                    Console.WriteLine("che libro vuoi? Inserisci un titolo, tra i precedenti");
                    risposta = Console.ReadLine();
                    while ((i < listaLibriDisp.Count) && (listaLibriDisp[i].titolo != risposta)) i++;
                    if (i == listaLibriDisp.Count)
                        Console.WriteLine("Errore");
                    else
                    {
                        p.libro = listaLibriDisp[i];
                        p.Data_inizio = DateTime.Now;
                        Console.WriteLine("Quando me lo porti? dammi la data nel formato yyyy-mm-dd");
                        risposta = Console.ReadLine();
                        p.Data_fine = Convert.ToDateTime(risposta);
                        PrestitoDAL.getIstanza().CreateInsert(p);
                    }
                }

                Console.WriteLine("Vuoi donare un libro? y/n");
                risposta = Console.ReadLine();
                if (risposta == "y")
                {
                    Libro l = new Libro();
                    Console.WriteLine("dammi il titolo");
                    l.titolo = Console.ReadLine();
                    Console.WriteLine("dammi l'anno di pubblicazione");
                    l.AnnoPub = Convert.ToInt32(Console.ReadLine());
                    l.IsDisp = true;
                    LibroDAL.getIstanza().CreateInsert(l);
                }


            } while (risposta != "n");
        }
    }
}
