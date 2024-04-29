using System;
using System.Collections.Generic;

namespace Abbigliamento.Models;

public partial class Variazione
{
    public int IdVariazione { get; set; }

    public int ProdottoRif { get; set; }

    public string Colore { get; set; } = null!;

    public string Taglia { get; set; } = null!;

    public int? Quantita { get; set; }

    public virtual ICollection<Offertum> Offerta { get; set; } = new List<Offertum>();

    public virtual Prodotto ProdottoRifNavigation { get; set; } = null!;
}
