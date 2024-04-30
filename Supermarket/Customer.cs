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
            if (FidelityCard != null)
            {
                _points += pointsToAdd;
            }   
        }

        public override string ToString()
        {
            return $"DNI/NIE->{Id} NOM-> {FullName}\tRATING ->{GetRating}  VENDES->{TotalInvocied}  {base.ToString()}";
        }
    }
}
