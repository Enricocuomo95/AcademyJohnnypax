using System;
using System.Collections.Generic;

namespace aspWeb.Models;

public partial class Impiegato
{
    public int ImpiegatoId { get; set; }

    public string Matricola { get; set; } = null!;

    public string Nome { get; set; } = null!;

    public string Cognome { get; set; } = null!;

    public DateOnly DataNascita { get; set; }

    public string Ruolo { get; set; } = null!;

    public string Reparto { get; set; } = null!;

    public string IndirizzoRes { get; set; } = null!;

    public string CittaRes { get; set; } = null!;

    public string ProvinciaRes { get; set; } = null!;
}
