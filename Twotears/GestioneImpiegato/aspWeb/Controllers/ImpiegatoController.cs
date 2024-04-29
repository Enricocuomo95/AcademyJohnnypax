using aspWeb.Models;
using aspWeb.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft;
using Newtonsoft.Json;

namespace aspWeb.Controllers
{
    public class ImpiegatoController : Controller
    {
        private  ImpiegatoService impiegato;
        private  RepartoService reparto;
        private  CittaService citta;
        private  ProvinciaService provincia;

        public ImpiegatoController(ImpiegatoService impiegatoService, RepartoService repartoService, CittaService cittaService, ProvinciaService provinciaService)
        {
            impiegato = impiegatoService;
            reparto = repartoService;
            citta = cittaService;
            provincia = provinciaService;
        }


        public void GetCitiesByProvince(string provincia)
        {
            // Supponiamo di avere una funzione nel nostro modello che restituisce le città associate a una provincia
            //qui passo soltanto la stringa per alleggerire il carico sul mio FE
            var cities = new List<string>();

            List<Cittum> lista = this.provincia.getForName(provincia).Citta.ToList();
            lista.ForEach(c => cities.Add(c.NomeCitta));

            // Convertiamo l'elenco di città in formato JSON
            string jsonString = JsonConvert.SerializeObject(cities);

        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login() { return View(); }

        public IActionResult Profiloamministratore()
        {
            string? valore = HttpContext.Request.Cookies["userLogged"];
            if (valore is not null && valore.Equals("ADMIN"))
                return (View());

            return (Redirect("/Impiegato/Login"));
        }

        public IActionResult Profiloutente()
        {
            string? valore = HttpContext.Request.Cookies["userLogged"];
            if (valore is not null && valore.Equals("USER"))
                return (View());

            return (Redirect("/Impiegato/Login"));
        }

        public IActionResult Insert()
        {
            ViewBag.listaReparto = reparto.getAll();
            ViewBag.listaCitta = citta.getAll();
            ViewBag.listaProvincia = provincia.getAll();

            HttpContext.Response.Cookies.Append("linguaggio", "ITA");
            HttpContext.Response.Cookies.Append("font", "22");

            //posso usare un json punto stynghifay ma con una libreria nuget proprietaria
            //NewtsoftJson è una delle librerie più vecchie di nuget 
            //essa è nata per sostituire e aggirare le problematiche dovte al 'xml nel protocollo SOAP
            //qui posso utilizzare questa API per passare un oggetto srializzato nel mio cookie

            Cittum c = new Cittum()
            {
                NomeCitta = "Nocera",
                CittaId = 100
            };
            HttpContext.Response.Cookies.Append("citta", JsonConvert.SerializeObject(c));


            //con la session
            /*
             * posso usare la session invece del cookie poichè questo è più sicuro
             * la session genera dei cookies per me con delle informazini, 
             * RUBBARE queste informazioni è estremamente difficile poichè questa è cifrata e il browser inoltre 
             * mi crea un antifogery: per rubbare i miei dati di sessione servirebbe la combinazione 
             * del cookie ASP.NET CORE SESSION che è una stringa lunghissima e che mecha con 
             * AspNetCore.AntiFogery.'codice' che mi da l'impronta del  browser che è differente da browser a browser
             * Questo valore lo genera lui automaticamnte. Inoltre l'antiforgery è collegato alla mia cpu
             *  
             */
            
            //HttpContext.Session.SetString("")

            return (View()); 
        }

        public IActionResult Lista()
        {
            String? lingua = HttpContext.Request.Cookies["linguaggio"];
            String? font = HttpContext.Request.Cookies["font"];
            String? value = HttpContext.Request.Cookies["citta"];

            ViewBag.limgua = lingua;
            ViewBag.font = font;

            if(value != null)
                ViewBag.value = JsonConvert.DeserializeObject<Cittum>(value);

            return(View());
        }

    }
}
