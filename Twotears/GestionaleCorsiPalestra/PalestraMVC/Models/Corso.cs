using System;
using System.Collections.Generic;

namespace PalestraMVC.Models;

public partial class Corso
{
    public string Nome { get; set; } = null!;

    public string Codice { get; set; } = null!;

    public string Descrizione { get; set; } = null!;

    public string TipoCorso { get; set; } = null!;

    public DateOnly DataCorso { get; set; }

    public string OraInizio { get; set; } = null!;

    public string OraFine { get; set; } = null!;

    public int NPosti { get; set; }

    public virtual ICollection<Utente> UsernameUtentes { get; set; } = new List<Utente>();
}
