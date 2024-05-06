using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
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
            if (shoppingList.ContainsKey(item))
            {
                shoppingList[item] += qty;
            }
            else
            {
                shoppingList.Add(item, qty);
            }
        }

        public void AddAllRandomly(SortedDictionary<Item, double> warehouse)
        {
            Random r = new Random();
            int nProductes = r.Next(1, 11);
            for (int i = 0; i < nProductes;i++)
            {

                
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
                preuTotal += item.Key.Price * quantitat;
            }
            return preuTotal;
        }
    }
}
