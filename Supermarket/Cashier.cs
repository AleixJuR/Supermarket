﻿using System;
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

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, FullName);
        }
        public override bool Equals(object? obj)
        {
            bool equals = true;
            if (obj is null) equals = this is null;
            else if (obj is Cashier other)
            {
                equals = (this.Id.Equals(other.Id) && this.FullName.Equals(other.FullName));
            }
            else equals = false;
            return equals;
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
            return $"DNI -> {Id,-15} NOM -> {FullName,-35} RATING -> {Math.Round(GetRating,2),-15} ANTIGUITAT -> {YearsOfService,-10} VENDES -> {base.TotalInvocied} {base.ToString()}";
        }

    }
}
