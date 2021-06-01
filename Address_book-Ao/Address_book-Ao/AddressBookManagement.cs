using System;
using System.Collections.Generic;
using System.Data;
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
        //UC2 Getting data
        public void GetAllContact()
        {
            //Creating object of AddressBookModel 
            AddressBookModel model = new AddressBookModel();
            try
            {
                using (connection)//Calling Connection
                {
                    // Query to get all the data from the table
                    string query = @"select * from dbo.AddressBookProcedure";

                    // Impementing the command on the connection fetched database table
                    //SqlCommands represents Transactquery or stored procedure to execute against databse
                    SqlCommand command = new SqlCommand(query, connection);

                    connection.Open();  //Open the connection.
                    // executing the sql data reader to fetch the records
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {

                        while (reader.Read())  // Mapping the data to the employee model class object
                        {
                            model.FirstName = reader.GetString(0);
                            model.LastName = reader.GetString(1);
                            model.Address = reader.GetString(2);
                            model.City = reader.GetString(3);
                            model.State = reader.GetString(4);
                            model.Zip = reader.GetString(5);
                            model.PhoneNumber = reader.GetString(6);
                            model.EmailId = reader.GetString(7);
                            model.AddressBookType = reader.GetString(8);
                            model.AddressBookName = reader.GetString(9);
                            Console.WriteLine("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9}", model.FirstName, model.LastName,
                            model.Address, model.City, model.State, model.Zip, model.PhoneNumber, model.EmailId, model.AddressBookType, model.AddressBookName);
                            Console.WriteLine("\n");
                        }
                    }
                    else
                    {
                        Console.WriteLine("There is No records in Address Book System Table");
                    }
                    reader.Close();
                }
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            //The finally block will execute when the try/catch block leaves the execution,
            //no matter what condition cause it.
            finally
            {
                //Closing connection
                connection.Close();
            }
        }
        public bool AddDataToTable(AddressBookModel model)
        {
            try
            {
                using (connection) // Using the connection established
                {
                    // // Implementing the stored procedure
                    SqlCommand command = new SqlCommand("dbo.AddressBookProcedure", connection);
                    //CommandType= how a command string is interpreted
                    command.CommandType = CommandType.StoredProcedure;
                    //Parameter is default empty connection
                    //Parameter is transact query or stored procedure
                    //Adds the values at the end of the sqlparameter to the sqlcollection
                    command.Parameters.AddWithValue("@FirstName", model.FirstName);
                    command.Parameters.AddWithValue("@LastName", model.LastName);
                    command.Parameters.AddWithValue("@Address", model.Address);
                    command.Parameters.AddWithValue("@City", model.City);
                    command.Parameters.AddWithValue("@State", model.State);
                    command.Parameters.AddWithValue("@Zip", model.Zip);
                    command.Parameters.AddWithValue("@PhoneNumber", model.PhoneNumber);
                    command.Parameters.AddWithValue("@EmailID", model.EmailId);
                    command.Parameters.AddWithValue("@addressBookType", model.AddressBookType);
                    command.Parameters.AddWithValue("@addressBookName", model.AddressBookName);

                    connection.Open();
                    var result = command.ExecuteNonQuery();
                    connection.Close();

                    if (result != 0)  //Return the result of the transaction 
                    {
                        return true;
                    }
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }
        // UC4:- Ability to edit existing contact person using their name
        public bool EditContactUsingName(string Zip, string FirstName, string LastName)
        {
            try
            {
                using (connection)
                {
                    connection.Open();
                    //Query for updating 
                    string query = @"update dbo.AddressBookSystem set Zip = @parameter1
                    where FirstName = @parameter2 and LastName = @parameter3";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@parameter1", Zip);
                    command.Parameters.AddWithValue("@parameter2", FirstName);
                    command.Parameters.AddWithValue("@parameter3", LastName);
                    //ExecuteNonQuery row affected
                    var result = command.ExecuteNonQuery();
                    connection.Close();//Close connection
                    if (result > 0)
                    {
                        return true;
                    }
                    return false;
                }
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }
        //UC5-DeleteContactUsingName
        public bool DeleteContactUsingName(string FirstName, string LastName)
        {
            try
            {
                using (connection)
                {
                    connection.Open();
                    string query = "delete from dbo.AddressBookSystem where FirstName = @parameter1 and LastName =@parameter2";
                    // Binding the parameter to the formal parameters
                    SqlCommand command = new SqlCommand(query, connection);

                    command.Parameters.AddWithValue("@parameter1", FirstName);
                    command.Parameters.AddWithValue("@parameter2", LastName);
                    // Storing the result of the executed query
                    var result = command.ExecuteNonQuery(); 
                    connection.Close();
                    if (result > 0)
                    {
                        return true;
                    }
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                //Connection have to be close
                connection.Close();
            }
        }
        public void RetrieveContactFromCityOrStateName()
        {
            Console.Write("Enter the Name of City:- ");
            string city = Console.ReadLine();
            Console.Write("Enter the Name of State:- ");
            string state = Console.ReadLine();
            //Create Object of AddressBookModel
            AddressBookModel model = new AddressBookModel();
            try
            {
                using (connection)
                {
                    // Query to get all the data from the table
                    string query = $@"select * from dbo.AddressBookProcedure where State='{state}' or City='{city}'";
                    /// Impementing the command on the connection fetched database table
                    SqlCommand command = new SqlCommand(query, connection);
                    //Opening the connection.
                    connection.Open();
                    // executing the sql data reader to fetch the records
                    //SQLDataReader=Provide the way of reading 
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        // Mapping the data to the employee model class object
                        while (reader.Read()) 
                        {
                            model.FirstName = reader.GetString(0);
                            model.LastName = reader.GetString(1);
                            model.Address = reader.GetString(2);
                            model.City = reader.GetString(3);
                            model.State = reader.GetString(4);
                            model.Zip = reader.GetString(5);
                            model.PhoneNumber = reader.GetString(6);
                            model.EmailId = reader.GetString(7);
                            model.AddressBookType = reader.GetString(8);
                            model.AddressBookName = reader.GetString(9);
                            Console.WriteLine("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9}", model.FirstName, model.LastName,
                            model.Address, model.City, model.State, model.Zip, model.PhoneNumber, model.EmailId, model.AddressBookType, model.AddressBookName);
                            Console.WriteLine("\n");
                        }
                    }
                    else
                    {
                        Console.WriteLine("No data found");
                    }
                    reader.Close();
                }
            }
            /// Catching the null record exception
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }
    }
}
