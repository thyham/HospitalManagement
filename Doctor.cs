using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

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
            bool running = true;
            while (running)
            {
                Console.WriteLine("Doctor Menu: " + doctorID);
                Console.WriteLine("Please choose an option: ");
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
                    ListAppointments();
                }

                else if (choice == "4")
                {
                    Console.Clear();
                    CheckPatient();
                    
                }

                else if (choice == "5")
                {
                    Console.Clear();
                    ListPatientAppointment();

                }

                else if (choice == "6")
                {
                    Console.Clear();
                    running = false;
                    break;
                }

                else if (choice == "7")
                {
                    Console.Clear();
                    Environment.Exit(0);
                }

                else
                {
                    Console.Clear();
                    Console.WriteLine("Enter a valid option: ");
                    continue;
                }
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
                int patID = int.Parse(contents[1]);
                string description = contents[2];
                Console.WriteLine("Patient details:");
                Console.WriteLine($"Doctor ID: {docID}");
                Console.WriteLine($"Patient ID: {patID}");
                Console.WriteLine($"Description: {description}");
                //Book appointment with patientID or patient name?
            }
        }

        public void CheckPatient()
        {
            while (true)
            {
                Console.WriteLine("Enter a patient ID: ");
                string raw = Console.ReadLine();

                if (raw.Equals("e"))
                    break;

                // ✅ Validate integer input
                if (!int.TryParse(raw, out int inputId))
                {
                    Console.WriteLine("Invalid input. Please enter a number.");
                    continue;
                }

                // ✅ Search patients
                bool found = false;

                foreach (var line in File.ReadAllLines("emp.txt"))
                {
                    string[] contents = line.Split(',');
                    string role = contents[0];

                    if (role.Equals("patient"))
                    {
                        int patid = int.Parse(contents[1]);
                        string password = contents[2];
                        int? doctorID = contents.Length > 3 ? int.Parse(contents[3]) : null;

                        if (patid == inputId)
                        {
                            Console.Clear();
                            Console.WriteLine("Patient details:");
                            Console.WriteLine($"ID: {patid}");
                            Console.WriteLine($"Password: {password}");
                            Console.WriteLine($"Doctor ID: {(doctorID.HasValue ? doctorID.Value.ToString() : "No doctor assigned")}");

                            found = true;
                            break;
                        }
                    }
                }

                if (!found)
                {
                    Console.Clear();
                    Console.WriteLine($"No patient found with ID {inputId}. Please try again.");
                }
                else
                {
                    break;
                }
            }
        }

        public void ListPatientAppointment()
        {
            while (true)
            {
                Console.WriteLine("Enter a patient ID: ");
                string raw = Console.ReadLine();

                if (raw.Equals("e"))
                    break;

                // ✅ Validate integer input
                if (!int.TryParse(raw, out int inputId))
                {
                    Console.WriteLine("Invalid input. Please enter a number.");
                    continue;
                }

                // ✅ Search patients
                bool found = false;

                foreach (var line in File.ReadAllLines("appointments.txt"))
                {
                    string[] contents = line.Split(',');
                    int docID = int.Parse(contents[0]);
                    int patID = int.Parse(contents[1]);

                    if (patID.Equals(inputId) && docID == doctorID)
                    {
                        string description = contents[2];
                        Console.WriteLine("Patient details:");
                        Console.WriteLine($"Doctor ID: {docID}");
                        Console.WriteLine($"Patient ID: {patID}");
                        Console.WriteLine($"Description: {description}");

                        found = true;
                    }
                }

                if (!found)
                {
                    Console.Clear();
                    Console.WriteLine($"No patient found with ID {inputId}. Please try again.");
                }
                else
                {
                    break;
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
