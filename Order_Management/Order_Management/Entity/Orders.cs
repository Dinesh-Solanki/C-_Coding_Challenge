using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order_Management.Entity
{
    internal class Orders
    {
        private int orderid;
        private DateTime orderdate;

        public int Orderid
        {
            get { return orderid; }
            set { orderid = value; }
        }
        public DateTime Orderdate
        {
            get { return orderdate; }
            set { orderdate = value; }
        }
        public Orders()
        {

        }

        public Orders(int orderid, DateTime orderdate)
        {
            Orderid = orderid;
            Orderdate = orderdate;
        }
    }
}