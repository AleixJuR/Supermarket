namespace Supermarket
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Supermarket prova = new Supermarket("PROVA","PROVA2","CASHIERS.TXT","CUSTOMERS.TXT","GROCERIES.TXT",5);
            //prova.MostraClients();
            //prova.MostraCashiers();
            //prova.MostraItems();
            //Console.WriteLine(prova.GetAvailableCashier());
            //Console.WriteLine(prova.Warehouse[1].PackagingType.ToString());
            ShoppingCart test = new ShoppingCart(prova.GetAvailableCustomer(), DateTime.Now);
            test.AddAllRandomly(prova.Warehouse);
            Console.WriteLine(test);
        }
    }
}