using Mc_Computer_API.App_Start;
using Mc_Computer_API.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;


namespace Mc_Computer_API.Implementation
{
    public class MasterImplementation
    {
        public static string AutoIDGenarator(string tablename)
        {

            string ID = null;
            string Strsql = "select count(*) from " + tablename;
            MySqlConnection Conn = DBConnection.ConnMcComputers;
            try
            {
                if (Conn.State == ConnectionState.Closed)
                    Conn.Open();
                MySqlCommand cmd = new MySqlCommand(Strsql, Conn);
                int count = Convert.ToInt16(cmd.ExecuteScalar()) + 1;
                if (tablename .Equals("mc_transaction"))
                    ID = "trid" + Convert.ToString(count);
                else if (tablename.Equals("mc_product"))
                    ID = "pid" + Convert.ToString(count);
                return ID;
            }
            catch (MySqlException er)
            {
                Console.WriteLine(er.Message.ToString());
            }
            finally
            {
                if (Conn.State == ConnectionState.Open)
                    Conn.Close();
            }

            return ID;
        }

        public static List<Mc_Products> GetAllProducts()
        {
            List<Mc_Products> Productslist = new List<Mc_Products>();
            
            MySqlConnection Conn = DBConnection.ConnMcComputers;
            String Srtsql = "Select mc_ID,mc_Name,mc_Description,mc_qty,mc_unitprice from mc_product";

            try
            {

                if (Conn.State == ConnectionState.Closed)
                    Conn.Open();
                MySqlCommand Cmd = new MySqlCommand(Srtsql, Conn);
                Cmd.CommandTimeout = 60;
                MySqlDataReader dr = Cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    try
                    {
                        while (dr.Read())
                        {
                            Mc_Products products = new Mc_Products();
                            products.productID = dr.GetString(0);
                            products.productName = dr.GetString(1);
                            products.productDescription = dr.GetString(2);
                            products.productQty = dr.GetInt32(3);
                            products.productUnitPrice = dr.GetFloat(4);
                            Productslist.Add(products);

                        }
                        return Productslist;
                    }
                    catch (MySqlException er)
                    {
                        Console.WriteLine(er.Message.ToString());
                    }
                    finally
                    {
                        dr.Close();
                    }
                    return Productslist;
                }
                else
                    return Productslist;
            }
            catch (MySqlException er)
            {
                Console.WriteLine(er.Message.ToString());
            }
            finally
            {
                if (Conn.State == ConnectionState.Open)
                    Conn.Close();
            }

            return Productslist;
        }

        public static bool AddProducts(Mc_Products pro)
        {

            MySqlConnection Conn = DBConnection.ConnMcComputers;
            string Strsql = "INSERT INTO `mc_product`(`mc_ID`,`mc_Name`,`mc_Description`,`mc_qty`,`mc_unitprice`) VALUES('" + pro.productID + "','" + pro.productName + "','" + pro.productDescription + "','" + pro.productQty + "','" + pro.productUnitPrice + "')";
            try
            {
                if (Conn.State == ConnectionState.Closed)
                    Conn.Open();
                MySqlCommand cmd = new MySqlCommand(Strsql, Conn);
                cmd.CommandTimeout = 60;
                if (cmd.ExecuteNonQuery() == 1)
                {
                    return true;
                }
            }
            catch (MySqlException er)
            {
                Console.WriteLine(er.Message.ToString());
            }
            finally
            {
                if (Conn.State == ConnectionState.Open)
                    Conn.Close();
            }
            return false;
        }

        public static bool AddTransaction(Mc_Bill tr){

            MySqlConnection Conn = DBConnection.ConnMcComputers;
            string Strsql = "INSERT INTO `mc_transaction`(`mc_trID`,`mc_trTotalPrice`) VALUES('"+tr.tractionID+"','"+tr.Totalamount+"')";
            try
            {
                if (Conn.State == ConnectionState.Closed)
                    Conn.Open();
                MySqlCommand cmd = new MySqlCommand(Strsql, Conn);
                cmd.CommandTimeout = 60;
                if (cmd.ExecuteNonQuery() == 1)
                {
                    return true;
                }
            }
            catch (MySqlException er)
            {
                Console.WriteLine(er.Message.ToString());
            }
            finally
            {
                if (Conn.State == ConnectionState.Open)
                    Conn.Close();
            }
            return false;
        }

        public static bool AddProduct_Transaction(Mc_Product_Tranasaction tr)
        {

            MySqlConnection Conn = DBConnection.ConnMcComputers;
            string Strsql = "INSERT INTO `mc_product_transaction`(`mc_trID`,`mc_PID`,`mc_qty`,`mc_unitprice`,`mc_amountprice`) VALUES('','','','','')";
            try
            {
                if (Conn.State == ConnectionState.Closed)
                    Conn.Open();
                MySqlCommand cmd = new MySqlCommand(Strsql, Conn);
                cmd.CommandTimeout = 60;
                if (cmd.ExecuteNonQuery() == 1)
                {
                    return true;
                }
            }
            catch (MySqlException er)
            {
                Console.WriteLine(er.Message.ToString());
            }
            finally
            {
                if (Conn.State == ConnectionState.Open)
                    Conn.Close();
            }
            return false;
        }

        public static bool AddBill(Mc_Bill bill)
        {
            List<Mc_Products> ProductsList = bill.loadProductsList;
            bill.tractionID = AutoIDGenarator("mc_transaction");
            foreach (Mc_Products pro in ProductsList)
            {

                if (pro.productID != null && bill.tractionID!=null)
                {
                    if(AddTransaction(bill)){
                        Mc_Product_Tranasaction billproducts = new Mc_Product_Tranasaction();
                        billproducts.trID = bill.tractionID;
                        billproducts.pID = pro.productID;
                        billproducts.Qty = pro.productQty;
                        billproducts.Unitprice = pro.productUnitPrice;
                        billproducts.Amountprice = pro.productUnitPrice;
                        if(AddProduct_Transaction(billproducts)==false)
                        {
                            return false;
                        }
                       
                    }
                    
                }else
                {
                    return false;
                }
            }
            return false;
        }


    }

    
}