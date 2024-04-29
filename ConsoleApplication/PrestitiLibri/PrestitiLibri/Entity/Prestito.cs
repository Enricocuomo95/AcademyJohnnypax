using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrestitiLibri.Entity
{
    internal class Prestito
    {
        public int Id { get; set;}
        public Libro? libro { get; set;}
        public Utente? utente { get; set;}
        public DateTime? Data_inizio { get; set; }
        public DateTime? Data_fine { get; set; }

        public Prestito() { }

        public Prestito(int id, Libro? libro, Utente? utente, DateTime? data_inizio, DateTime? data_fine)
        {
            Id = id;
            this.libro = libro;
            this.utente = utente;
            Data_inizio = data_inizio;
            Data_fine = data_fine;
        }
    }
}
