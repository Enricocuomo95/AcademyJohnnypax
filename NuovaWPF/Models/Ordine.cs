using System;
using System.Collections.Generic;

namespace Abbigliamento.Models;

public partial class Ordine
{
    public int IdOrdine { get; set; }

    public int UtenteRif { get; set; }

    public DateOnly DataOrdine { get; set; }

    public DateOnly DataConsegna { get; set; }

    public virtual Utente UtenteRifNavigation { get; set; } = null!;

    public virtual ICollection<Prodotto> ProdottoRifs { get; set; } = new List<Prodotto>();
}
