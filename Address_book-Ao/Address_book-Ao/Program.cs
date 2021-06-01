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
            // addressBookManagement.GetAllContact();//UC2
            //addressBookManagement.AddDataToTable();

            //AddNewContactDetails();//UC3
            // Console.WriteLine(addressBookManagement.EditContactUsingName("445566", "Naina", "Wadal") ? "Update Record successfully\n" : "Update failed"); //UC4
            //Console.WriteLine(addressBookManagement.DeleteContactUsingName("Naina", "Wadal") ? "Delete Record successfully\n" : "Delete failed"); //UC5
           // addressBookManagement.RetrieveContactFromCityOrStateName(); //UC6
            addressBookManagement.CountByCityOrState(); //UC7
            Console.ReadLine();

        }
        //UC3:- Ability to insert new Contacts to Address Book 
        public static void AddNewContactDetails()
        {
            AddressBookManagement repository = new AddressBookManagement();
            AddressBookModel model = new AddressBookModel();
            model.FirstName = "Naina";
            model.LastName = "Wadal";
            model.Address = "Warje";
            model.City = "Pune";
            model.State = "Maharashtra";
            model.Zip =" 445566";
            model.PhoneNumber = "9090909090";
            model.EmailId = "naina@gmail.com";
            model.AddressBookType = "Friend";
            model.AddressBookName = "Naina";

            Console.WriteLine(repository.AddDataToTable(model) ? "Record inserted successfully\n" : "Record inserted Failed");
        }
    }
}
    