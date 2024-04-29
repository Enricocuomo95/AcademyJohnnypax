using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PalestraMVC.Models;
using PalestraMVC.Service;
using System.IO.Pipelines;

namespace PalestraMVC.Controllers
{
    public class UtenteController : Controller
    {
        private IService<Utente> serviceUtente;
        private IService<Corso> serviceCorso;

        public UtenteController(IService<Utente> serviceUtente, IService<Corso> serviceCorso)
        {
            this.serviceUtente = serviceUtente;
            this.serviceCorso = serviceCorso;
        }
        public IActionResult Login() { return View(); }

        public IActionResult Registrazione(Utente utente)
        {
            if(utente is not null)
                if ((utente.Username is not null) && (!utente.Username.Equals("")) && (utente.PasswordUtente is not null) && (!utente.PasswordUtente.Equals("")))
                {
                    this.serviceUtente.insert(utente);
                    return (RedirectToAction("Profiloutente", utente));
                }

            return (View());
        }

        public IActionResult Profiloutente(Utente utente)
        {
            if (utente is not null)
                if ((utente.Username is not null) && (!utente.Username.Equals("")) && (utente.PasswordUtente is not null) && (!utente.PasswordUtente.Equals("")))
                {
                    //qui do per convenzione che solo enrico ha i permessi di admin
                    if(utente.Username == "enrico")
                    {
                        HttpContext.Session.SetString("permesso_accesso", "ADMIN");
                        return (RedirectToAction("Profiloadmin", utente));
                    }
                    HttpContext.Session.SetString("permesso_accesso", "USER");
                    HttpContext.Session.SetString("user_loggato", utente.Username);
                    List<Corso> listCorso = serviceCorso.findAll();
                    //qui potrei implementare un controllo per selezionare solo
                    //i corsi che hanno data inizio maggiore o uguale alla data corrente

                    return (View(listCorso));

                    //Qui non uso più il cookie come con l'esercizio della gestione impiegato
                    //Uso la sessione per raggioni di efficienza e sicurezza!!!
                    //string? valore = HttpContext.Request.Cookies["userLogged"];
                    //if (valore is not null && valore.Equals("USER"))
                    //{
                    //    if(this.service.findByName(utente.Username) != null)
                    //        return (View());
                    //}
                }

            return (Redirect("/Utente/Login"));
        }

        public IActionResult RegistrazioneCorso(string? cod)
        {
            string? Loggato = HttpContext.Session.GetString("user_loggato");
            if (Loggato is not null)
            {
                Corso corso = serviceCorso.findByName(cod);
                if ((corso == null) || (corso.NPosti - 1 < 0))
                    return (BadRequest());

                corso.NPosti = corso.NPosti - 1;
                Utente utente = serviceUtente.findByName(Loggato);
                corso.UsernameUtentes.Add(utente);
                serviceCorso.update(corso);
                utente.NomeCorsos.Add(corso);
                return (Ok());
            }
            return (BadRequest());
        }

        public IActionResult Profiloadmin(Utente utente)
        {
            string? Permessi = HttpContext.Session.GetString("permesso_accesso");
            if (Permessi is not null && Permessi.Equals("ADMIN"))
                return (View());
            //altrimenti... qualcosa non è andato bene 
            //te lo torno questo valore... 
            //lo torno a Profiloutente che mi setta la sessione
            return (RedirectToAction("Profiloutente", utente));
        }

    }
}
