using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supermarket 
{
    public abstract class Person : IComparable<Person> 
    {
        private string _id;
        protected string _fullName;
        protected int _points = 0;
        private double _totalInvocied = 0;
        private bool active = false;
        protected Person(string id, string fullName, int points) 
        {
            _id = id;
            _fullName = fullName;
            _points = points;
        }
        protected Person(string id, string fullName) :this(id,fullName,0) { }
        public string FullName
        {
            get => _fullName;
        }

        public int Points
        {
            get => _points;
        }
        

        public bool Active
        {
            get => active;
            set => active = value;
        }
        public abstract double GetRating
        {
            get;
        }
        public string Id { get => _id; }
        protected double TotalInvocied { get => _totalInvocied; }

        public int CompareTo(Person? other)
        {
            int result;
            if (other is null) result = -1;
            else
            {
                result = this.GetRating.CompareTo(other.GetRating);
            }
            return result;
        }
        public void AddInvoiceAmount(double amount) 
        {
            this._totalInvocied+= amount;   
        }

        public abstract void AddPoints(int pointsToAdd);
        public override string ToString()
        {
            return Active ? "DISPONIBLE -> NO" : "DISPONIBLE -> SÍ";
        }

    }
}
