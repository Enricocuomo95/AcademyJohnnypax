using Newtonsoft.Json;
using Abbigliamento.DAL;
using Abbigliamento.Models;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Abbigliamento
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class Registrazione : Window
    {
        public Registrazione()
        {
            InitializeComponent();           
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameBox.Text;
            string password = passwordBox.Password;
            string confirmPassword = ConfirmPasswordBox.Password;
            string nome = FirstNameBox.Text;
            string cognome = LastNameBox.Text;
            string email = EmailBox.Text;
            Regex matchEx = new Regex("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)[a-zA-Z\\d]{8,}$");

            if (string.IsNullOrWhiteSpace(username))
            {
                MessageBox.Show("inserire Username!");
                return;
            }
            if (string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("inserire Password!");
                return;
            }
            if (string.IsNullOrWhiteSpace(nome))
            {
                MessageBox.Show("inserire Nome!");
                return;
            }
            if (string.IsNullOrWhiteSpace(cognome))
            {
                MessageBox.Show("inserire Cognome!");
                return;
            }
            if (string.IsNullOrWhiteSpace(email))
            {
                MessageBox.Show("inserire Email!");
                return;
            }
            if (!password.Equals(confirmPassword))
            {
                MessageBox.Show("Password errata");
                return;
            }
            if (!matchEx.IsMatch(password))
            {
                MessageBox.Show("Password errata. \n Sono richiesti: Minimo otto caratteri, almeno una lettera maiuscola, una lettera minuscola ed un numero:");
                return;
            }

            UtenteDAL.getIstanza().CreateInsertValue(new Utente() { EmailUtente = email, PasswordUtente = password });
            MessageBox.Show("Utente registrato");
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var page = new Login();
            page.Show();
            this.Close();
        }
    }
}