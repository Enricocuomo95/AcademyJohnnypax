using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abbigliamento.Utilities
{
    class VistaProdotto
    {
        public int IdProdotto { get; set; }
        public int IdVariazione { get; set; }
        public int IdOfferta { get; set; }

        public string UrlImg { get; set; } = null!;

        public string Descrizione { get; set; } = null!;

        public string Taglia { get; set; } = null!;

        public string Quantita { get; set; }

        public string DataInizio { get; set; }

        public string DataFine { get; set; }
        public string PrezzoOfferta { get; set; }
        public string Prezzo { get; set; }
    }
}
