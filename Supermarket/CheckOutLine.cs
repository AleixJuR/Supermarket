using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supermarket
{
    public class CheckOutLine
    {
        private int number;
        private Queue<ShoppingCart> queue;
        private Person cashier;
        private bool active;

        public CheckOutLine(Person responsible, int number)
        {
            if (responsible is Cashier caixer)
            {
                cashier = caixer;
                this.number = number;
                queue = new Queue<ShoppingCart>();
                active = true;
            }
            else throw new ArgumentException("La persona responsable ha de ser un caixer");
        }
        public bool Active
        {
            get => active; 
            set => active = value;
        }
        //public bool CheckOut()
        //{
        //    bool result = false;
        //    if (active == false) throw new Exception("La cua no està activa");
        //    if (queue.Count == 0) throw new Exception("No hi han carros");
        //}
    }
}
