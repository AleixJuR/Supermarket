using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Supermarket
{
    public class Item : IComparable<Item>
    {
        public const int MINSTOCK = 5;
        public enum Category
        {
            Beverage = 1, FRUITS, VEGETABLES, BREAD, MILK_AND_DERIVATIVES, GARDEN, MEAT, SWEETS, SAUCES, FROZEN, CLEANING, FISH, OTHER
        };
        public enum Packaging
        {
            Unit, Kg, Package
        };

        private char currency = '\u20AC';
        private static int codeInfo = 1;
        private int code;
        private string description;
        private bool onSale;
        private double price;
        private Category category;
        private Packaging packaging;
        private double stock;
        private int minStock;


        public Item(string description, int category, char packaging, double price)
        {
            this.code = codeInfo++;
            Random r = new Random();
            this.description = description;
            //this.onSale = onSale;
            this.price = price;
            this.category = (Category)category;
            if (packaging == 'U') this.packaging = Packaging.Unit;
            else if (packaging == 'K') this.packaging = Packaging.Kg;
            else this.packaging = Packaging.Package;
            minStock = r.Next(20)+1;
            stock = r.Next(MinStock,200);
            this.onSale = r.Next(5) + 1 == 4;
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(Code,description);
        }
        public override bool Equals(object? obj)
        {
            bool equals;
            if (obj is null) equals = this is null;
            else if (obj is Item other)
            {
                equals = (this.Code.Equals(other.Code));
            }
            else equals = false;
            return equals;
        }
        
        public char Currency
        {
            get => currency;
        }

        public int Code
        {
            get => code;
        }
        public double Stock
        {
            get => this.stock;
        }
        public int MinStock
        {
            get => this.minStock;
        }
        public Category GetCategory
        {
            get => this.category;
        }
        public Packaging PackagingType
        {
            get => this.packaging;
        }
        public string Description
        {
            get => this.description;
        }
        public bool OnSale
        {
            get => this.onSale;
        }

        public static void UpdateStock(Item item, double qty)
        {
            item.stock += qty;
        }
        public double Price
        {
            get 
            {
                double priceR = price;
                if (onSale) priceR = this.price * 0.9;
                return Math.Round(priceR,2);
            }
            
        }
        public override string ToString()
        {
            string saleStatus = OnSale ? $"Y({Price})" : "N";
            return $"Code -> {Code,-10} Description -> {Description,-25} Category -> {GetCategory,-25} Stock: {Stock,-5} MinStock -> {MinStock,-5} Price {Math.Round(this.price) +""+ currency,-10} ON SALE -> {saleStatus}";
        }

        public int CompareTo(Item? other)
        {
            int result;
            if (other == null) result = 1;
            else
            {
                result = this.Stock.CompareTo(other.Stock);
                if (result == 0) result = Code.CompareTo(other.Code);
            }
            return result;
        }


       
    }

}