namespace Supermarket
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Supermarket prova = new Supermarket("PROVA","PROVA2","CASHIERS.TXT","CUSTOMERS.TXT","GROCERIES.TXT",5);
            //Console.WriteLine(prova.GetAvailableCashier());
            //Console.WriteLine(prova.Warehouse[1].PackagingType.ToString());
            //ShoppingCart test = new ShoppingCart(prova.GetAvailableCustomer(), DateTime.Now);
            Person clientPerProva = prova.GetAvailableCustomer();
            Console.WriteLine($"Test del clients -> {clientPerProva.FullName}");
            Console.WriteLine($"Punts Actuals -> {clientPerProva.Points} S'afegiràn 5 punts");
            clientPerProva.AddPoints(5);
            Console.WriteLine($"Punts Actualitzats -> {clientPerProva.Points}");
            Console.WriteLine($"RATING SENSE HAVER COMPRAT -> {clientPerProva.GetRating}");
            clientPerProva.AddInvoiceAmount(5);
            Console.WriteLine($"RANKING AL HAVER FET UNA COMPRA -> {clientPerProva.GetRating}");
            Console.WriteLine("TEST COMPARABLE DE PERSONES");
            Person caixer1 = prova.GetAvailableCashier();
            Person client1 = prova.GetAvailableCustomer();
            Person caixer2 = prova.GetAvailableCashier();
            client1.AddInvoiceAmount(5);    
            client1.AddInvoiceAmount(5);
            caixer1.AddInvoiceAmount(3);
            Console.WriteLine("PERSONES A COMPARAR");
            Console.WriteLine(caixer1);
            Console.WriteLine(client1);
            Console.WriteLine(caixer2);
            Console.WriteLine("ELS AFEGEIXO EN UNA ARRAY I FAIG UN SORT");
            Person[] persones = new Person[3];
            persones[0] = caixer1;
            persones[1] = caixer2;
            persones[2] = client1;
            Array.Sort(persones);
            foreach (Person person in persones)
            {
                Console.WriteLine(person);
            }

            Cashier caixer = (Cashier)caixer1;
            Console.WriteLine($"Years of Service del caixer {caixer.FullName} -> {caixer.YearsOfService}, va entrar el dia {caixer.JoinDate}");
            Console.ReadKey();
            Console.Clear();
            Console.WriteLine("CLASSE ITEM");
            Item item1 = new Item("Apple", (int)Item.Category.FRUITS, 'U', 1.99);
            Item item2 = new Item("Milk", (int)Item.Category.MILK_AND_DERIVATIVES, 'P', 2.49);
            Item item3 = new Item("Bread", (int)Item.Category.BREAD, 'P', 0.99);

            Console.WriteLine("Informació d'items exemple");
            Console.WriteLine(item1);
            Console.WriteLine(item2);
            Console.WriteLine(item3);
            Console.WriteLine();


            Item.UpdateStock(item1, 10);
            Item.UpdateStock(item2, 3);
            Item.UpdateStock(item3, 5);


            Console.WriteLine("Informació després d'actualitzar stock:");
            Console.WriteLine(item1);
            Console.WriteLine(item2);
            Console.WriteLine(item3);
            Console.WriteLine("Al haver actualitzat l'stock, al afegir en una array, podrem fer un sort que farà servir el compareto");
            Item[] items = new Item[3];
            items[0] = item1;
            items[1] = item2;
            items[2] = item3;
            Array.Sort(items);
            foreach (Item i in items) { Console.WriteLine(i); }
            Console.WriteLine("LES PROVES ANTERIORS S'HAN FET A PARTIR DE LA CARREGA DELS FITXERS DE SUPERMARKET, AQUEST ES EL TOSTRING AMB TOT EL QUE S'HA AFEGIT");
            Console.WriteLine("CLICK PER ACCEDIR");
            Console.ReadKey();
            Console.Clear();
            Console.WriteLine("CUSTOMERS");
            prova.MostraClients();
            Console.WriteLine("CAIXERS");
            prova.MostraCashiers();
            Console.WriteLine("ITEM");
            prova.MostraItems();
       
        }
    }
}