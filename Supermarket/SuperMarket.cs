using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Supermarket
{
    public class Supermarket
    {
        private string name;
        private string address;
        private static int MAXLINES = 5;
        private int activeLines;
        private  CheckOutLine[] lines = new CheckOutLine[MAXLINES];
        private Dictionary<string, Person> staff = new Dictionary<string, Person>();
        private Dictionary<string, Person> customers = new Dictionary<string, Person>();
        private SortedDictionary<int,Item> warehouse = new SortedDictionary<int,Item>();
        public Supermarket(string name, string address, string fileCashiers, string fileCustomers, string fileItems, int activeLines)
        {
            this.name = name;
            this.address = address;
            if (activeLines > MAXLINES) throw new ArgumentException("No poden haver més línies del màxim");
            this.activeLines = activeLines;
            this.customers = LoadCustomers(fileCustomers);
            this.staff = LoadCashiers(fileCashiers);
            this.warehouse = LoadWarehouse(fileItems);
            for (int i = 0;i<activeLines;i++)
            {
                OpenCheckOutLine(i + 1);
            }
            
        }

        public void OpenCheckOutLine(int line2Open)
        {
            if (line2Open < 0 || line2Open > MAXLINES) throw new ArgumentException("La línia oberta ha d'estar entre 1 i el màxim");
            if (this.lines[line2Open-1] != null)
            {
                if (lines[line2Open - 1].Active == true) throw new Exception("La caixa ja és oberta");
                lines[line2Open - 1].Active = true;
            } 
            else
            {
                lines[line2Open-1] = new CheckOutLine(GetAvailableCashier(), line2Open);
            }
        }

        public CheckOutLine GetCheckOutLine(int lineNumber)
        {
            CheckOutLine result;
            if (lines[lineNumber - 1] == null || lineNumber - 1 > lines.Length || lineNumber - 1 < 0) result = null;
            else
            {
                result = lines[lineNumber - 1];
            }
            return result;
        }

        public bool JoinTheQueue(ShoppingCart theCart, int line)
        {
            bool result = true;
            try
            {
                if (lines[line - 1] == null || line - 1 > lines.Length || line - 1 < 0) throw new Exception("Linia no vàlida");
                lines[line - 1].CheckIn(theCart);
            }
            catch(Exception e)
            {
                result = false;
            }
            return result;
        }
        public bool CheckOut(int line)
        {
            bool result = true;
            try
            {
                if (lines[line - 1] == null || line - 1 > lines.Length || line - 1 < 0) throw new Exception("Linia no vàlida");
                lines[line - 1].CheckOut();
            }
            catch (Exception e)
            {
                result = false;
            }
            return result;

        }

        
        private Dictionary<string,Person> LoadCustomers(string fileName)
        {
            Dictionary<string, Person> customers = new Dictionary<string, Person>();
            StreamReader sr = new StreamReader(fileName);
            string linia = sr.ReadLine();
            string[] data;
            while (linia != null)
            {
                data = linia.Split(';');
                int? fidelity;
                if (data[2] == "") fidelity = null;
                else fidelity = Convert.ToInt32(data[2]);
                Person personaActual = new Customer(data[0], data[1], fidelity);
                customers.Add(data[0], personaActual);
                linia = sr.ReadLine();
            }
            sr.Close();
            return customers;
        }

        private Dictionary<string,Person> LoadCashiers(string fileName)
        {
            Dictionary<string, Person> cashiers = new Dictionary<string, Person>();
            StreamReader sr = new StreamReader(fileName);
            string linia = sr.ReadLine();
            string [] data;
            while (linia != null)
            {
                data = linia.Split(';');
                Person personaActual = new Cashier(data[0], data[1], Convert.ToDateTime(data[3]));
                cashiers.Add(data[0], personaActual);
                linia = sr.ReadLine();
            }
            sr.Close();
            return cashiers;
        }

        private SortedDictionary<int,Item> LoadWarehouse(string fileName)
        {
            SortedDictionary<int,Item> warehouse = new SortedDictionary<int,Item>();   
            StreamReader sr = new StreamReader(fileName);
            string linia = sr.ReadLine();
            string[] data;
            while (linia != null)
            {
                data = linia.Split(';');
                Item itemActual = new Item(data[0], Convert.ToInt32(data[1]), Convert.ToChar(data[2]), Convert.ToDouble(data[3]));
                warehouse.Add(itemActual.Code, itemActual);
                linia = sr.ReadLine();
            }
            sr.Close();
            return warehouse;
        }

        public Customer GetAvailableCustomer()
        {
            bool trobat = false;
            Customer cRandom = null;
            bool algunDisponible = false;
            Random r = new Random();
            IEnumerator<KeyValuePair<String,Person>> enumerador = customers.GetEnumerator();    
            while (enumerador.MoveNext() && !algunDisponible)
            {
                if (!enumerador.Current.Value.Active) algunDisponible = true;
            }
            if (algunDisponible)
            {
                while (!trobat)
                {
                    int aleatori = r.Next(0, customers.Count);
                    string clau = customers.Keys.ElementAt(aleatori);
                    cRandom = (Customer)customers[clau];
                    if (!cRandom.Active)
                    {
                        trobat = true;
                        cRandom.Active = true;
                    }
                }
            }
            return cRandom;
        }

        public Cashier GetAvailableCashier()
        {
            bool trobat = false;
            Cashier cRandom = null;
            bool algunDisponible = false;   
            Random r = new Random();
            IEnumerator<KeyValuePair<String, Person>> enumerador = staff.GetEnumerator();
            while (enumerador.MoveNext() && !algunDisponible)
            {
                if (!enumerador.Current.Value.Active) algunDisponible = true;
            }
            if (algunDisponible)
            {
                while (!trobat)
                {
                    int aleatori = r.Next(0, staff.Count);
                    string clau = staff.Keys.ElementAt(aleatori);
                    cRandom = (Cashier)staff[clau];
                    if (!cRandom.Active)
                    {
                        trobat = true;
                        cRandom.Active = true;
                    }
                }
            }
            return cRandom;
        }

        public SortedSet<Item> GetItemsByStock()
        {
            SortedSet<Item> items = new SortedSet<Item>();
            foreach (KeyValuePair<int,Item> item in warehouse)
            {
                items.Add(item.Value);
            }
            return items;
        }

        public SortedDictionary<int,Item> Warehouse
        {
            get => warehouse;
        }

        public Dictionary<String,Person> Customers
        {
            get => customers;
        }

        public Dictionary<String,Person> Staff
        {
            get => staff;
        }

        public int ActiveLines
        {
            get => activeLines;
        }


        public void MostraClients()
        {
            foreach (KeyValuePair<string, Person> prova in customers) 
            {
                Console.WriteLine(prova.Value);
            }
        }

        public void MostraCashiers()
        {
            foreach (KeyValuePair<string, Person> prova in staff)
            {
                Console.WriteLine(prova.Value);
            }
        }

        public void MostraItems()
        {
            foreach (KeyValuePair<int, Item> prova in warehouse)
            {
                Console.WriteLine(prova.Value);
            }
        }

        public override string ToString()
        {
            return$"{this.name}\n{this.address}\n{lines}";
        }
    }
}
