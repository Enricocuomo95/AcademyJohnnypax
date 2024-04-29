using Abbigliamento.DAL;
using Newtonsoft.Json;
using Abbigliamento.Models;
using System.Text;
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
using Microsoft.IdentityModel.Tokens;
using Abbigliamento.View;

namespace Abbigliamento
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameBox.Text;
            string password = passwordBox.Password;
            List<Utente>? listaJson;

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

            foreach(Utente x in UtenteDAL.getIstanza().ReadGetValue())
            {
                if((x.PasswordUtente.Equals(password)) && (x.EmailUtente.Equals(username)))
                {
                    try
                    {
                        string json = System.IO.File.ReadAllText("C:\\Users\\Utente\\source\\repos\\Abbigliamento\\Abbigliamento\\Utilitis\\admin.json");
                        if (!json.IsNullOrEmpty())
                        {
                            listaJson = JsonConvert.DeserializeObject<List<Utente>>(json);

                            foreach(Utente u in listaJson)
                                if ((u.PasswordUtente.Equals(password))&&(u.EmailUtente.Equals(username)))
                                {
                                    var page = new ViewAdmin();
                                    page.Show();
                                    this.Close();
                                    return;
                                }
                                else
                                {
                                    var page1 = new ViewUtente(u.IdUtente);
                                    page1.Show();
                                    this.Close();
                                    return;
                                }
                        }
                            
                    } catch(Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }      
                }
            }

            MessageBox.Show("nome utente o password non corretti");
            return;

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var page = new Registrazione();
            page.Show();
            this.Close();
        }
    }
}