using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrestitiLibri.Entity
{
    internal class Libro
    {
        public int Id { get; set; }
        public string titolo { get; set; }
        public int AnnoPub { get; set; }
        public bool IsDisp { get; set; }
        public Libro() { }

        public Libro(int id, string titolo, int annoPub, bool isDisp)
        {
            Id = id;
            this.titolo = titolo;
            AnnoPub = annoPub;
            IsDisp = isDisp;
        }
    }
}
