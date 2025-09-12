using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagement
{
    class Admin : User
    {
        private int adminID;
        private string adminPW;
        private string role;

        public Admin(int id, string password) : base(id, password)
        {
            adminID = id;
            adminPW = password;
            role = "admin";
        }

        public override void ShowMenu(List<User> allUsers)
        {
            bool running = true;
            while (running)
            {
                Console.Clear();
                Console.WriteLine("Admin Menu: " + adminID);
                Console.WriteLine("Please choose an option: ");
                string choice = Console.ReadLine();

                if (choice == "1")
                {
                    Console.Clear();
                    ListDoctors();
                    WaitForKey();

                }

                else if (choice == "2")
                {
                    Console.Clear();
                    CheckDoctor();
                    WaitForKey();
                }

                else if (choice == "3")
                {
                    Console.Clear();
                    ListPatients();
                    WaitForKey();
                }

                else if (choice == "4")
                {
                    Console.Clear();
                    CheckPatient();
                    WaitForKey();
                }

                else if (choice == "5")
                {
                    Console.Clear();
                    AddDoctor();
                    WaitForKey();
                }

                else if (choice == "6")
                {
                    Console.Clear();
                    AddPatient();
                    WaitForKey();
                }

                else if (choice == "7")
                {
                    Console.Clear();
                    running = false;
                    break;
                }

                else if (choice == "8")
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

        public void ListDoctors()
        {
            foreach (var line in File.ReadAllLines("emp.txt"))
            {
                string[] contents = line.Split(',');
                string role = contents[0];

                if (role == "doctor")
                {
                    int id = int.Parse(contents[1]);
                    string password = contents[2];
                    int? doctorID = contents.Length > 3 ? int.Parse(contents[3]) : null;
                    Console.WriteLine(id + password);

                }
            }
        }

        public void CheckDoctor()
        {
            while (true)
            {
                Console.WriteLine("Enter a doctor ID: ");
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

                    if (role.Equals("doctor"))
                    {
                        int docid = int.Parse(contents[1]);
                        string password = contents[2];

                        if (docid == inputId)
                        {
                            Console.Clear();
                            Console.WriteLine("Doctor details:");
                            Console.WriteLine($"ID: {docid}");
                            Console.WriteLine($"Password: {password}");

                            found = true;
                            break;
                        }
                    }
                }

                if (!found)
                {
                    Console.Clear();
                    Console.WriteLine($"No doctor found with ID {inputId}. Please try again.");
                }
                else
                {
                    break;
                }
            }
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
                    Console.WriteLine(id + password);

                }
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

                        if (patid == inputId)
                        {
                            Console.Clear();
                            Console.WriteLine("Patient details:");
                            Console.WriteLine($"ID: {patid}");
                            Console.WriteLine($"Password: {password}");

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

        public void AddDoctor()
        {
            Console.WriteLine("Add a doctor");
            var name = Console.ReadLine();
            Console.WriteLine();
            var newDoctor = new Doctor(5, name);
            File.AppendAllText("emp.txt", newDoctor.ToString() + Environment.NewLine);
        }

        public void AddPatient()
        {
            Console.WriteLine("Add a patient");
            var name = Console.ReadLine();
            Console.WriteLine();
            var newPatient = new Patient(5, name);
            File.AppendAllText("emp.txt", newPatient.ToString() + Environment.NewLine);
        }
    }
}
