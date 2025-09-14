//Timothy Pham 24842813 
//31927/32998 Assignment 1: C# Console Application

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
        public string StreetNo { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public Doctor(int id, string password, string fname, string lname, string email, string phoneNumber, string streetno, string street, string city, string state, int? doctorID = null)
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

        public override void ShowMenu()
        {
            bool running = true;
            while (running)
            {
                Console.Clear();
                Header("Doctor Menu");
                MenuOptions(); 
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
                if (role == "patient")
                {
                    string fname = contents[3];
                    string lname = contents[4];
                    string email = contents[5];
                    string phone = contents[6];
                    string streetno = contents[7];
                    string street = contents[8];
                    string city = contents[9];
                    string state = contents[10];
                    int? docID = contents.Length > 11 ? int.Parse(contents[11]) : (int?)null;

                    if (docID == ID)
                    {
                        string docFullName = FName + " " + LName;
                        string patFullName = $"{fname} {lname}";
                        string fullAddress = $"{streetno} {street}, {city}, {state}";
                        Console.WriteLine($"{docFullName,-20} | {patFullName,-20} | {email,-25} | {phone,-15} | {fullAddress,-30}");
                    }
                }
            }
     }

        public void ListAppointments()
        {
            Header("Appointments");
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

        //Lists a patient based on user input and database search for matching role
        public void CheckPatient()
        {
            Console.Clear();
            Header("Check Patient");
            while (true)
            {
                Console.Write("Enter a patient ID: ");
                string raw = Console.ReadLine();

                if (raw.Equals("e"))
                    break;

                if (!int.TryParse(raw, out int inputId))
                {
                    Console.Clear();
                    Header("Check Patient");
                    Console.WriteLine("Invalid input. Please enter a number.");
                    continue;
                }

                bool found = false;
                Console.Clear();
                Header("Check Patient");
                Console.WriteLine($"\n{"Doctor",-20} | {"Patient",-20} | {"Email",-25} | {"Phone",-15} | {"Address",-10}");
                Console.WriteLine(new string('-', 120));
                foreach (var line in File.ReadAllLines("emp.txt"))
                {
                    string[] contents = line.Split(',');
                    string role = contents[0];
                    int id = int.Parse(contents[1]);
                    string password = contents[2];

                    if (role == "patient")
                    {
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
                    }

                    if (!found)
                {
                    Console.Clear();
                    Header("Check Patient");
                    Console.WriteLine($"No patient found with ID {inputId}. Please try again.");
                }
                else
                {
                    break;
                }
            }
        }

        //Lists appointments based on matching user input and class doctor ID
        public void ListPatientAppointment()
        {
            Console.Clear();
            Header("Appointments With");
            while (true)
            {
                bool found = false;
                Console.Write("Enter a patient ID: ");
                string raw = Console.ReadLine();

                if (raw.Equals("e"))
                    break;

                if (!int.TryParse(raw, out int inputId))
                {
                    Console.Clear();
                    Header("Appointments With");
                    Console.WriteLine("Invalid input. Please enter a number.");
                    continue;
                }

                Console.Clear();
                Header("Appointments With");
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
                        string patFullName = GetPatientFirstName(inputId) + " " + GetPatientLastName(inputId);

                        Console.WriteLine($"{docFullName,-25} | {patFullName,-25} | {description,-30}");
                        break;
                    }
                }

                if (!found)
                {
                    Console.Clear();
                    Header("Appointments With");
                    Console.WriteLine($"No patient found with ID {inputId}. Please try again.");
                }
                else
                {
                    break;
                }
            }
        }

        public string GetPatientFirstName(int id)
        {
            foreach (var line in File.ReadAllLines("emp.txt"))
            {
                string[] contents = line.Split(',');
                string role = contents[0];
                int patID = int.Parse(contents[1]);
                if (role == "patient" && patID == id)
                {
                    string name = contents[3];
                    return name;
                }
            }
            return "";
        }

        public string GetPatientLastName(int id)
        {
            foreach (var line in File.ReadAllLines("emp.txt"))
            {
                string[] contents = line.Split(',');
                string role = contents[0];
                int patID = int.Parse(contents[1]);
                if (role == "patient" && patID == id)
                {
                    string lname = contents[4];
                    return lname;
                }
            }
            return "";
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
            Console.WriteLine("1. List doctor details");
            Console.WriteLine("2. List patients");
            Console.WriteLine("3. List appointments");
            Console.WriteLine("4. Check particular patient");
            Console.WriteLine("5. List appointments with patient");
            Console.WriteLine("6. Logout");
            Console.WriteLine("7. Exit \n");

        }

        public string PrintToString()
        {
            return $"ID: {ID} | {FName} {LName} | {Email} | {PhoneNumber} | {StreetNo} {Street}, {City}, {State}";
        }
        public string ToString()
        {
            return $"doctor,{ID},{Password},{FName},{LName},{Email},{PhoneNumber},{StreetNo},{Street},{City},{State}";
        }

    }
}
