using Abbigliamento.Models;
using Abbigliamento.DAL;
using Abbigliamento.Utilities;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using Abbigliamento.View;


namespace Abbigliamento
{
    /// <summary>
    /// Logica di interazione per Window1.xaml
    /// </summary>
    public partial class ViewAdmin : Window
    {
        public ViewAdmin()
        {
            InitializeComponent();
            PopolaTable();          
        }

        private void PopolaTable()
        {

            ObservableCollection<VistaProdotto> listaView = new ObservableCollection<VistaProdotto>();

            foreach (Prodotto prodotto in ProdottoDAL.getIstanza().ReadGetValue())
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
                        vP.Quantita = "Ci sono: " + variazione.Quantita + "\ndi colore: "+ variazione.Colore;
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

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private bool IsMaximized = false;
        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if(e.ClickCount == 2)
            {
                if(IsMaximized)
                {
                    this.WindowState = WindowState.Normal;
                    this.Width = 1080;
                    this.Height = 720;

                    IsMaximized = false;
                }
                else
                {
                    this.WindowState = WindowState.Maximized;
                    IsMaximized = true;
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var page = new InsertProdotto();
            page.Show();
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var page = new Login();
            page.Show();
            this.Close();
        }

        private void btn_update(object sender, RoutedEventArgs e)
        {
            //ogni immagine corrisponde un prodotto 
            //l'admin può cambiare sono la quantita (se fa un nuovo ordine) [variazione]
            //il prezzo e la descrizione [prodotto]
            //prezzo dell'offerta [offerta]
            
            VistaProdotto vP = (VistaProdotto) DG1.SelectedItem;
            ModificaProdotto update;

            if(vP.DataInizio != null)
                update = new ModificaProdotto(vP.IdProdotto,vP.IdVariazione,vP.IdOfferta);
            else
                update = new ModificaProdotto(vP.IdProdotto, vP.IdVariazione);

            update.Show();
            this.Close();
        }

        private void btn_delete(object sender, RoutedEventArgs e)
        {
            //dovrei gesire lo storico quindi non posso elliminarli
            //avrei dovuto mettere nel db un flag 
            //si... avrei dovuto 
            VistaProdotto vP = (VistaProdotto)DG1.SelectedItem;

            if (vP.DataInizio != null)
                OffertaDAL.getIstanza().DeleteValueForId(vP.IdOfferta);
            VariazioneDAL.getIstanza().DeleteValueForId(vP.IdVariazione);
            PopolaTable();
        }
    }
}
