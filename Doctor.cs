using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagement
{
    class Doctor : User
    {
        public Doctor (int id, string password)
            : base (id, password) { }
        public override void ShowMenu()
        {
            bool running = true;
            while (running)
            {
                Console.WriteLine("Doctor Menu");
                string choice = Console.ReadLine();
                if (choice == "1")
                {
                    Console.WriteLine("Choice 1");
                }
                break;
            }
        }
    }
}
