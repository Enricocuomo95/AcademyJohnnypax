using Abbigliamento.DAL;
using Abbigliamento.Models;
using Abbigliamento.Utilities;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualBasic.Devices;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Abbigliamento.View
{
    /// <summary>
    /// Logica di interazione per ViewUtente.xaml
    /// </summary>
    public partial class ViewUtente : Window
    {
        private int idUtente;
        public ViewUtente(int id)
        {
            InitializeComponent();
            idUtente = id;
            buttonOrdine.Visibility = Visibility.Collapsed;
            PopolaTable(ProdottoDAL.getIstanza().ReadGetValue());
        }

        private void PopolaTable(List<Prodotto> listaProdotto)
        {

            ObservableCollection<VistaProdotto> listaView = new ObservableCollection<VistaProdotto>();

            foreach (Prodotto prodotto in listaProdotto)
            {
                List<Variazione> ListVariazioni = VariazioneDAL.getIstanza().getVariazioneForProdotto(prodotto);

                foreach (Variazione variazione in ListVariazioni)
                {
                    //ho una sola offerta valida nella data corrente 
                    //sul db ne ho un botto, ma solo una sarà validata nel mio codice
                    //il db mantiene lo storico. Qui non mi frega di gesti sta cosa
                    //quindi avrò una sola offerta per ogni variazione
                    Offertum offerta = OffertaDAL.getIstanza().getOffertaForVariazione(variazione);

                    VistaProdotto vP = new VistaProdotto();
                    vP.IdProdotto = prodotto.IdProdotto;
                    vP.Descrizione = prodotto.Descrizione + "\n" + prodotto.Marca + "\n" + prodotto.Prezzo + "$";
                    vP.Prezzo = prodotto.Prezzo + "";

                    if (variazione != null)
                    {
                        vP.Taglia = variazione.Taglia;
                        vP.Quantita = "Ci sono: " + variazione.Quantita + "\ndi colore: " + variazione.Colore;
                        vP.IdVariazione = variazione.IdVariazione;
                    }

                    if (offerta != null)
                    {
                        vP.DataInizio = offerta.DataInizio + "";
                        vP.DataFine = offerta.DataFine + "";
                        vP.PrezzoOfferta = offerta.PrezzoOfferta + "";
                        vP.IdOfferta = offerta.IdOfferta;
                    }

                    listaView.Add(vP);
                }
            }

            DG1.DataContext = listaView;

        }

        private void btn_acquisti(object sender, RoutedEventArgs e)
        {
            VistaProdotto vP = (VistaProdotto)DG1.SelectedItem;
            List< VistaProdotto> listaVP = new List< VistaProdotto>();
            listaVP.Add( vP );

            using (StreamWriter file = File.CreateText("C:\\Users\\Utente\\source\\repos\\Abbigliamento\\Abbigliamento\\Utilitis\\carrello.json"))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, listaVP);

            }

            acquistaColl.Visibility = Visibility.Collapsed;
            vistaCarrello();
        }

        private void vistaCarrello()
        {
            MessageBox.Show("Benvenuto nel carrello");
            
            List<VistaProdotto>? lista = null;
            buttonCategoria.Visibility = Visibility.Collapsed;
            buttonOrdine.Visibility = Visibility.Visible;

            try
            {
                string json = File.ReadAllText("C:\\Users\\Utente\\source\\repos\\Abbigliamento\\Abbigliamento\\Utilitis\\carrello.json");
                lista = JsonConvert.DeserializeObject<List<VistaProdotto>>(json);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                MessageBox.Show("C'è un problema!");
            }

            if(lista != null)
                DG1.DataContext = lista;
        }

        private void btn_categoriaSelezionata(object sender, SelectionChangedEventArgs e)
        {
            if(!CategoriaBox.Text.IsNullOrEmpty())
                PopolaTable(ProdottoDAL.getIstanza().getValueForCategoria(CategoriaBox.Text));
            else
                PopolaTable(ProdottoDAL.getIstanza().ReadGetValue());
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var page = new ViewUtente(idUtente);
            page.Show();
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var page = new Login();
            page.Show();
            this.Close();
        }

        private void btn_ordina(object sender, RoutedEventArgs e)
        {
            List<VistaProdotto>? lista = new List<VistaProdotto>();
            List<Prodotto> listaProdotti = new List<Prodotto>();
            buttonCategoria.Visibility = Visibility.Collapsed;
            buttonOrdine.Visibility = Visibility.Visible;

            try
            {
                string json = File.ReadAllText("C:\\Users\\Utente\\source\\repos\\Abbigliamento\\Abbigliamento\\Utilitis\\carrello.json");
                lista = JsonConvert.DeserializeObject<List<VistaProdotto>>(json);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                MessageBox.Show("C'è un problema!");
            }

            Ordine ordine;
            if (lista != null)
            {
                foreach(VistaProdotto item in lista)
                    listaProdotti.Add(ProdottoDAL.getIstanza().getProdottoForId(item.IdProdotto));

                ordine = new Ordine() { DataOrdine = DateOnly.FromDateTime(DateTime.Now), UtenteRif = idUtente, DataConsegna = DateOnly.FromDateTime(DateTime.Now), ProdottoRifs = listaProdotti};
                OrdineDAL.getIstanza().CreateInsertValue(ordine);
                MessageBox.Show("Ordine Creato");
                //svuoto il mio file json

            }
            else
                MessageBox.Show("C'è un problema!");
            
        }
    }
}
