using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Principal;

namespace MarioKart.Models
{
    public class Giocatore 
    {
        public int GiocatoreID { get; set; } 
        public String Username { get; set; } = null!;
        public String Passward { get; set; } = null!;
        public String Nominativo { get; set; } = null!;
        public virtual ICollection<Personaggio> Squadra { get; set; }

    }
}
