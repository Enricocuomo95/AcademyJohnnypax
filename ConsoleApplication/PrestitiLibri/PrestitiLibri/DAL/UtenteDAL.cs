using PrestitiLibri.Entity;
using PrestitiLibri.Utilities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrestitiLibri.DAL
{
    //N.B.: anche le classi Data Access Layer le renderò singleton poichè 
    //voglio performare il mio stato della memoria
    internal class UtenteDAL : ElementCRUD<Utente>
    {
        private List<Utente> utenteList;
        private string credenziali;
        private static UtenteDAL istanza;

        public static UtenteDAL getIstanza()
        {
            if(istanza == null)
                istanza = new UtenteDAL();
            return(istanza);
        }

        private UtenteDAL()
        {
            utenteList = new List<Utente>();
            credenziali = ConnectionItem.getIstanza().GetCredenziali();
        }
        public bool Delete(Utente t)
        {
            bool risultato = false;

            using (SqlConnection con = new SqlConnection(credenziali))
            {
                string query = "DELETE from Utente where idUtente = @id";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@id", t.Id);

                try
                {
                    con.Open();

                    if (cmd.ExecuteNonQuery() > 0)
                        risultato = true;
                }
                catch (SqlException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    con.Close();
                }

                return (risultato);

            }
        }

        private void getList()
        {
            Utente u;

            using (SqlConnection con = new SqlConnection(credenziali))
            {
                string query = "select idUtente,nome,cognome,email from Utente";
                SqlCommand cmd = new SqlCommand(query, con);

                try
                {
                    con.Open();

                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        u = new Utente();
                        u.Id = Convert.ToInt32(reader[0]);
                        u.Nome = reader.GetString(1);
                        u.Cognome = reader.GetString(2);
                        u.Email = reader.GetString(3);
                        utenteList.Add(u);
                    }
                }
                catch (SqlException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    con.Close();
                }

            }
        }

        public List<Utente> ReadGetAll()
        {
            if (utenteList.Count == 0)
                getList();

            return (utenteList);
        }

        public bool CreateInsert(Utente t)
        {
            bool risultato = false;
            using (SqlConnection con = new SqlConnection(credenziali))
            {
                string query = "Insert into Utente (nome, cognome, email) values (@nome, @cognome, @email)";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@nome", t.Nome);
                cmd.Parameters.AddWithValue("@cognome", t.Cognome);
                cmd.Parameters.AddWithValue("@email", t.Email);

                try
                {
                    con.Open();
                    if (cmd.ExecuteNonQuery() > 0)
                        risultato = true;

                }
                catch (SqlException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    con.Close();
                }

                getList();
                return (risultato);
            }

        }

        public bool Update(Utente t)
        {
            bool risultato = false;
            using (SqlConnection con = new SqlConnection(credenziali))
            {
                string query = "Update Utente set nome = @nome, cognome = @cognome, email = @email where utenteId = @id";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@nome", t.Nome);
                cmd.Parameters.AddWithValue("@cognome", t.Cognome);
                cmd.Parameters.AddWithValue("@email", t.Email);
                cmd.Parameters.AddWithValue("@id", t.Id);

                try
                {
                    con.Open();
                    if (cmd.ExecuteNonQuery() > 0)
                        risultato = true;

                }
                catch (SqlException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    con.Close();
                }

            }

            int i = 0;
            while ((i < utenteList.Count()) && (utenteList[i].Id != t.Id)) i++;

            if (utenteList[i] != null)
            {
                utenteList.Remove(utenteList[i]);
                utenteList.Add(t);
            }
            else
                getList();

            return (risultato);
        }

        public Utente getUtenteForId(int id)
        {
            if (utenteList.Count == 0)
                this.getList();

            var val = from Utente in utenteList
                      where Utente.Id == id
                      select Utente;

            Utente risultato = new Utente();
            foreach (Utente x in val)
            {
                risultato.Id = x.Id;
                risultato.Email = x.Email;
                risultato.Nome = x.Nome;
                risultato.Cognome = x.Cognome;
            }

            return (risultato);
        }

        public Utente getUtenteForEmail(string email)
        {
            if (utenteList.Count == 0)
                this.getList();

            var val = from Utente in utenteList
                      where Utente.Email == email
                      select Utente;

            Utente risultato = new Utente();
            foreach(Utente x in val)
            {
                risultato.Id = x.Id;
                risultato.Email = x.Email;
                risultato.Nome = x.Nome;
                risultato.Cognome = x.Cognome;
            }
                
            return (risultato);
        }
    }
}
