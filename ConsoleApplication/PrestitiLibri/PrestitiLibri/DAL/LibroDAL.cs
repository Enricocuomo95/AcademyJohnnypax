using PrestitiLibri.Entity;
using PrestitiLibri.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrestitiLibri.DAL
{
    //N.B.: anche le classi Data Access Layer le renderò singleton poichè 
    //voglio performare il mio stato della memoria
    internal class LibroDAL : ElementCRUD<Libro>
    {
        private List<Libro> libroList;
        private string credenziali;
        private static LibroDAL istanza;

        public static LibroDAL getIstanza()
        {
            if(istanza == null)
                istanza = new LibroDAL();
            return(istanza);
        }

        private LibroDAL()
        {
            libroList = new List<Libro>();
            credenziali = ConnectionItem.getIstanza().GetCredenziali();
        }
        public bool Delete(Libro t)
        {
            bool risultato = false;

            using (SqlConnection con = new SqlConnection(credenziali))
            {
                string query = "DELETE from Libro where idLibro = @id";
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
            Libro l;

            using (SqlConnection con = new SqlConnection(credenziali))
            {
                string query = "select idLibro,titolo,annoPubblicazione,isDisponibile from Libro";
                SqlCommand cmd = new SqlCommand(query, con);

                try
                {
                    con.Open();

                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        l = new Libro();
                        l.Id = Convert.ToInt32(reader["idLibro"]);
                        l.titolo = reader.GetString(1);
                        l.AnnoPub = Convert.ToInt32(reader["annoPubblicazione"]);
                        l.IsDisp = Convert.ToBoolean(reader["isDisponibile"]);
                        libroList.Add(l);
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

        public List<Libro> ReadGetAll()
        {
            if(libroList.Count == 0)
                getList();

            return (libroList);    
        }

        public bool CreateInsert(Libro t)
        {
            bool risultato = false;
            using(SqlConnection con = new SqlConnection(credenziali))
            {
                string query = "Insert into Libro (titolo, annoPubblicazione, isDisponibile) values (@titolo, @annoPubblicazione, @isDisponibile)";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@titolo", t.titolo);
                cmd.Parameters.AddWithValue("@annoPubblicazione", t.AnnoPub);
                cmd.Parameters.AddWithValue("@isDisponibile", t.IsDisp);

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

        public bool Update(Libro t)
        {
            bool risultato = false;
            var val = from Libro in libroList
                      where Libro.Id == t.Id
                      select Libro;

            if (val == null)
                return risultato;

            using (SqlConnection con = new SqlConnection(credenziali))
            {
                string query = "Update Libro set titolo = @titolo, annoPubblicazione = @annoPubblicazione, isDisponibile = @isDisponibile where idLibro = @id";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@titolo", t.titolo);
                cmd.Parameters.AddWithValue("@annoPubblicazione", t.AnnoPub);
                cmd.Parameters.AddWithValue("isDisponibile", t.IsDisp);
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

            foreach(Libro l in val)
                libroList.Remove(l);
                        
            libroList.Add(t);
            return (risultato);
        }

        public List<Libro> getLibriDisponibili()
        {
            if (libroList.Count == 0)
                this.getList();
            List<Libro> risultato = new List<Libro> ();
            var valori = from Libro in libroList
                               where Libro.IsDisp == true
                               select Libro;

            risultato = valori.ToList();
            
            return(risultato);

        }

        public Libro getLibroForId(int id)
        {
            var val = from Libro in libroList
                         where Libro.Id == id
                         select Libro;
            return ((Libro)val);
        }
    }

}
