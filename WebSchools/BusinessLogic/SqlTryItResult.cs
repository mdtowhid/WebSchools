using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using WebSchools.Models.Sql;

namespace WebSchools.BusinessLogic
{
    public class SqlTryItResult : Db
    {
        public bool IsTableNameExist(string userQuery)
        {
            string query = "SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES";
            Command = new SqlCommand(query, Connection);
            Reader = Command.ExecuteReader();
            string tableName = "";
            bool isExist = false;
            while (Reader.Read())
            {
                tableName = Reader["TABLE_NAME"].ToString();
                if (Regex.IsMatch(userQuery, string.Format(@"\b{0}\b", Regex.Escape(tableName))))
                {
                    isExist = true;
                    Reader.Close();
                    return isExist;
                }
            }

            return false;
        }
        public List<DemoCustomer> GetCustomersDataByUserQuery(string userQuery)
        {
            Connection.Open();
            List<DemoCustomer> demoCustomers = new List<DemoCustomer>();

            try
            {
                if (IsTableNameExist(userQuery))
                {
                    Command = new SqlCommand(userQuery, Connection);
                    Reader = Command.ExecuteReader();

                    while (Reader.Read())
                    {
                        DemoCustomer demoCustomer = new DemoCustomer();
                        demoCustomer.Id = Convert.ToInt32(Reader["Id"]);
                        demoCustomer.CustomerName = Reader["CustomerName"].ToString();
                        demoCustomer.ContactNumber = Reader["ContactNumber"].ToString();
                        demoCustomer.City = Reader["City"].ToString();
                        demoCustomer.Email = Reader["Email"].ToString();
                        demoCustomers.Add(demoCustomer);
                    }
                    Reader.Close();
                    Connection.Close();
                    return demoCustomers;
                }
                else
                {
                    Reader.Close();
                    Connection.Close();
                    return demoCustomers = null;
                }
            }
            catch (Exception)
            {
                Reader.Close();
                Connection.Close();
                return demoCustomers = null;
            }
            
        }
    }
}