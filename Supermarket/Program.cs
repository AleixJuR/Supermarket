using System.Runtime.InteropServices;

namespace Supermarket
{
    internal class Program
    {
        public static void MostrarMenu()
        {
            Console.WriteLine("1- UN CLIENT ENTRA AL SUPER I OMPLE EL SEU CARRO DE LA COMPRA");
            Console.WriteLine("2- AFEGIR UN ARTICLE A UN CARRO DE LA COMPRA");
            Console.WriteLine("3- UN CARRO PASSA A CUA DE CAIXA (CHECKIN)");
            Console.WriteLine("4- CHECKOUT DE CUA TRIADA PER L'USUARI");
            Console.WriteLine("5- OBRIR SEGÜENT CUA DISPONIBLE");
            Console.WriteLine("6- INFO CUES");
            Console.WriteLine("7- CLIENTS VOLTANT PEL SUPERMERCAT");
            Console.WriteLine("8- LLISTAR CLIENTS PER RATING (DESCENDENT)");
            Console.WriteLine("9- LLISTAR ARTICLES PER STOCK (DE  - A  +)");
            Console.WriteLine("A- CLOSE QUEUE");
            Console.WriteLine("0- EXIT");
        }
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;


            SuperMarket super = new SuperMarket("HIPERCAR", "C/Barna 99", "CASHIERS.TXT", "CUSTOMERS.TXT", "GROCERIES.TXT", 2);
            //
            Dictionary<Customer, ShoppingCart> carrosPassejant = new Dictionary<Customer, ShoppingCart>();

            ConsoleKeyInfo tecla;
            do
            {
                Console.Clear();
                MostrarMenu();
                tecla = Console.ReadKey();
                switch (tecla.Key)
                {
                    case ConsoleKey.D1:
                        DoNewShoppingCart(carrosPassejant, super);
                        break;
                    case ConsoleKey.D2:
                        DoAfegirUnArticleAlCarro(carrosPassejant, super);

                        break;
                    case ConsoleKey.D3:
                        DoCheckIn(carrosPassejant, super);

                        break;
                    case ConsoleKey.D4:
                        Console.Clear();
                        if (DoCheckOut(super)) Console.WriteLine("BYE BYE. HOPE 2 SEE YOU AGAIN!");
                        else Console.WriteLine("NO S'HA POGUT TANCAR CAP COMPRA");
                        MsgNextScreen("PREM UNA TECLA PER ANAR AL MENÚ PRINCIPAL");

                        break;
                    case ConsoleKey.D5:
                        DoOpenCua(super);

                        break;
                    case ConsoleKey.D6:
                        DoInfoCues(super);

                        break;

                    case ConsoleKey.D7:
                        DoClientsComprant(carrosPassejant);


                        break;
                    case ConsoleKey.D8:
                        DoListCustomers(super);

                        break;

                    case ConsoleKey.D9:
                        SortedSet<Item> articlesOrdenatsPerEstoc = super.GetItemsByStock();
                        DoListArticlesByStock("LLISTAT D'ARTICLES - DATA " + DateTime.Now, articlesOrdenatsPerEstoc);

                        break;
                    case ConsoleKey.A:
                        DoCloseQueue(super);

                        break;

                    case ConsoleKey.D0:
                        MsgNextScreen("PRESS ANY KEY 2 EXIT");
                        break;
                    default:
                        MsgNextScreen("Error. Prem una tecla per tornar al menú...");
                        break;
                }

            } while (tecla.Key != ConsoleKey.D0);


        }
        //OPCIO 1 - Entra un nou client i se li assigna un carro de la compra. S'omple el carro de la compra
        /// <summary>
        /// Crea un nou carro de la compra assignat a un Customer inactiu
        /// L'omple d'articles aleatòriament 
        /// i l'afegeix als carros que estan passejant pel super
        /// </summary>
        /// <param name="carros">Llista de carros que encara no han entrat a cap 
        /// cua de pagament</param>
        /// <param name="super">necessari per poder seleccionar un client inactiu</param>
        public static void DoNewShoppingCart(Dictionary<Customer, ShoppingCart> carros, SuperMarket super)
        {
            Console.Clear();
            Customer client = super.GetAvailableCustomer();
            carros.Add(client, new ShoppingCart(client, DateTime.Now));
            carros[client].AddAllRandomly(super.Warehouse);
            Console.WriteLine(carros[client]);
            MsgNextScreen("PREM UNA TECLA PER ANAR AL MENÚ PRINCIPAL");
        }

        //OPCIO 2 - AFEGIR UN ARTICLE ALEATORI A UN CARRO DE LA COMPRA ALEATORI DELS QUE ESTAN VOLTANT PEL SUPER
        /// <summary>
        /// Dels carros que van passejant pel super, 
        /// es selecciona un carro a l'atzar i un article a l'atzar
        /// i s'afegeix al carro de la compra
        /// amb una quantitat d'unitats determinada
        /// Cal mostrar el carro seleccionat abans i després d'afegir l'article.
        /// </summary>
        /// <param name="carros">Llista de carros que encara no han entrat a cap 
        /// cua de pagament</param>
        /// <param name="super">necessari per poder seleccionar un article del magatzem</param>
        public static void DoAfegirUnArticleAlCarro(Dictionary<Customer, ShoppingCart> carros, SuperMarket super)
        {
            if (carros.Count == 0) Console.WriteLine("NO HI HA CARROS VOLTANT");
            else
            {
                Console.Clear();
                Random r = new Random();
                int n = r.Next(carros.Count);
                ShoppingCart seleccionat = carros.Values.ElementAt(n);
                Console.WriteLine($"CARRO SENSE ACTUALITZAR: \n{seleccionat}");
                Item iRandom = super.Warehouse.Values.ElementAt(r.Next(super.Warehouse.Count));
                seleccionat.AddOne(iRandom, r.Next(1, 5));
                Console.WriteLine($"CARRO ACTUALITZAT: \n{seleccionat}");
            }
            
            MsgNextScreen("PREM UNA TECLA PER ANAR AL MENÚ PRINCIPAL");

        }
        // OPCIO 3 : Un dels carros que van pululant pel super  s'encua a una cua activa
        // La selecció del carro i de la cua és aleatòria
        /// <summary>
        /// Agafem un dels carros passejant (random) i l'encuem a una de les cues actives
        /// de pagament.
        /// També hem d'eliminar el carro seleccionat de la llista de carros que passejen 
        /// Si no hi ha cap carro passejant o no hi ha cap linia activa, cal donar missatge 
        /// 
        /// </summary>
        /// <param name="carros">Llista de carros que encara no han entrat a cap 
        /// cua de pagament</param>
        /// <param name="super">necessari per poder encuar un carro a una linia de caixa</param>
        public static void DoCheckIn(Dictionary<Customer, ShoppingCart> carros, SuperMarket super)
        {
            Console.Clear();
            if (super.ActiveLines == 0) Console.WriteLine("NO HI HA CAP CAIXA");
            else if (carros.Count == 0) Console.WriteLine("NO HI HA CARROS VOLTANT");
            else
            {
                Random r = new Random();
                Customer cRandomKey = null;
                ShoppingCart cRandom = null;
                CheckOutLine caixa = null;
                caixa = super.GetCheckOutLine(r.Next(super.ActiveLines)+1);
                cRandomKey = carros.Keys.ElementAt(r.Next(carros.Count));
                cRandom = carros[cRandomKey];
                caixa.CheckIn(cRandom);
                carros.Remove(cRandomKey);
                Console.WriteLine(caixa);
            }
            
            MsgNextScreen("PREM UNA TECLA PER ANAR AL MENÚ PRINCIPAL");
        }

        // OPCIO 4 - CHECK OUT D'UNA CUA TRIADA PER L'USUARI
        /// <summary>
        /// Es demana per teclat una cua de les actives (1..ActiveLines)
        /// i es fa el checkout del ShoppingCart que toqui
        /// Si no hi ha cap carro a la cua triada, es dona un missatge
        /// </summary>
        /// <param name="super">necessari per fer el checkout</param>
        /// <returns>true si s'ha pogut fer el checkout. False en cas contrari</returns>

        public static bool DoCheckOut(SuperMarket super)
        {
            bool fet = true;
            int cua=0;
            bool correcte = false;
            CheckOutLine cuaTriada = null;
            CheckOutLine cuaCKOut = null;
            if (super.ActiveLines == 0) 
            {
                Console.WriteLine("CAP CUA ACTIVA");
                fet = false;
            } 
            else
            {
                while (!correcte)
                {
                    try
                    {
                        Console.Write($"Tria una cua (1-{super.ActiveLines} --> )");
                        cua = Convert.ToInt32(Console.ReadLine());
                        if (cua < 0 || cua > super.ActiveLines) throw new Exception("Cua no vàlida");
                        cuaCKOut = super.GetCheckOutLine(cua);
                        if (cuaCKOut.Queue.Count == 0) fet = false;
                        else
                        {
                            cuaCKOut.CheckOut();
                        }
                        correcte = true;
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        fet = false;
                    }
                }
                cuaTriada = super.GetCheckOutLine(cua);
                cuaTriada.CheckOut();
            }

            return fet;
        }
        /// <summary>
        /// En cas que hi hagin cues disponibles per obrir, s'obre la 
        /// següent linia disponible
        /// </summary>
        /// <param name="super"></param>
        /// <returns>true si s'ha pogut obrir la cua</returns>
        // OPCIO 5 : Obrir la següent cua disponible (si n'hi ha)
        public static bool DoOpenCua(SuperMarket super)
        {
            bool fet = true;
            if (super.ActiveLines < super.MaxLines)
            {
                super.OpenCheckOutLine(super.ActiveLines+1);
                Console.WriteLine(super.GetCheckOutLine(super.ActiveLines));
            }
            else fet = false;
            MsgNextScreen("PREM UNA TECLA PER ANAR AL MENÚ PRINCIPAL");
            return fet;
        }

        //OPCIO 6 : Llistar les cues actives
        /// <summary>
        /// Es llisten totes les cues actives des de la 1 fins a ActiveLines.
        /// Apretar una tecla després de cada cua activa
        /// </summary>
        /// <param name="super"></param>
        public static void DoInfoCues(SuperMarket super)
        {
            Console.Clear();
            for (int i = 0; i < super.ActiveLines;i++)
            {
                Console.WriteLine(super.GetCheckOutLine(i + 1));
            }
            MsgNextScreen("PREM UNA TECLA PER CONTINUAR");

        }


        // OPCIO 7 - Mostrem tots els carros de la compra que estan voltant
        // pel super però encara no han anat a cap cua per pagar
        /// <summary>
        /// Es mostren tots els carros que no estan en cap cua.
        /// Cal apretar una tecla després de cada carro
        /// </summary>
        /// <param name="carros"></param>
        public static void DoClientsComprant(Dictionary<Customer, ShoppingCart> carros)
        {
            Console.Clear();
            if (carros.Count == 0) Console.WriteLine("NO HI CAP CAP CARRO VOLTANT PEL SUPERMERCAT");
            foreach (KeyValuePair<Customer,ShoppingCart> voltant in carros)
            {
                Console.WriteLine(voltant.Value);
            }
            MsgNextScreen("PREM UNA TECLA PER CONTINUAR");

        }

        //OPCIO 8 : LListat de clients per rating
        /// <summary>
        /// Cal llistar tots els clients de més a menys rating
        /// Per poder veure bé el llistat, primer heu de fer uns quants
        /// checkouts i un cop fets, fer el llistat. Aleshores els
        /// clients que han comprat tindran ratings diferents de 0
        /// Jo he fet una sentencia linq per solucionar aquest apartat
        /// </summary>
        /// <param name="super"></param>
        public static void DoListCustomers(SuperMarket super)
        {

            Console.Clear();
            List<Customer> list = new List<Customer>(); 
            foreach (Customer c in super.Customers.Values) list.Add(c);
            list.Sort();
            foreach (Customer c in list) Console.WriteLine(c);
            MsgNextScreen("PREM UNA TECLA PER CONTINUAR");

        }

        // OPCIO 9
        /// <summary>
        /// Llistar de menys a més estoc, tots els articles del magatzem
        /// </summary>
        /// <param name="header">Text de capçalera del llistat</param>
        /// <param name="items">articles que ja vindran preparats en la ordenació desitjada</param>
        public static void DoListArticlesByStock(String header, SortedSet<Item> items)
        {
            Console.Clear();
            Console.WriteLine(header);
            foreach (Item i in items) Console.WriteLine(i);
            MsgNextScreen("PREM UNA TECLA PER CONTINUAR");
        }

        // OPCIO A : Tancar cua. Només si no hi ha cap client
        /// <summary>
        /// Començant per la última cua disponible, tanca la primera que trobi sense
        /// cap carro encuat. (primer mirem la número "ActiveLines" després ActiveLines-1 ...
        /// Fins trobar una que estigui buida. La que trobem la eliminarem
        /// Cal afegir la propietat Empty a la classe ChecOutLine i  a la classe SuperMarket:
        /// el mètode public static bool RemoveQueue(Supermarket super, int lineToRemove)
        /// que elimina la cua amb número = lineToRemove i retorna true en cas que l'hagi 
        /// pogut eliminar (perquè no hi ha cap carro pendent de pagament)
        /// </summary>
        /// <param name="super"></param>
        public static void DoCloseQueue(SuperMarket super)
        {
            Console.Clear();
            bool success = false;
            int linia = super.ActiveLines;
            while (!success && linia > 0)
            {
                bool res = SuperMarket.RemoveQueue(super, linia);
                if (res)
                {
                    Console.WriteLine($"Cua {linia} eliminada correctament");
                    success = true;
                }
                else 
                {
                    linia--;
                    res = SuperMarket.RemoveQueue(super, linia);
                }
            }
            if (linia == 0) Console.WriteLine("No s'ha pogut tancar cap línia");
            MsgNextScreen("PREM UNA TECLA PER CONTINUAR");
        }


        public static void MsgNextScreen(string msg)
        {
            Console.WriteLine(msg);
            Console.ReadKey();
        }



    }
}