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

        public override void ShowMenu(List<User> allUsers)
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
                    //Console.WriteLine("Enter Description: ");
                    Console.Clear();
                    var ap = new Appointment(4, 28, "description");
                    File.AppendAllText("appointments.txt", ap.ToString() + Environment.NewLine);
                    ListAppointments();
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

        public void ListAppointments()
        {
            foreach (var line in File.ReadAllLines("appointments.txt"))
            {
                string[] contents = line.Split(',');
                int docID = int.Parse(contents[0]);
                if (docID == this.doctorID)
                {
                    int patID = int.Parse(contents[1]);
                    string description = contents[2];
                    CheckPatient(patID);
                    Console.WriteLine(docID + patID + description);
                    //Book appointment with patientID or patient name?
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

        public override string ToString()
        {
            return $"{role},{DoctorID},{DoctorPW}";
        }
    }
}
