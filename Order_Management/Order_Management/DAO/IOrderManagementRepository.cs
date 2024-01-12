using Order_Management.Entity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order_Management.DAO
{
    internal interface IOrderManagementRepository
    {
        bool createUser(User user);
        bool createProduct(User user, Products product);

        List<Products> getAllProducts();
        List<Tuple<Orders, Products>> getOrderByUser(User user);

        bool cancelOrder(int userId, int orderId);

        bool createOrder(User user, List<Products> product);

    }
}