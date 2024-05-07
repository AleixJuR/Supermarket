using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supermarket
{
    public class Customer : Person
    {
        private int? fidelity_Card;

        public Customer(string id, string fullName, int? fidelityCard) : base(id, fullName)
        {
            this.fidelity_Card = fidelityCard;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, FullName);
        }
        public override bool Equals(object? obj)
        {
            bool equals = true;
            if (obj is null) equals = this is null;
            else if (obj is Customer other)
            {
                equals = (this.Id.Equals(other.Id) && this.FullName.Equals(other.FullName));
            }
            else equals = false;
            return equals;
        }
        public override double GetRating 
        {
            get => TotalInvocied* 0.2; 
        }

        public int? FidelityCard
        {
            get => fidelity_Card;
        }

        public override void AddPoints(int pointsToAdd)
        {
            if (FidelityCard != null && FullName != "CASH")
            {
                _points += pointsToAdd;
            }   
        }

        public override string ToString()
        {
            return $"DNI/NIE -> {Id,-15} NOM -> {FullName,-35} RATING -> {GetRating,-15} VENDES -> {TotalInvocied,-10}{base.ToString()}";
        }
    }
}
