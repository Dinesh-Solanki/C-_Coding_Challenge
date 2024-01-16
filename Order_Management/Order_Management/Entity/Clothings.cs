using Order_Management.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order_Management.Entity
{
    internal class Clothings : Products
    {
        private string size;
        private string color;

        public string Size
        {
            get { return size; }
            set { size = value; }
        }

        public string Color
        {
            get { return color; }
            set { color = value; }
        }

        public Clothings()
        {

        }

        public Clothings(int productId, string productName, string description, decimal price, int quantityInStock, string _type, string _size, string _color) : base(productId, productName, description, price, quantityInStock, _type)
        {
            Size = _size;
            Color = _color;
        }
    }
}