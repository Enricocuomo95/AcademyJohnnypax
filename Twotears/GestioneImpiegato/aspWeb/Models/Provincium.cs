using System;
using System.Collections.Generic;

namespace aspWeb.Models;

public partial class Provincium
{
    public int ProvinciaId { get; set; }

    public string? Sigla { get; set; }

    public string Nome { get; set; } = null!;

    public virtual ICollection<Cittum> Citta { get; set; } = new List<Cittum>();
}
