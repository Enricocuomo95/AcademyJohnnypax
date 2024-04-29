using System.ComponentModel.DataAnnotations;

namespace FerramentaCodeFirst.Models
{
    public class Contatto
    {
        [Key]
        public int ContattoId { get; set; }
        [MaxLength(250)]
        [Required] //not null
        public string Nome { get; set; } = null!;
        [MaxLength(250)]
        public string? Cognome { get; set; }
        [MaxLength(250)]
        [Required] //not null
        public string Telefono { get; set; } = null!;
        [MaxLength(250)]
        public string? Email { get; set; }

        [MaxLength(250)]
        public string? Indirizzo { get; set; }
    }
}
