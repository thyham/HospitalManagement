//Timothy Pham 24842813 
//31927/32998 Assignment 1: C# Console Application

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

        public override void ShowMenu()
        {
            bool running = true;
            while (running)
            {
                Console.Clear();
                Header("Admin Menu"); 
                MenuOptions();
                Console.Write("Please choose an option: ");
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

        //Lists every doctor in the system
        public void ListDoctors()
        {
            Console.Clear();
            Header("All Doctors");
            Console.WriteLine($"{"Name",-20} | {"Email",-25} | {"Phone",-15} | {"Address",-10}");
            Console.WriteLine(new string('-', 120));
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
                    int? docID = contents.Length > 11 ? int.Parse(contents[11]) : (int?)null;
                    string docFullName = $"{fname} {lname}";
                    string fullAddress = $"{streetno} {street}, {city}, {state}";
                    Console.WriteLine($"{docFullName,-20} | {email,-25} | {phone,-15} | {fullAddress,-10}");
                }
            }
        }

        //Lists a doctor based on user input
        public void CheckDoctor()
        {
            Console.Clear();
            Header("Doctor Details");
            while (true)
            {
                Console.Write("Enter a doctor ID: ");
                string raw = Console.ReadLine();

                if (raw.Equals("e"))
                    break;

                if (!int.TryParse(raw, out int inputId))
                {
                    Console.Clear();
                    Header("Doctor Details");
                    Console.WriteLine("Invalid input. Please enter a number.");
                    continue;
                }

                bool found = false;

                Console.Clear();
                Header("Doctor Details");
                Console.WriteLine($"{"Name",-20} | {"Email",-25} | {"Phone",-15} | {"Address",-10}");
                Console.WriteLine(new string('-', 120));
                foreach (var line in File.ReadAllLines("emp.txt"))
                {
                    string[] contents = line.Split(',');
                    string role = contents[0];
                    int id = int.Parse(contents[1]);
                    string password = contents[2];
                    if (role.Equals("doctor") && id == inputId)
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
                        string docFullName = $"{fname} {lname}";
                        string fullAddress = $"{streetno} {street}, {city}, {state}";
                        Console.WriteLine($"{docFullName,-20} | {email,-25} | {phone,-15} | {fullAddress,-10}");
                        found = true;
                        break;
                    }
                }

                if (!found)
                {
                    Console.Clear();
                    Header("Doctor Details");
                    Console.WriteLine($"No doctor found with ID {inputId}. Please try again.");
                }
                else
                {
                    break;
                }
            }
        }

        //Lists all patients in the system
        public void ListPatients()
        {
            Console.Clear();
            Header("All Patients");
            Console.WriteLine($"{"Name",-20} | {"Email",-25} | {"Phone",-15} | {"Address",-10}");
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
                    string docFullName = $"{fname} {lname}";
                    string fullAddress = $"{streetno} {street}, {city}, {state}";
                    Console.WriteLine($"{docFullName,-20} | {email,-25} | {phone,-15} | {fullAddress,-10}");
                }
            }
        }

        //Returns doctor first name
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

        //Returns doctor last name
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
        //Lists a patient based on user input
        public void CheckPatient()
        {
            Console.Clear();
            Header("Patient Details");
            while (true)
            {
                Console.Write("Enter a Patient ID: ");
                string raw = Console.ReadLine();

                if (raw.Equals("e"))
                    break;

                if (!int.TryParse(raw, out int inputId))
                {
                    Console.Clear();
                    Header("Patient Details");
                    Console.WriteLine("Invalid input. Please enter a number.");
                    continue;
                }

                bool found = false;
                Console.Clear();
                Header("Patient Details");

                Console.WriteLine($"{"Patient",-20} | {"Doctor",-20} | {"Email",-25} | {"Phone",-15} | {"Address",-10}");
                Console.WriteLine(new string('-', 120));
                foreach (var line in File.ReadAllLines("emp.txt"))
                {
                    string[] contents = line.Split(',');
                    string role = contents[0];
                    int id = int.Parse(contents[1]);
                    string password = contents[2];
                    if (role.Equals("patient") && id == inputId)
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
                        string patFullName = $"{fname} {lname}";
                        string fullAddress = $"{streetno} {street}, {city}, {state}";
                        string docFullName = "";
                        if (docID != null)
                        {
                            string docfname = GetDoctorFirstName(docID.Value);
                            string doclname = GetDoctorLastName(docID.Value);
                            docFullName = $"{docfname} {doclname}";
                        }
                        Console.WriteLine($"{patFullName,-20} | {docFullName,-20} | {email,-25} | {phone,-15} | {fullAddress,-30}");

                        found = true;
                        break;

                    }
                }

                if (!found)
                {
                    Console.Clear();
                    Header("Patient Details");
                    Console.WriteLine($"No patient found with ID {inputId}. Please try again.");
                }
                else
                {
                    break;
                }
            }
        }

        //Adds a doctor by creating a doctor object and adding it to the database as a ToString
        public void AddDoctor()
        {
            Console.Clear();
            Header("Add Doctor");
            Console.WriteLine("Registering a new doctor with the Hospital Management System \n");
            Console.WriteLine("Doctor ID: " + GenerateDoctorID());
            Console.Write("First Name: ");
            var fname = Console.ReadLine();

            Console.Write("Last Name: ");
            var lname = Console.ReadLine();

            Console.Write("Email: ");
            var email = Console.ReadLine();

            Console.Write("Phone: ");
            var phone = Console.ReadLine();

            Console.Write("Street Number: ");
            var streetNo = Console.ReadLine();

            Console.Write("Street: ");
            var street = Console.ReadLine();

            Console.Write("City: ");
            var city = Console.ReadLine();

            Console.Write("State: ");
            var state = Console.ReadLine();

            Console.Write("Enter a password: ");
            var password = Console.ReadLine();

            var newDoctor = new Doctor(GenerateDoctorID(), password, fname, lname, email, phone, streetNo, street, city, state);
            File.AppendAllText("emp.txt", newDoctor.ToString() + Environment.NewLine);
        }

        //Adds a patient by creating a patient object and adding it to the database as a ToString
        public void AddPatient()
        {
            Console.Clear();
            Header("Add Patient");
            Console.WriteLine("Registering a new patient with the Hospital Management System \n");
            Console.WriteLine("Patient ID: " + GeneratePatientID());
            Console.Write("First Name: ");
            var fname = Console.ReadLine();

            Console.Write("Last Name: ");
            var lname = Console.ReadLine();

            Console.Write("Email: ");
            var email = Console.ReadLine();

            Console.Write("Phone: ");
            var phone = Console.ReadLine();

            Console.Write("Street Number: ");
            var streetNo = Console.ReadLine();

            Console.Write("Street: ");
            var street = Console.ReadLine();

            Console.Write("City: ");
            var city = Console.ReadLine();

            Console.Write("State: ");
            var state = Console.ReadLine();

            Console.Write("Enter a password: ");
            var password = Console.ReadLine();

            Console.WriteLine();
            var newPatient = new Patient(GeneratePatientID(), password, fname, lname, email, phone, streetNo, street, city, state);
            File.AppendAllText("emp.txt", newPatient.ToString() + Environment.NewLine);
        }

        public int GenerateDoctorID()
        {
            int count = 100;
            foreach (var line in File.ReadAllLines("emp.txt"))
            {
                string[] contents = line.Split(',');
                string role = contents[0];
                if (role == "doctor")
                {
                    count++;
                }
            }
            return count;
        }

        public int GeneratePatientID()
        {
            int count = 200;
            foreach (var line in File.ReadAllLines("emp.txt"))
            {
                string[] contents = line.Split(',');
                string role = contents[0];
                if (role == "patient")
                {
                    count++;
                }
            }
            return count;
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
            Console.WriteLine("1. List all doctors");
            Console.WriteLine("2. Check doctor details");
            Console.WriteLine("3. List all patients");
            Console.WriteLine("4. Check patient details");
            Console.WriteLine("5. Add doctor");
            Console.WriteLine("6. Add patient");
            Console.WriteLine("7. Logout");
            Console.WriteLine("8. Exit \n");
        }

    }
}
