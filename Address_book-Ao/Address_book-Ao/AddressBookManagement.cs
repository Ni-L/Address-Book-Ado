using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Address_book_Ao
{
    /// <summary>
    /// For Maneging data of address book
    /// connection Establishment
    /// </summary>
    class AddressBookManagement
    {
        //Connection string for the connection to the database
        public static string connectionString = @"Data Source=DESKTOP-DL043RM;Initial Catalog=AddressBookDataBase;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        // SqlConnection=Open connection to the sql server database
        //Initialised new instance of sqlConnection and pass connection string in that connection
        SqlConnection connection = new SqlConnection(connectionString);

        //Adding method for databse connection 
        public void DataBaseConnection()
        {
            try
            {
                //DtaTime is a instant of time
                //It describes date and time of the day
                //create object DateTime class 
                //Now=local date and time
                DateTime now = DateTime.Now;
                connection.Open(); // open connection
                using (connection)  //using SqlConnection
                {
                    Console.WriteLine($"Connection is created Successful {now}"); //print msg

                }
                connection.Close(); //close connection
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
    }
}