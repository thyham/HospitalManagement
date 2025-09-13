using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagement
{
    public abstract class User
    {
        public string Role { get; set; }
        public int ID { get; set; }
        public string Password { get; set; }
        public string? FName { get; set; }
        public string? LName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? StreetNo { get; set; }
        public string? Street { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }

        //, string fname, string lname,  string email, int phoneNumber, int streetno, string street, string city, string state
        protected User(int id, string password, string? fname = null, string? lname = null, string? email = null, string? phoneNumber = null, string? streetno = null, string? street = null, string? city = null, string? state = null)
        {
            ID = id;
            Password = password;
            FName = fname;
            LName = lname;
            Email = email;
            PhoneNumber = phoneNumber;
            StreetNo = streetno;
            Street = street;
            City = city;
            State = state;
        }

        protected void WaitForKey()
        {
            Console.WriteLine();
            Console.WriteLine("Press any key to return to the menu...");
            Console.ReadKey(true); 
        }

        public abstract void ShowMenu();
    }
}
