using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supermarket
{
    public class ShoppingCart
    {
        private Dictionary<Item, double> shoppingList;
        private Customer customer;
        private DateTime dateOfPurshase;

        public ShoppingCart(Customer customer, DateTime dateOfPurshase)
        {
            shoppingList = new Dictionary<Item, double>();
            this.customer = customer;
            this.dateOfPurshase = dateOfPurshase;
        }

        public void AddOne(Item item, double qty)
        {
            if (item.PackagingType.ToString() != "Kg") Math.Truncate(qty);
            if (shoppingList.ContainsKey(item))
                {
                    shoppingList[item] += qty;
                }
                else
                {
                    shoppingList.Add(item, qty);
                }
        }

        public void AddAllRandomly(SortedDictionary<int,Item > warehouse)
        {
            Random r = new Random();
            Item iRandom;
            int nProductes = r.Next(1, 11);
            try
            {
                for (int i = 0; i < nProductes; i++)
                {
                    int aleatori = r.Next(0, warehouse.Count);
                    int key = warehouse.Keys.ElementAt(aleatori);
                    iRandom = warehouse[key];
                    double quantitat = r.Next(1, 5);
                    AddOne(iRandom, quantitat);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

        }

        public int RawPointsObtainedAtCheckout(double totalInvoiced)
        {
            return Convert.ToInt32(totalInvoiced) % 100;
        }

        public static double ProcessItems(ShoppingCart cart)
        {
            double preuTotal=0;
            double quantitat;
            foreach (KeyValuePair<Item,Double> item in cart.shoppingList)
            {
                quantitat = item.Value;
                if (quantitat > item.Key.Stock) quantitat = item.Key.Stock;
                Item.UpdateStock(item.Key, -quantitat);
                preuTotal += item.Key.Price * quantitat;
            }
            return Math.Round(preuTotal,2);
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder("*******\n");
            sb.Append($"INFO DEL CARRITO DE LA COMPRA DEL CLIENT --> {Customer.FullName}\n");
            foreach (KeyValuePair<Item,Double> llista in shoppingList)
            {
                sb.Append($"{llista.Key.Description,-25} -CAT-> {llista.Key.GetCategory,-25} -QTY-->{llista.Value,-15} -UNIT PRICE->{llista.Key.Price}{llista.Key.Currency}");
                if (llista.Key.OnSale) sb.Append("(*)\n");
                else sb.Append("\n");
            }
            sb.Append("******* FI DE CARRO DE LA COMPRA *******");
            return sb.ToString();
            
        }

        public Dictionary<Item,Double> ShoppingList
        {
            get => shoppingList;
        }

        public Customer Customer
        {
            get => customer;
        }

        public DateTime DateOfPurshase
        {
            get => dateOfPurshase;
        }
    }
}
