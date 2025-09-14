using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace HospitalManagement
{
    class Patient : User
    {
        public string Role { get; set; }
        public int ID { get; set; }
        public string Password { get; set; }
        public string FName { get; set; }
        public string LName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string StreetNo { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        private int? DoctorID { get; set; }
        public Patient(int id, string password, string fname, string lname, string email, string phoneNumber, string streetno, string street, string city, string state, int? doctorID = null)
            : base(id, password, fname, lname, email, phoneNumber, streetno, street, city, state)
        {
            Role = "patient";
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
            DoctorID = doctorID;

        }

        public override void ShowMenu()
        {
            bool running = true;
            while (running)
            {
                Console.Clear();
                Header("Patient Menu");
                MenuOptions();
                string choice = Console.ReadLine();

                if (choice == "1")
                {
                    Console.Clear();
                    Header("My Details");
                    ListPatientDetails();
                    WaitForKey();
                }

                else if (choice == "2")
                {
                    Console.Clear();
                    Header("My Doctor");
                    ListDoctor();
                    WaitForKey();
                }

                else if (choice == "3")
                {
                    Console.Clear();
                    Header("My Appointments");
                    ListAppointments();
                    WaitForKey();
                }


                else if (choice == "4")
                {
                    Console.Clear();
                    Header("Book Appointment");
                    BookAppointment();
                    WaitForKey();
                }

                else if (choice == "5")
                {
                    Console.Clear();
                    running = false;
                    break;
                }

                else if (choice == "6")
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
        public void ListPatientDetails()
        {
            Console.WriteLine("Patient ID: " + ID);
            Console.WriteLine("Full Name: " + FName + " " + LName);
            Console.WriteLine("Address: " + StreetNo + " " + Street + " " + City + " " + State);
            Console.WriteLine("Email: " + Email);
            Console.WriteLine("Phone: " + PhoneNumber);
        }

        //Lists a patients' doctor if Doctor ID parameter exists on Patient object
        public void ListDoctor()
        {

            if (DoctorID == null)
            {
                Console.WriteLine("No doctor");
            }

            else
            {
                Console.WriteLine($"{"Name",-20} | {"Email Address",-25} | {"Phone",-15} | {"Address",-30}");
                Console.WriteLine(new string('-', 100));

                foreach (var line in File.ReadAllLines("emp.txt"))
                {
                    string[] contents = line.Split(',');
                    string role = contents[0];
                    int id = int.Parse(contents[1]);
                    string password = contents[2];
                    if (role == "doctor")
                    {
                        string fname = contents[3];
                        string lname = contents[4];
                        string email = contents[5];
                        string phone = contents[6];
                        string streetno = contents[7];
                        string street = contents[8];
                        string city = contents[9];
                        string state = contents[10];

                        if (id == DoctorID)
                        {
                            string fullName = $"{fname} {lname}";
                            string fullAddress = $"{streetno} {street}, {city}, {state}";

                            Console.WriteLine($"{fullName,-20} | {email,-25} | {phone,-15} | {fullAddress,-30}");
                        }
                    }
                }

            }
        }

        public void ListAppointments()
        {
            Console.WriteLine($"{"Doctor",-25} | {"Patient",-25} | {"Description",-30}");
            Console.WriteLine(new string('-', 100));
            foreach (var line in File.ReadAllLines("appointments.txt"))
            {
                string[] contents = line.Split(',');
                int docID = int.Parse(contents[0]);
                int patID = int.Parse(contents[1]);
                if (patID == ID)
                {
                    string description = contents[2];
                    string fname = GetDoctorFirstName(docID);
                    string lname = GetDoctorLastName(docID);
                    string docFullName = $"{fname} {lname}";
                    string patFullName = $"{FName} {LName}";

                    Console.WriteLine($"{docFullName,-25} | {patFullName,-25} | {description,-30}");
                }
            }
        }

        public string GetDoctorFirstName(int id)
        {
            foreach (var line in File.ReadAllLines("emp.txt"))
            {
                string[] contents = line.Split(',');
                string role = contents[0];
                int docID = int.Parse(contents[1]);
                if (role == "doctor" && docID == id)
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
                string role = contents[0];
                int docID = int.Parse(contents[1]);
                if (role == "doctor" && docID == id)
                {
                    string lname = contents[4];
                    return lname;
                }
            }
            return "";
        }


        //Creates an appointment based on user prompt or fetches existing Doctor ID parameter from Patient object
        public void BookAppointment()
        {
            if (DoctorID != null)
            {
                int docID = (int)DoctorID;
                Console.WriteLine("Booking with Dr. " + GetDoctorFirstName(docID) + " " + GetDoctorLastName(docID));
                Console.Write("Description of the appointment: ");
                string? description = Console.ReadLine();
                var ap = new Appointment(docID, ID, description);
                DoctorID = docID;
                File.AppendAllText("appointments.txt", ap.ToString() + Environment.NewLine);
                Console.WriteLine("\n The appointment has been booked successfully.");
            }

            while (DoctorID == null)
            {
                Console.WriteLine("You are not registered with any doctor! Please choose which doctor you would like to register with\n");
                ListDoctors();
                Console.Write("\nPlease enter a doctor ID: ");
                string input = Console.ReadLine();


                if (int.TryParse(input, out int chooseDoctor))
                {
                    if (DoctorExists(chooseDoctor))
                    {
                        Console.Write("Description of the appointment: ");
                        string? description = Console.ReadLine();
                        var ap = new Appointment(chooseDoctor, ID, description);
                        DoctorID = chooseDoctor;
                        File.AppendAllText("appointments.txt", ap.ToString() + Environment.NewLine);
                        UpdatePatientDoctor(ID, chooseDoctor);
                        Console.WriteLine("The appointment has been booked successfully.\n");
                        break;
                    }

                    else
                    {
                        Console.Clear();
                        Header("Book Appointment");
                        Console.WriteLine("Doctor ID does not exist. Try again");
                    }
                }
                else
                {
                    Console.Clear();
                    Header("Book Appointment");
                    Console.WriteLine("Invalid input. Please enter a numeric doctor ID.");
                }
            }
        }

        //Checks if doctor exists to mitigate user error
        public bool DoctorExists(int id)
        {
            foreach (var line in File.ReadAllLines("emp.txt"))
            {
                string[] contents = line.Split(',');
                string role = contents[0];
                int docID = int.Parse(contents[1]);
                if (role == "doctor" && docID == id)
                {
                    return true;
                }
            }
            return false;
        }

        //Implemented to fetch all doctors
        public void ListDoctors()
        {
            foreach (var line in File.ReadAllLines("emp.txt"))
            {
                string[] contents = line.Split(',');
                string role = contents[0];
                int id = int.Parse(contents[1]);
                string password = contents[2];
                if (role == "doctor")
                {
                    string fname = contents[3];
                    string lname = contents[4];
                    string email = contents[5];
                    string phone = contents[6];
                    string streetno = contents[7];
                    string street = contents[8];
                    string city = contents[9];
                    string state = contents[10];
                    Doctor doctor = new Doctor(id, password, fname, lname, email, phone, streetno, street, city, state);
                    string fullName = $"{fname} {lname}";
                    string fullAddress = $"{streetno} {street}, {city}, {state}";

                    Console.WriteLine(doctor.PrintToString());
                }
            }
        }

        //Initalises the int? doctorID parameter in the database, appending the relevant data
        private void UpdatePatientDoctor(int patientId, int doctorId)
        {
            var lines = File.ReadAllLines("emp.txt").ToList();

            //Iterates through database
            for (int i = 0; i < lines.Count; i++)
            {
                string[] parts = lines[i].Split(',');

                //Until matching patient ID is found
                if (parts[0] == "patient" && int.Parse(parts[1]) == patientId)
                {
                    // If already has doctor ID, replace it
                    if (parts.Length > 11)
                    {
                        parts[11] = doctorId.ToString();
                    }
                    else
                    {
                        // Append doctor ID
                        lines[i] = lines[i] + "," + doctorId;
                    }
                    break;
                }
            }

            File.WriteAllLines("emp.txt", lines);
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
            Console.WriteLine("5. Exit to login");
            Console.WriteLine("6. Exit system \n");

        }

        public override string ToString()
        {
            return $"patient,{ID},{Password},{FName},{LName},{Email},{PhoneNumber},{StreetNo},{Street},{City},{State}";
        }
    }
}