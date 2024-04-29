using Abbigliamento.DAL;
using Abbigliamento.Models;
using Abbigliamento.View;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Globalization;
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

namespace Abbigliamento
{
    /// <summary>
    /// Logica di interazione per InsertProdotto.xaml
    /// </summary>
    public partial class InsertProdotto : Window
    {
        public InsertProdotto()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string UrlImg = UrlBox.Text;
            string descrizione = DescrizioneBox.Text;
            string marca = MarcaBox.Text;
            string prezzo = PrezzoBox.Text;
            string categoria = CategoriaBox.Text;

            string colore = ColoreBox.Text;
            string taglia = TagliaBox.Text;
            string quantita = QuantitaBox.Text;

            DateTime? dataInitOfferta = DateInit.SelectedDate.Value;
            DateTime? dataFineOfferta = DateFine.SelectedDate.Value;

            string prezzoOfferta = PrezzoScontatoBox.Text;
            Regex matchEx = new Regex("^[0-9]*$");

            //prodotto
            if (UrlImg.IsNullOrEmpty())
            {
                //do per scontato che l'amministratore inserisce prima l'imaggine nell'apposita directory
                //voglio concedermi questo lusso
                MessageBox.Show("Url image è un campo obbligatorio");
                return;
            }
            if (descrizione.IsNullOrEmpty())
            {
                MessageBox.Show("Descrizione è un campo obbligatorio");
                return;
            }
            if (marca.IsNullOrEmpty())
            {
                MessageBox.Show("Marca è un campo obbligatorio");
                return;
            }
            if ((!matchEx.IsMatch(prezzo))&&((prezzo.Length == 2)||(prezzo.Length == 3)))
            {
                MessageBox.Show("Inserire il prezzo in forma corretta");
                return;
            }
            if (categoria.IsNullOrEmpty())
            {
                MessageBox.Show("Categoria è un campo obbligatorio");
                return;
            }

            //variazione
            if (taglia.IsNullOrEmpty())
            {
                MessageBox.Show("Taglia è un campo obbligatorio");
                return;
            }
            if (colore.IsNullOrEmpty())
            {
                MessageBox.Show("Colore è un campo obbligatorio");
                return;
            }
            if (!matchEx.IsMatch(quantita))
            {
                MessageBox.Show("Inserire il prezzo in forma corretta");
                return;
            }

            //offerta o la inserisci tutta o non la inserisci affatto
            Offertum? offerta = null;
            if((!prezzoOfferta.IsNullOrEmpty()) && (matchEx.IsMatch(prezzoOfferta)) && (Convert.ToInt32(prezzo) > Convert.ToInt32(prezzoOfferta)))
            {
                offerta = new Offertum();
                offerta.PrezzoOfferta = Convert.ToInt32(prezzoOfferta);

                if (dataInitOfferta != null)
                    offerta.DataInizio = DateOnly.ParseExact(dataInitOfferta.ToString(), "dd/MM/yyyy 00:00:00");

                if (dataFineOfferta != null)
                    offerta.DataFine = DateOnly.ParseExact(dataFineOfferta.ToString(), "dd/MM/yyyy 00:00:00");

                if(offerta.DataInizio >= offerta.DataFine)
                {
                    MessageBox.Show("Date inserite non valide!! " + offerta.DataInizio + " " + offerta.DataFine);
                    return;
                }

            }            

            Variazione? variazione = new Variazione() { Colore = colore, Taglia = taglia, Quantita = Convert.ToInt32(quantita)};
            Prodotto? prodotto = new Prodotto() { UrlImg = UrlImg, Descrizione = descrizione, Marca = marca, Prezzo = Convert.ToInt32(prezzo), CategoriaProdotto = categoria };
            //ProdottoDAL.getIstanza().CreateInsertValue(prodotto);
            //il mio id è autoincrementale 
            //quindi per recuperarlo devo fare sta cosa 
            //nel mio db metto urlimg come unique. Qui assumo che lamministratore inserisce l'immagine e poi l'url
            //in questo modo la mia ricerca (getProdotoo(Prodotto)) mi restituirà con precisione il valore inserito poc'anzi
            //Si lo so sono un professionista!!!!!


            if (prodotto != null)
            {
                ProdottoDAL.getIstanza().CreateInsertValue(prodotto);
                prodotto = ProdottoDAL.getIstanza().getProdotto(prodotto);

                if(prodotto == null)
                {
                    MessageBox.Show("Prodotto non inserito in modo corretto");
                    return;
                }
                else
                    MessageBox.Show("Prodotto inserito");
            }
                
            if(variazione != null)
            {
                variazione.ProdottoRif = prodotto.IdProdotto;
                VariazioneDAL.getIstanza().CreateInsertValue(variazione);
                variazione = VariazioneDAL.getIstanza().getVariazione(variazione);

                if (variazione == null)
                {
                    MessageBox.Show("variazione non inserito in modo corretto");
                    return;
                }
                else
                    MessageBox.Show("variazione inserito");


                if (offerta != null)
                {
                    offerta.VariazioneRif = variazione.IdVariazione;
                    OffertaDAL.getIstanza().CreateInsertValue(offerta);
                    MessageBox.Show("Offerta inserito");
                }
            }
                

            var page = new ViewAdmin();
            page.Show();
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var page = new ViewAdmin();
            page.Show();
            this.Close();
        }
    }
}
