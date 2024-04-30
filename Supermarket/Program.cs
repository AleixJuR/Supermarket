namespace Supermarket
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Supermarket prova = new Supermarket("PROVA","PROVA2","CASHIERS.TXT","CUSTOMERS.TXT","CASHIERS.TXT",5);
            prova.MostraClients();
        }
    }
}