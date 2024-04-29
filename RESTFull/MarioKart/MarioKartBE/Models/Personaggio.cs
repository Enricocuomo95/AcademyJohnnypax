using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MarioKart.Models
{
    public class Personaggio
    {
        public int PersonaggioID { get; set; }
        public int? GiocatoreRIF {  get; set; }
        public String Nome { get; set; } = null!;
        public String Costo { get; set; } = null!;
        public String Categoria { get; set; } = null!;
        public bool Disponibile { get; set; }

        public virtual Giocatore? Giocatore { get; set; } 
    }
}
