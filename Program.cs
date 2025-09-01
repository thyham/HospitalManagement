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

            Console.Write("ID: ");
            string id = Console.ReadLine();
            int numID = Convert.ToInt32(id);

            Console.Write("Password: ");
            string password = Console.ReadLine();

            try
            {            
                string[] lines = File.ReadAllLines("C:\\Users\\invic\\source\\repos\\HospitalManagement\\Accounts.txt");

                foreach (string name in lines)
                {
                    char delimiter = ',';
                    string[] parts = name.Split(delimiter);
                    Console.WriteLine($"UserName: {parts[0]}, Password: {parts[1]}");
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