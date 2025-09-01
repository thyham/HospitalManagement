using System;
using System.IO;
/*
 The given text file contains 5 set of user IDs and Password. 
 The file stores userID and password in the following format:
<User>, <password>
Hello, abc1234
Admin, admin,
User, user
Write a program to read a given text file and print the data about the user Id and password. 
Write appropriate exception handling code to handle exceptions which might occur while reading the text file.  
 */


namespace HospitalManagement
{
    class Program2
    {
        static void Main(string[] args)
        {
            bool successLogin = false;

            while (successLogin != true)
            {
                try
                {
                    string[] lines = File.ReadAllLines("Accounts.txt");

                    Console.Write("ID: ");
                    int id = Convert.ToInt32(Console.ReadLine());

                    Console.Write("Password: ");
                    string password = Console.ReadLine();

                    foreach (string name in lines)
                    {
                        char delimiter = ',';
                        string[] parts = name.Split(delimiter);
                        if (Convert.ToInt32(parts[0]) == id && password == parts[1])
                        {
                            Console.WriteLine("Valid credentials");
                            successLogin = true;
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Try again mate");
                            break;
                        }
                    }
                }


                catch (Exception ex)
                {
                    {
                        Console.WriteLine($"File not found: {ex}");
                    }

                }
            }
            }
        }
}