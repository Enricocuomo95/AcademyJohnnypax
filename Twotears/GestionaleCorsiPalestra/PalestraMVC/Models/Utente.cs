using System;
using System.Collections.Generic;

namespace PalestraMVC.Models;

public partial class Utente
{
    public string Username { get; set; } = null!;

    public string PasswordUtente { get; set; } = null!;

    public virtual ICollection<Corso> NomeCorsos { get; set; } = new List<Corso>();
}
