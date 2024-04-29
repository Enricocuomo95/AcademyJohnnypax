using MarioKart.Models;

namespace MarioKart.DTO
{
    public class PersonaggioDTO
    {
        public String Nome { get; set; } = null!;
        public String Costo { get; set; } = null!;
        public String Categoria { get; set; } = null!;
        public bool Disponibile { get; set; }
        public Giocatore Giocatore { get; set; } = new Giocatore();
    }
}
