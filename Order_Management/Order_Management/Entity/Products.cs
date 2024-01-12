using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order_Management.Entity
{
    internal class Products
    {
        private int productId;
        private string productName;
        private string description;
        private decimal price;
        public int quantityInStock;
        public string Type;


        public int product_id
        {
            get { return productId; }
            set { productId = value; }
        }
        public string ProductName
        {
            get { return productName; }
            set { productName = value; }
        }
        public string Description
        {
            get { return description; }
            set { description = value; }
        }
        public decimal Price
        {
            get { return price; }
            set { price = value; }
        }
        public string type
        {
            get { return Type; }
            set { Type = value; }
        }

        public Products()
        {

        }

        public Products(int productId, string productName, string description, decimal price, int quantityInStock, string _type)
        {

            product_id = product_id;
            ProductName = productName;
            Description = description;
            Price = price;
            type = _type;
        }
    }
}