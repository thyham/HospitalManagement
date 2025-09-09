using System;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Security.Principal;

namespace HospitalManagement
{
    class Program //single database file, role in index 0, separate search method depending on the role
    {
        static void Main(string[] args)
        {
            List<User> users = LoadUsers("emp.txt");
            User loggedInUser = null;

            while (loggedInUser == null)
            {
                Console.Write("Enter ID: ");
                int id = int.Parse(Console.ReadLine());

                Console.Write("Enter password: ");
                string password = Console.ReadLine();

                loggedInUser = users
                    .FirstOrDefault(u => u.ID == id && u.Password == password);

                if (loggedInUser == null)
                    Console.WriteLine("Try again");
                }
                Console.WriteLine("Valid Credentials");
            Console.Clear();
                loggedInUser.ShowMenu();
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

                    return role switch
                    {
                        "doctor" => new Doctor(id, password),
                        "patient" => new Doctor(id, password),
                        _ => throw new Exception($"Unknown role: {role}")
                    };
                }
            }
        }