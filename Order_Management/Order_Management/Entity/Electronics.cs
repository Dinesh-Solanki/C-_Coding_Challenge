using Order_Management.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order_Management.Entity
{
    internal class Electronics : Products
    {
        private string brand;
        private int warrantyPeriod;

        public string Brand
        {
            get { return brand; }
            set { brand = value; }
        }

        public int WarrantyPeriod
        {
            get { return warrantyPeriod; }
            set { warrantyPeriod = value; }
        }

        public Electronics() : base()
        {

        }

        public Electronics(int productId, string productName, string description, decimal price, int quantityInStock, string _type, string _brand, int _warrantyPeriod) : base(productId, productName, description, price, quantityInStock, _type)
        {
            Brand = _brand;
            WarrantyPeriod = _warrantyPeriod;
        }
    }
}