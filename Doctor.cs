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
        public string Role { get; set; }
        public int ID { get; set; }
        public string Password { get; set; }
        public string FName { get; set; }
        public string LName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public int StreetNo { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public Doctor(int id, string password, string fname, string lname, string email, string phoneNumber, int streetno, string street, string city, string state, int? doctorID = null)
            : base(id, password, fname, lname, email, phoneNumber, streetno, street, city, state)
        {
            Role = "doctor";
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

        public override void ShowMenu(List<User> allUsers)
        {
            bool running = true;
            while (running)
            {
                Console.Clear();
                Header("Doctor Menu");
                MenuOptions(); //fix menu options
                string choice = Console.ReadLine();

                if (choice == "1")
                {
                    Console.Clear();
                    ListDoctorDetails();
                    WaitForKey();
                }

                else if (choice == "2")
                {
                    Console.Clear();
                    ListPatients();
                    WaitForKey();
                }

                else if (choice == "3")
                {
                    Console.Clear();
                    ListAppointments();
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
                    ListPatientAppointment();
                    WaitForKey();

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
            Header("My Details");
            Console.WriteLine("Patient ID: " + ID);
            Console.WriteLine("Full Name: " + FName + " " + LName);
            Console.WriteLine("Address: " + StreetNo + " " + Street + " " + City + " " + State);
            Console.WriteLine("Email: " + Email);
            Console.WriteLine("Phone: " + PhoneNumber);
        }

        public void ListPatients()
        {
            Header("My Patients");
            Console.WriteLine($"{"Doctor",-20} | {"Patient",-20} | {"Email",-25} | {"Phone",-15} | {"Address",-10}");
            Console.WriteLine(new string('-', 120));
            foreach (var line in File.ReadAllLines("emp.txt"))
            {
                string[] contents = line.Split(',');
                string role = contents[0];
                int id = int.Parse(contents[1]);
                string password = contents[2];
                string fname = contents[3];
                string lname = contents[4];
                string email = contents[5];
                string phone = contents[6];
                string streetno = contents[7];
                string street = contents[8];
                string city = contents[9];
                string state = contents[10];
                int? docID = contents.Length > 11 ? int.Parse(contents[11]) : (int?)null;

                    if (role == "patient" && docID == ID)
                    {
                    string docFullName = FName + " " + LName;
                    string patFullName = $"{fname} {lname}";
                    string fullAddress = $"{streetno} {street}, {city}, {state}";
                    Console.WriteLine($"{docFullName,-20} | {patFullName,-20} | {email,-25} | {phone,-15} | {fullAddress,-30}");
                    }
            }
     }

        public void ListAppointments()
        {
            Header("My Patients");
            Console.WriteLine($"{"Doctor",-25} | {"Patient",-25} | {"Description",-30}");
            Console.WriteLine(new string('-', 100));
            foreach (var line in File.ReadAllLines("appointments.txt"))
            {
                string[] contents = line.Split(',');
                int docID = int.Parse(contents[0]);
                int patID = int.Parse(contents[1]);
                string description = contents[2];
                string fname = GetDoctorFirstName(docID);
                string lname = GetDoctorLastName(docID);
                string docFullName = $"{fname} {lname}";
                string patFullName = $"{FName} {LName}";

                Console.WriteLine($"{docFullName,-25} | {patFullName,-25} | {description,-30}");
            }
        }

        public string GetDoctorFirstName(int id)
        {
            foreach (var line in File.ReadAllLines("emp.txt"))
            {
                string[] contents = line.Split(',');
                int docID = int.Parse(contents[1]);
                if (docID == id)
                {
                    string name = contents[3];
                    return name;
                }
            }
            return "";
        }

        public string GetDoctorLastName(int id)
        {
            foreach (var line in File.ReadAllLines("emp.txt"))
            {
                string[] contents = line.Split(',');
                int docID = int.Parse(contents[1]);
                if (docID == id)
                {
                    string lname = contents[4];
                    return lname;
                }
            }
            return "";
        }


        public void CheckPatient()
        {
            while (true)
            {
                Console.Clear();
                Header("Check Patient");
                Console.Write("Enter a patient ID: ");
                string raw = Console.ReadLine();

                if (raw.Equals("e"))
                    break;

                if (!int.TryParse(raw, out int inputId))
                {
                    Console.Clear();
                    Header("Check Patient Details");
                    Console.WriteLine("Invalid input. Please enter a number.");
                    continue;
                }

                bool found = false;
                Console.WriteLine($"{"Doctor",-20} | {"Patient",-20} | {"Email",-25} | {"Phone",-15} | {"Address",-10}");
                Console.WriteLine(new string('-', 120));
                foreach (var line in File.ReadAllLines("emp.txt"))
                {
                    string[] contents = line.Split(',');
                    string role = contents[0];
                    int id = int.Parse(contents[1]);
                    string password = contents[2];
                    string fname = contents[3];
                    string lname = contents[4];
                    string email = contents[5];
                    string phone = contents[6];
                    string streetno = contents[7];
                    string street = contents[8];
                    string city = contents[9];
                    string state = contents[10];
                    int? patID = contents.Length > 11 ? int.Parse(contents[11]) : (int?)null;

                    if (role == "patient" && id == inputId)
                    {
                        found = true;
                        string docFullName = FName + " " + LName;
                        string patFullName = $"{fname} {lname}";
                        string fullAddress = $"{streetno} {street}, {city}, {state}";
                        Console.WriteLine($"{docFullName,-20} | {patFullName,-20} | {email,-25} | {phone,-15} | {fullAddress,-10}");
                    }
                }

                if (!found)
                {
                    Header("Check Patient Details");
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
                Console.Clear();
                bool found = false;
                Header("Find Appointment");
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
                Console.WriteLine($"{"Doctor",-25} | {"Patient",-25} | {"Description",-30}");
                Console.WriteLine(new string('-', 100));
                foreach (var line in File.ReadAllLines("appointments.txt"))
                {
                    string[] contents = line.Split(',');
                    int docID = int.Parse(contents[0]);
                    int patID = int.Parse(contents[1]);

                    if (patID.Equals(inputId) && docID == ID)
                    {
                        found = true;
                        string description = contents[2];
                        string fname = GetDoctorFirstName(docID);
                        string lname = GetDoctorLastName(docID);
                        string docFullName = $"{fname} {lname}";
                        string patFullName = $"{FName} {LName}";

                        Console.WriteLine($"{docFullName,-25} | {patFullName,-25} | {description,-30}");
                        break;
                    }
                }

                if (!found)
                {
                    Console.WriteLine($"\nNo patient found with ID {inputId}. Please try again.");
                }
                else
                {
                    break;
                }
            }
        }
        public static void Header(string type)
        {
            Console.WriteLine("Timothy's Hospital Management System");
            Console.WriteLine("------------------------------------");
            Console.WriteLine($"            {type}           ");
            Console.WriteLine("\n");
        }

        public static void MenuOptions()
        {
            Console.WriteLine("Please choose an option: ");
            Console.WriteLine("1. List patient details");
            Console.WriteLine("2. List my doctor details");
            Console.WriteLine("3. List all appointments");
            Console.WriteLine("4. Book appointment");
            Console.WriteLine("5. Exist to login");
            Console.WriteLine("6. Exit system \n");

        }

    }
}
