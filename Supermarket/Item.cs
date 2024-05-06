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
        private static int codeInfo = 0;
        private int code;
        private string description;
        private bool onSale;
        private double price;
        private Category category;
        private Packaging packaging;
        private double stock;
        private int minStock;

        public Item(string description, int category, char packaging, double price)
        :this(description,category,packaging,price,MINSTOCK,false)
        {
            

        }

        public Item(string description, int category, char packaging, double price, double stock, bool onSale)
        {
            this.code = codeInfo++;
            this.description = description;
            //this.onSale = onSale;
            this.price = price;
            this.category = (Category)category;
            if (packaging == 'U') this.packaging = Packaging.Unit;
            else if (packaging == 'K') this.packaging = Packaging.Kg;
            else this.packaging = Packaging.Package;
            if (stock < minStock) throw new Exception("S'ha de cumplir l'stock mínim");
            this.onSale = onSale;
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(Code);
        }
        public override bool Equals(object? obj)
        {
            bool equals = true;
            if (obj is null) equals = this is null;
            else if (obj is Item other)
            {
                equals = (this.Code.Equals(other.Code));
            }
            else equals = false;
            return equals;
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
            if (item.stock < item.minStock) { UpdateStock(item, 5); }
        }
        public double Price
        {
            get 
            {
                double price;
                if (onSale) price = this.price * 0.9;
                else price = this.price;
                return price;
            }
            
        }
        public override string ToString()
        {
            string saleStatus = OnSale ? $"Y({Price})" : "N";

            return $"Code -> {Code} Description -> {Description}      Category -> {GetCategory}      Stock: {Stock} MinStock -> {MinStock}" +
                $" Price {this.price}{currency} ON SALE -> {saleStatus}";
        }

        public int CompareTo(Item? other)
        {
            int result;
            if (other == null) result = -1;
            else
            {
                result = this.Stock.CompareTo(other.Stock);
            }
            return result;
        }

       
    }

}