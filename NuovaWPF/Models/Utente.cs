using System;
using System.Collections.Generic;

namespace Abbigliamento.Models;

public partial class Utente
{
    public int IdUtente { get; set; }

    public string EmailUtente { get; set; } = null!;

    public string PasswordUtente { get; set; } = null!;

    public virtual ICollection<Ordine> Ordines { get; set; } = new List<Ordine>();
}
