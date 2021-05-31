using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Address_book_Ao
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("*********WELCOME TO THE ADRESSBOOK ADO*********");

            AddressBookManagement addressBookManagement = new AddressBookManagement();

            addressBookManagement.DataBaseConnection(); //UC1
            Console.ReadLine();
           
        }
    }
}
