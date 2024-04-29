using System;
using System.Collections.Generic;

namespace Abbigliamento.Models;

public partial class Offertum
{
    public int IdOfferta { get; set; }

    public int VariazioneRif { get; set; }

    public DateOnly DataInizio { get; set; }

    public DateOnly DataFine { get; set; }

    public int PrezzoOfferta { get; set; }

    public virtual Variazione VariazioneRifNavigation { get; set; } = null!;
}
