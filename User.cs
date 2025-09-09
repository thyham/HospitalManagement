using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagement
{
    abstract class User
    {
        public string Role { get; set; }
        public int ID { get; set; }
        public string Password { get; set; }
        //public string FName { get; set; }
        //public string LName { get; set; }
        //public string Email { get; set; }
        //public int PhoneNumber { get; set; }
        //public int StreetNo { get; set; }
        //public string Street { get; set; }
        //public string City { get; set; }
        //public string State { get; set; }

        //, string fname, string lname,  string email, int phoneNumber, int streetno, string street, string city, string state
        protected User(int id, string password)
        {
            ID = id;
            Password = password;
            //FName = fname;
            //LName = lname;
            //Email = email;
            //PhoneNumber = phoneNumber;
            //StreetNo = streetno;
            //Street = street;
            //City = city;
            //State = state;
        }

        public abstract void ShowMenu();
    }
}
