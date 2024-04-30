using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supermarket
{
    public abstract class Person
    {
        protected string _id;
        protected string _fullName;
        protected int _points;
        private bool active = false;
        protected Person(string id, string fullName, int points) 
        {
            _id = id;
            _fullName = fullName;
            _points = points;
        }
        protected Person(string id, string fullName) :this(id,fullName,0) { }
        public string fullName
        {
            get => _fullName;
        }

    }
}
