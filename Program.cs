using System;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Security.Principal;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace HospitalManagement
{
    class Program //single database file, role in index 0, separate search method depending on the role
    {
        static void Main(string[] args)
        {
            List<User> users = LoadUsers("emp.txt");
            bool running = true;

            while (running)
            {
                User loggedInUser = null;

                while (loggedInUser == null)
                {
                    Console.Clear();
                    Header();

                    // Validate ID input
                    int id;
                    while (true)
                    {
                        Console.Write("Enter ID: ");
                        string rawId = Console.ReadLine();

                        if (int.TryParse(rawId, out id))
                        {
                            break; // valid number, exit inner loop
                        }
                        else
                        {
                            Console.Clear();
                            Header();
                            Console.WriteLine("Invalid input. Please enter a numeric ID.\n");
                        }
                    }

                    // Password entry
                    Console.Write("Enter password: ");
                    string password = ReadPassword();

                    loggedInUser = users
                        .FirstOrDefault(u => u.ID == id && u.Password == password);

                    if (loggedInUser == null)
                    {
                        Console.WriteLine("Invalid ID or password. Try again.\n");
                        Console.WriteLine("Press any key to retry...");
                        Console.ReadKey();
                    }
                }

                Console.WriteLine("Valid Credentials");
                Console.Clear();
                loggedInUser.ShowMenu(users);
            }
        }

        static List<User> LoadUsers(string filepath)
        {
            // prints each employee details in the employees list on a seperate line
            var users = new List<User>();
            foreach (var line in File.ReadAllLines(filepath))
            {
                users.Add(CreateUserPerLine(line));
            }
            return users;
        }
        static User CreateUserPerLine(string line)
        {
            // Split the comma seperated string into fields 
            string[] contents = line.Split(',');

            // Assign values to respective properties/ members
            string role = contents[0];
            int id = int.Parse(contents[1]);
            string password = contents[2];
            string fname = contents[3];
            string lname = contents[4];
            string email = contents[5];
            string phoneNumber = contents[6];
            int streetno = int.Parse(contents[7]);
            string street = contents[8];
            string city = contents[9];
            string state = contents[10];

            return role switch
            {
                "doctor" => new Doctor(id, password, fname, lname, email, phoneNumber, streetno, street, city, state),
                "patient" => new Patient(id, password, fname, lname, email, phoneNumber, streetno, street, city, state, contents.Length > 11 ? int.Parse(contents[11]) : (int?)null),
                //"admin" => new Admin(id, password),
                _ => throw new Exception($"Unknown role: {role}")
            };
        }

        static string ReadPassword()
        {
            string password = "";
            ConsoleKeyInfo keyInfo;

            do
            {
                keyInfo = Console.ReadKey(intercept: true);

                if (keyInfo.Key == ConsoleKey.Backspace && password.Length > 0)
                {
                    password = password.Substring(0, password.Length - 1);
                    Console.Write("\b \b");
                }
                else if (!char.IsControl(keyInfo.KeyChar))
                {
                    password += keyInfo.KeyChar;
                    Console.Write("*");
                }
            }
            while (keyInfo.Key != ConsoleKey.Enter);
            Console.WriteLine();
            return password;
        }

        public static void Header()
        {
            Console.WriteLine("Timothy's Hospital Management System");
            Console.WriteLine("------------------------------------");
            Console.WriteLine("             Login Page             ");
            Console.WriteLine("\n");
        }
    }
}