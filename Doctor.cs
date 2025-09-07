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
        private int doctorID;
        private string doctorPW;
        public Doctor(int id, string password)
            : base(id, password) {
            doctorID = id;
            doctorPW = password;
        }
        public override void ShowMenu()
        {
            bool running = true;
            while (running)
            {
                Console.WriteLine("Doctor Menu");
                string choice = Console.ReadLine();
                if (choice == "1")
                {
                    foreach (var line in File.ReadAllLines("patients.txt"))
                    {
                        string[] contents = line.Split(',');
                        int patientID = int.Parse(contents[0]);
                        string name = contents[1];
                        string description = contents[2];
                        Console.WriteLine(patientID + name + description);
                    }
                }
                break;
            }
        }

        public void LoadDoctors(string line)
        {
            // Split the comma seperated string into fields 
            string[] contents = line.Split(',');

            // Assign values to respective properties/ members
            int id = int.Parse(contents[0]);
            string password = contents[1];
            string role = contents[2];
        }

        public int DoctorID
        {
            get { return doctorID; }
        }

        public string DoctorPW
        {
            get { return doctorPW; }
        }
    }
}
