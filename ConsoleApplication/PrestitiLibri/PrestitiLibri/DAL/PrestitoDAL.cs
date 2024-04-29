using PrestitiLibri.Entity;
using PrestitiLibri.Utilities;
using System.Data;
using System.Data.SqlClient;

namespace PrestitiLibri.DAL
{
    internal class PrestitoDAL : ElementCRUD<Prestito>
    {
        private List<Prestito> prestitoList;
        private string credenziali;
        private static PrestitoDAL istanza;

        public static PrestitoDAL getIstanza()
        {
            if (istanza == null)
                istanza = new PrestitoDAL();
            return (istanza);
        }

        private PrestitoDAL()
        {
            prestitoList = new List<Prestito>();
            credenziali = ConnectionItem.getIstanza().GetCredenziali();
        }

        private void getList()
        {
            Prestito p;

            using (SqlConnection con = new SqlConnection(credenziali))
            {
                string query = "select idPrestito, libroRif, utenteRif, dataInizio, dataFine from Prestito";
                SqlCommand cmd = new SqlCommand(query, con);

                try
                {
                    con.Open();

                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        p = new Prestito();
                        p.Id = Convert.ToInt32(reader[0]);
                        p.libro = LibroDAL.getIstanza().getLibroForId(Convert.ToInt32(reader[1]));
                        p.utente = UtenteDAL.getIstanza().getUtenteForId(Convert.ToInt32(reader[2]));
                        p.Data_inizio = reader.GetDateTime(3);
                        p.Data_fine = reader.GetDateTime(4);
                        this.prestitoList.Add(p);
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

        public bool Delete(Prestito t)
        {
            bool risultato = false;

            using (SqlConnection con = new SqlConnection(credenziali))
            {
                string query = "DELETE from Prestito where idPrestito = @id";
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

                getList();
                return (risultato);
            }
        }

        public bool CreateInsert(Prestito t)
        {
            bool risultato = false;
            using (SqlConnection con = new SqlConnection(credenziali))
            {
                String query = "INSERT INTO Prestito (libroRif, utenteRif, dataInizio, dataFine) VALUES (@libroRif, @utenteRif, @dataInizio, @dataFine)";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("libroRif", t.libro.Id);
                cmd.Parameters.AddWithValue("utenteRif", t.utente.Id);
                cmd.Parameters.AddWithValue("dataInizio", t.Data_inizio);
                cmd.Parameters.AddWithValue("dataFine", t.Data_fine);

                try
                {
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

                getList();
                t.libro.IsDisp = false;
                LibroDAL.getIstanza().Update(t.libro);
                return (risultato);
            }
        }

        public List<Prestito> ReadGetAll()
        {
            return(this.prestitoList);
        }

        public bool Update(Prestito t)
        {
            bool risultato = false;
            var val = from Prestito in prestitoList
                      where Prestito.Id == t.Id
                      select Prestito;
            if (val == null)
                return risultato;

            using (SqlConnection con = new SqlConnection(credenziali))
            {
                string query = "Update Prestito set dataFine = @dataFine where idPrestito = @id";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@dataFine", t.Data_fine);
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

            prestitoList.Remove((Prestito)val);
            prestitoList.Add(t);

            return (risultato);    
        }
    }

}
