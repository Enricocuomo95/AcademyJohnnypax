namespace Ristorante
{
    internal class Program
    {
        static void Main(string[] args)
        {
            float temperatura;

            try
            {
                do
                {
                    Console.WriteLine("dammi la temperatura del cliente");
                    temperatura = float.Parse(Console.ReadLine());

                    if ((35 > temperatura) || (temperatura >= 42))
                        Console.WriteLine("Il cliente è malato");
                    else if (temperatura >= 37.5)
                        Console.WriteLine("ok puoi entrare");
                    else
                        Console.WriteLine("Mi dispiace non puoi entrare");

                    Console.WriteLine("Ci sono altri clienti?");

                } while (!Console.ReadLine().Equals("no"));

            }
            catch (IOException ex)
            {
                Console.WriteLine(ex.ToString());
            }
            catch (OutOfMemoryException ex)
            {
                Console.WriteLine(ex.ToString());
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Console.WriteLine(ex.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally {
                Console.WriteLine("Tu sei terminato!!!!");
            }
        }
    }
}
