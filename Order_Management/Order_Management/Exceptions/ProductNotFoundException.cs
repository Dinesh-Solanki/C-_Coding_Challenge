using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order_Management.Exceptions
{
    internal class ProductNotFoundException :Exception
    {
        public ProductNotFoundException() : base("Product does not exist")
        {

        }
    }
}
