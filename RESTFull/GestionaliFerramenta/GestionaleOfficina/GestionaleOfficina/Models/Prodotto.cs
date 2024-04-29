using System;
using System.Collections.Generic;

namespace GestionaleOfficina.Models;

public partial class Prodotto
{
    public string Codice { get; set; } = null!;

    public string Nome { get; set; } = null!;

    public string Descrizione { get; set; } = null!;

    public int Prezzo { get; set; }

    public int Quantita { get; set; }

    public string Categoria { get; set; } = null!;

    public DateOnly? DataCreazione { get; set; }
}
