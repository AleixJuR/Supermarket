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
                active = false;
            }
            else throw new ArgumentException("La persona responsable ha de ser un caixer");
        }
        public bool Active
        {
            get => active;
            set => active = value;
        }


        public bool CheckIn(ShoppingCart oneShoppingCart)
        {
            bool result = true;
            if (queue.Contains(oneShoppingCart)) throw new Exception("Ja hi ha aquest carro de la compra");
            try
            {
                if (!Active) throw new Exception("Cua Inactiva");
                queue.Enqueue(oneShoppingCart);
            }
            catch (Exception ex)
            {
                result = false;
            }
            return result;
        }

        public Queue<ShoppingCart> Queue
        {
            get => queue;
        }
        public bool CheckOut()
        {
            bool result = true;

            try
            {
                if (!Active) throw new Exception("La cua no està activa");
                if (queue.Count == 0) throw new Exception("No hi han carros");
                ShoppingCart desencua = queue.Dequeue();
                double preuTotal = ShoppingCart.ProcessItems(desencua);
                
                //Acabar
                desencua.Customer.Active = false;
            }
            catch (Exception e)
            {
                result = false;
            }
            return result;
        }

        public override string ToString()
        {
            StringBuilder result= new StringBuilder($"NUMERO DE CAIXA --> {this.number}\nCAIXER /A A CÀRREC -> {this.cashier.FullName}\n*********\n");
            if (queue.Count == 0) result.Append("CUA BUIDA");
            else
            {
                foreach (ShoppingCart compra in queue)
                {
                    result.Append(compra);
                }
            }
            return result.ToString();

        }
    }
}
