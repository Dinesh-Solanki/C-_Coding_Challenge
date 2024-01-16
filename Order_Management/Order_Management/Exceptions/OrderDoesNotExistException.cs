using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Order_Management.Exceptions
{
    internal class OrderDoesNotExistException :Exception
    {

        public OrderDoesNotExistException() : base("Order does not exist")
        {

        }
    }
}
