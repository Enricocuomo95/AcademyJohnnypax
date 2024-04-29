using System;
using System.Collections.Generic;

namespace Abbigliamento.Models;

public partial class Prodotto
{
    public int IdProdotto { get; set; }

    public string Marca { get; set; } = null!;

    public string Descrizione { get; set; } = null!;

    public string UrlImg { get; set; } = null!;

    public int Prezzo { get; set; }

    public string? CategoriaProdotto { get; set; }

    public virtual ICollection<Variazione> Variaziones { get; set; } = new List<Variazione>();

    public virtual ICollection<Ordine> OrdineRifs { get; set; } = new List<Ordine>();
}
