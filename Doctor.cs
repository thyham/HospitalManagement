using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace HospitalManagement
{
    class Doctor : User
    {
        private int doctorID;
        private string doctorPW;
        private string role;
        int patientID;

        public Doctor(int id, string password) : base(id, password)
        {
            doctorID = id;
            doctorPW = password;
            role = "doctor";
        }

        public override void ShowMenu()
        {
            Console.WriteLine("Doctor Menu: " + doctorID);
            Console.WriteLine("Please choose an option: ");
            bool running = true;
            while (running)
            {
                string choice = Console.ReadLine();

                if (choice == "1") {
                    Console.Clear();
                    ListDoctorDetails();
                }

                else if (choice == "2")
                {
                    Console.Clear();
                    ListPatients();
                }

                else if (choice == "3")
                {
                    var ap = new Appointment(doctorID, 15, "cc");
                    Console.WriteLine(ap);
                    File.AppendAllLines("appointments.txt", new[] { ap.ToString()});
                }

                else if (choice == "4")
                {
                    Console.Clear();
                    string inputID = Console.ReadLine();
                    int id;
                    bool isInteger = int.TryParse(inputID, out id);

                    if (isInteger)
                    {
                        CheckPatient(id);
                    }
                    else
                    {
                        Console.WriteLine("Enter a valid ID");
                    }

                }

                else if (choice == "Exit")
                {
                    break;
                }
                continue;
            }
        }


        public void ListDoctorDetails()
        {
            Console.WriteLine(doctorID + doctorPW);
        }

        public void ListPatients()
        {
            foreach (var line in File.ReadAllLines("emp.txt"))
            {
                string[] contents = line.Split(',');
                string role = contents[0];

                if (role == "patient")
                {
                    int id = int.Parse(contents[1]);
                    string password = contents[2];
                    int? doctorID = contents.Length > 3 ? int.Parse(contents[3]) : null;

                    if (doctorID == this.doctorID)
                    {
                        Console.WriteLine(id);
                    }

                }
            }
        }

        public void CheckPatient(int patID)
        {
            foreach (var line in File.ReadAllLines("emp.txt"))
            {
                string[] contents = line.Split(',');
                string role = contents[0];

                if (role == "patient")
                {
                    int id = int.Parse(contents[1]);
                    string password = contents[2];
                    int? doctorID = contents.Length > 3 ? int.Parse(contents[3]) : null;

                    if (id == patID && doctorID == this.doctorID)
                    {
                        Console.WriteLine(id);
                    }

                }
            }
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
