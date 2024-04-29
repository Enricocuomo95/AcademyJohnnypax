namespace ScritturaSuFile
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //questo non funziona perche non ho i permessi di accesso     string path = "C:\\test.tx";
            //string path = "C:\\Users\\Utente\\Desktop\\test.txt";    'questo funge!!'
            //Grazie a get directory name posso prendere il path del progetto Program (nome di questo file)
            //mi prendo l'assembly e ne ricavo la locazione. Sto salvando nella directory del progetto il mio text.txt!!! FIGATA

            //string? path = Path.GetDirectoryName(typeof(Program).Assembly.Location);
            string path = "C:\\Users\\Utente\\Desktop\\test.txt";
            string contenuto = "Enrico Cuomo";

            //using serve per non occupare la memoria 
            //qui sto dichiarando una allocazione di memoria che nasce e muore quando la parentesi si chiude
            //apro inoltre uno stream path per creare 
            //Ho così un metodo che viene eseguito come un applicativo a se.
            //Ovvero quando termina la memoria che ha allocato viene disallocata 

            try {
                //SCRITTURA
                if (path is not null)
                {
                    using (StreamWriter sw = new StreamWriter(path))
                    {
                        sw.WriteLine(contenuto);
                        // non ho bisogno di: sw.Close();
                        //questo per via delle proprietà di using che lo fa per me
                        Console.WriteLine("Tutto ok!");
                    }

                    //LETTURA
                    using (StreamReader sr = new StreamReader(path))
                    {
                        string? line;
                        while ((line = sr.ReadLine()) != null)
                            Console.WriteLine(line);
                    }
                }

            }catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
