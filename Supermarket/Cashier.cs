using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supermarket
{
    public class Cashier : Person
    {
        private DateTime _joiningDate;
        public Cashier(string id, string fullName, DateTime joiningDate) : base(id, fullName)
        {
            _joiningDate = joiningDate;
        }

        public DateTime JoinDate
        {
            get => _joiningDate;
        }
        public int YearsOfService
        {
            get
            {
                TimeSpan dif = DateTime.Now - JoinDate;
                int anys = (int)(dif.Days/365);
                return anys;
            }
        }
        public override double GetRating
        {
            get
            {
                TimeSpan dif = DateTime.Now - JoinDate;
                return dif.Days + TotalInvocied * 0.10;
            }
        }

        public override void AddPoints(int pointsToAdd)
        {
            _points += pointsToAdd * (YearsOfService + 1);
        }

        public override string ToString()
        {
            return $"DNI ->{Id} NOM -> {FullName} \tRATING -> {GetRating} ANTIGUITAT -> {YearsOfService} VENDES ->CANVIAR {base.ToString()}";
        }
    }
}
