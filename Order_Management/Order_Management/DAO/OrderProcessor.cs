using Order_Management.Exceptions;
using Order_Management.Entity;
using Order_Management.Util;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Services;
using System.Text;
using System.Threading.Tasks;
using System.Data.Common;

namespace Order_Management.DAO
{
    internal class OrderProcessor : IOrderManagementRepository
    {
        static SqlConnection conn;
        SqlCommand cmd;

        //implementation for create product
        public bool createProduct(User user, Products product)
        {
            try
            {
                conn = DBUtil.GetConnection();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.Text;
                conn.Open();
               
                cmd.CommandText = $"insert into product(productname,Price,description,quantityinstock,type) values('{product.ProductName}','{product.Price}','{product.Description}',{product.quantityInStock},'{product.type}')";
                cmd.ExecuteNonQuery();
                cmd.CommandText = $"select SCOPE_IDENTITY()";
                SqlDataReader reader = cmd.ExecuteReader();
                reader.Read();
                Console.WriteLine($"\n\nYour have been allocated a Product id Kindly note it down\n\n\nproduct id={reader[0]}\n\n");
                return true;
               
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            finally
            {
                conn.Close();
            }
        }

        //implementation for create user
        public bool createUser(User user)
        {
            try
            {
                using (conn = DBUtil.GetConnection())
                {
                    cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandType = System.Data.CommandType.Text;
                    conn.Open();
                    cmd.CommandText = $"insert into Users(username,password,role) values('{user.UserName}','{user.Password}','{user.Role}')";
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = $"select SCOPE_IDENTITY()";
                    SqlDataReader reader = cmd.ExecuteReader();
                    reader.Read();
                    Console.WriteLine($"\n\nYou have been allocated a ID Kindly note it down\n\n\nCustomerid={reader[0]}\n\n");
                    return true;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
            finally
            {
                conn.Close();
            }
        }

        


        
        //implementation for get all products
        public List<Products> getAllProducts()
        {
            try
            {
                List<Products> productsincartlist = new List<Products>();
                conn = DBUtil.GetConnection();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = $"select productId,productName,Price,description,type from product";
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        productsincartlist.Add(new Products()
                        {
                            product_id = (int)reader[0],
                            ProductName = reader[1].ToString(),
                            Price = (decimal)reader[2],
                            Description = (string)reader[3].ToString(),
                            type = reader[4].ToString()
                        });
                    }
                    return productsincartlist;
                }
                else
                {
                    throw new Exception("No product Exist");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
            finally
            {
                conn.Close();
            }
        }

        //implementation for get order by user
        public List<Tuple<Orders, Products>> getOrderByUser(User user)
        {
            try
            {
                List<Tuple<Orders, Products>> customerOrders = new List<Tuple<Orders, Products>>();
                conn = DBUtil.GetConnection();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = $"select o.orderId,p.productid,p.productName,p.description,p.type,o.date from orders o join product p  on o.productid=p.productid where o.userId={user.UserId}";
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Products products = new Products() { product_id = (int)reader[1], ProductName = reader[2].ToString(), Description = reader[3].ToString(), type = reader[4].ToString() };
                        Orders orderdetails = new Orders() { Orderid = (int)reader[0], Orderdate = (DateTime)reader[5] };
                        customerOrders.Add(Tuple.Create(orderdetails, products));
                    }
                    return customerOrders;
                }
                else
                {
                    throw new OrderDoesNotExistException();
                }
            }
            catch (OrderDoesNotExistException e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
            finally
            {
                conn.Close();
            }
        }

        //implementation for create order
        public bool createOrder(User user, List<Products> product)
        {
            try
            {
                conn = DBUtil.GetConnection();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.Text;
                conn.Open();
                int placed = 0;
                foreach (var item in product)
                {

                    Console.WriteLine($"Working on placing your order for product id: {item.product_id}");
                    cmd.CommandText = $"Select type from product where productid={item.product_id}";
                    string type = (string)cmd.ExecuteScalar();
                    if (type == "Electronics")
                    {
                        Electronics elec = new Electronics();
                        Console.WriteLine("Enter Brand");
                        elec.Brand = Console.ReadLine();
                        Console.WriteLine("Enter Brand warranty in years");
                        elec.WarrantyPeriod = int.Parse(Console.ReadLine());
                        cmd.CommandText = $"insert into orders(userid,productid,warranty,brand,date) values ({user.UserId},{item.product_id},'{elec.WarrantyPeriod}','{elec.Brand}',getdate())";
                        int order_id = cmd.ExecuteNonQuery();
                        placed++;
                    }
                    else
                    {
                        Clothings cloth = new Clothings();
                        Console.WriteLine("Enter Colour");
                        cloth.Color = Console.ReadLine();
                        Console.WriteLine("Enter Size");
                        cloth.Size = Console.ReadLine();
                        cmd.CommandText = $"insert into orders(userid,productid,size,color,date) values({user.UserId},{item.product_id},'{cloth.Size}','{cloth.Color}',getdate())";
                        int order_id = cmd.ExecuteNonQuery();
                        placed++;
                    }
                    Console.WriteLine($"Order Placed for product id:{item.product_id}");


                }

                if (placed > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }


            }

            catch (ProductNotFoundException p)
            {
                Console.WriteLine(p.Message);
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error placing order: {ex.Message}");
                return false;
            }
        }


        //implementation for cancel order
        public bool cancelOrder(int userId, int orderId)
        {
            try
            {
                conn = DBUtil.GetConnection();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.Text;
                conn.Open();
                cmd.CommandText = $"Delete from Orders where Userid={userId} and orderid={orderId}";
                int a = cmd.ExecuteNonQuery();
                if (a > 0)
                {
                    return true;
                }
                else
                {
                    throw new OrderDoesNotExistException();
                }

            }
            catch (UserNotFoundException un)
            {
                Console.WriteLine(un.Message);
                return false;
            }
            catch (OrderDoesNotExistException on)
            {
                Console.WriteLine(on.Message);
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            finally
            {
                conn.Close();
            }
        }


        public bool ValidateUserToCancelOrder(int userid)
        {
            conn = DBUtil.GetConnection();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = System.Data.CommandType.Text;
            conn.Open();
            cmd.CommandText = $"SELECT CASE WHEN EXISTS (SELECT 1 FROM users WHERE userid = {userid}) THEN 1 ELSE 0 END";
            int customerexistance = (int)cmd.ExecuteScalar();
            if (customerexistance == 1)
            {
                conn.Close();
                return true;
            }
            else
            {
                conn.Close();
                throw new UserNotFoundException();
            }
        }


        public bool ValidateUserToCreateOrder(User user)
        {
            conn = DBUtil.GetConnection();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = System.Data.CommandType.Text;
            conn.Open();
            cmd.CommandText = $"SELECT CASE WHEN EXISTS (SELECT 1 FROM users WHERE userid = {user.UserId}) THEN 1 ELSE 0 END";
            int customerexistance = (int)cmd.ExecuteScalar();
            if (customerexistance == 1)
            {
                cmd.CommandText = $"SELECT CASE WHEN EXISTS (SELECT 1 FROM users WHERE userid = {user.UserId} and role like 'User') THEN 1 ELSE 0 END";
                int customerexistance1 = (int)cmd.ExecuteScalar();
                if (customerexistance1 == 1)
                {
                    conn.Close();
                    return true;
                }
                else
                {
                    conn.Close();
                    throw new Exception("Entered user id belongs to Admin, Enter Customer user ID");
                }
                
            }
            else
            {
                conn.Close();
                throw new UserNotFoundException();
            }
        }


        public bool ValidateUsererExistAndIsAdmin(User user)
        {
            conn = DBUtil.GetConnection();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = System.Data.CommandType.Text;
            conn.Open();
            cmd.CommandText = $"SELECT CASE WHEN EXISTS (SELECT 1 FROM users WHERE userid = {user.UserId} and role like 'Admin') THEN 1 ELSE 0 END";
            int customerexistance = (int)cmd.ExecuteScalar();
            if (customerexistance == 1)
            {
                conn.Close();
                return true;
            }
            else
            {
                conn.Close();
                throw new Exception("User is not admin");
                
            }
        }

        public bool ValidateProductExistance(int product_id)
        {
            conn = DBUtil.GetConnection();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = System.Data.CommandType.Text;
            conn.Open();
            cmd.CommandText = $"SELECT CASE WHEN EXISTS (SELECT 1 FROM Product WHERE productId = {product_id}) THEN 1 ELSE 0 END";
            int customerexistance = (int)cmd.ExecuteScalar();
            if (customerexistance == 1)
            {
                conn.Close();
                return true;
            }
            else
            {
                conn.Close();
                throw new ProductNotFoundException();
                
            }
        }


    }
}