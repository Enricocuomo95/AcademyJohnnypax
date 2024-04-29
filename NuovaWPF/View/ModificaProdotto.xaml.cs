using Abbigliamento.DAL;
using Abbigliamento.Models;
using Abbigliamento.Utilities;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Abbigliamento.View
{
    /// <summary>
    /// Logica di interazione per ModificaProdotto.xaml
    /// </summary>
    public partial class ModificaProdotto : Window
    {
        private Prodotto? prodotto;
        private Variazione? variazione;
        private Offertum? offerta;

        public ModificaProdotto(int idProdotto, int idVariazione)
        {
            InitializeComponent();

            prodotto = ProdottoDAL.getIstanza().getProdottoForId(idProdotto);
            variazione = VariazioneDAL.getIstanza().getVariazioneForId(idVariazione);
            offerta = null;

            if((prodotto == null)||(variazione== null))
            {
                MessageBox.Show("Valori passati non vaidi");
                return;
            }

            InizializzaValori();
        }

        public ModificaProdotto(int idProdotto, int idVariazione, int idOfferta)
        {
            InitializeComponent();
            prodotto = ProdottoDAL.getIstanza().getProdottoForId(idProdotto);
            variazione = VariazioneDAL.getIstanza().getVariazioneForId(idVariazione);
            offerta = OffertaDAL.getIstanza().getOffertaForId(idOfferta);

            if ((prodotto == null) || (variazione == null) || (offerta == null))
            {
                MessageBox.Show("Valori passati non vaidi");
                return;
            }

            InizializzaValori();
            InizializzaOfferta();

        }

        private void InizializzaValori()
        {
            DescrizioneBox.Text = prodotto.Descrizione;
            PrezzoBox.Text = prodotto.Prezzo + "";

            QuantitaBox.Text = variazione.Quantita +"";
        }

        private void InizializzaOfferta()
        {
            DateFine.Text = offerta.DataFine.ToString();
            DateInit.Text = offerta.DataInizio.ToString();
            PrezzoScontatoBox.Text = offerta.PrezzoOfferta.ToString();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string descrizione = DescrizioneBox.Text;
            string prezzo = PrezzoBox.Text;

            string quantita = QuantitaBox.Text;

            DateTime? dataInitOfferta = DateInit.SelectedDate.Value;
            DateTime? dataFineOfferta = DateFine.SelectedDate.Value;

            string prezzoOfferta = PrezzoScontatoBox.Text;
            Regex matchEx = new Regex("^[0-9]*$");

            if ((!matchEx.IsMatch(prezzo)) || (prezzo.Length < 2) || (prezzo.Length > 3))
            {
                MessageBox.Show("Inserire il prezzo in forma corretta");
                return;
            }
            if (descrizione.IsNullOrEmpty())
            {
                MessageBox.Show("Descrizione è un campo obbligatorio");
                return;
            }

            if ((!descrizione.Equals(prodotto.Descrizione)) || (Convert.ToInt32(prezzo) != prodotto.Prezzo))
            {
                prodotto.Descrizione = descrizione;
                prodotto.Prezzo = Convert.ToInt32(prezzo);
                ProdottoDAL.getIstanza().UpdateValue(prodotto);
                MessageBox.Show("Prodotto modificato");
            }

            if (!matchEx.IsMatch(quantita))
            {
                MessageBox.Show("Inserire la quantità in forma corretta");
                return;
            }

            if (Convert.ToInt32(quantita) != variazione.Quantita)
            {
                variazione.Quantita = Convert.ToInt32(quantita);
                VariazioneDAL.getIstanza().UpdateValue(variazione);
                MessageBox.Show("Variazione modificata");
            }


            if ((!prezzoOfferta.IsNullOrEmpty()) && (matchEx.IsMatch(prezzoOfferta)) && (Convert.ToInt32(prezzo) > Convert.ToInt32(prezzoOfferta)))
            {
                DateOnly DataInizio = DateOnly.ParseExact(dataInitOfferta.ToString(), "dd/MM/yyyy 00:00:00");
                DateOnly DataFine = DateOnly.ParseExact(dataFineOfferta.ToString(), "dd/MM/yyyy 00:00:00");

                if (DataInizio >= DataFine)
                {
                    MessageBox.Show("Date inserite non valide!! " + DataInizio.ToString() + " " + DataFine.ToString());
                    return;
                }
               

                if (offerta == null)
                {
                    offerta = new Offertum() { DataInizio = DataInizio, DataFine = DataFine, PrezzoOfferta = Convert.ToInt32(prezzoOfferta), VariazioneRif = variazione.IdVariazione };
                    OffertaDAL.getIstanza().CreateInsertValue(offerta);
                    MessageBox.Show("Offerta inserita");

                } else if ((!offerta.DataInizio.Equals(DataInizio)) || (!offerta.DataFine.Equals(DataFine)) || 
                    (offerta.PrezzoOfferta != Convert.ToInt32(prezzoOfferta)))
                {
                    offerta.DataInizio = DataInizio;
                    offerta.DataFine = DataFine;
                    offerta.PrezzoOfferta = Convert.ToInt32(prezzoOfferta);
                    OffertaDAL.getIstanza().UpdateValue(offerta);
                    MessageBox.Show("Offerta modificata");
                }

            }

            backFunction();
        }

        private void backFunction()
        {
            var page = new ViewAdmin();
            page.Show();
            this.Close();
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            backFunction();
        }
    }
}
