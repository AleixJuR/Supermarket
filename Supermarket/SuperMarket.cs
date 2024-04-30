using System;
using System.Collections.Generic;
using System.Linq;
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
        //public CheckOutLine[] = new CheckOutLine[MAXLINES];
        private Dictionary<string, Person> staff = new Dictionary<string, Person>();
        private Dictionary<string, Person> customers = new Dictionary<string, Person>();
        //private SortedDictionary<int,Item> warehouse = new SortedDictionary<int,Item>();
        public Supermarket(string name, string address, string fileCashiers, string fileCustomers, string fileItems, int activeLines)
        {
            this.name = name;
            this.address = address;
            this.activeLines = activeLines;
            this.customers = LoadCustomers(fileCustomers);
            this.staff = LoadCashiers(fileCashiers);
            //this.warehouse = LoadWarehouse(fileItems);
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

        public void MostraClients()
        {
            foreach (KeyValuePair<string, Person> prova in customers) 
            {
                Console.WriteLine(prova.Value);
            }
        }
    }
}
