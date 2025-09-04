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
    class Program
    {
        static void Main(string[] args)
        {

            EmployeeList myEmployees = new EmployeeList();

            try
            {
                myEmployees.LoadEmployees("emp.txt");
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("File not found");
                return;
            }

            bool successLogin = false;

                Console.Write("ID: ");
                int id = Convert.ToInt32(Console.ReadLine());

            if (myEmployees.ReturnEmployees(id))
            {
                Console.WriteLine("Valid");
                successLogin = true;

            }
            //// Display the employee details 
            myEmployees.PrintEmployees();

            // Sort the employee detail and display again.
            myEmployees.SortEmployees();
            Console.WriteLine("\n After Sorting:");
            myEmployees.PrintEmployees();

            Console.ReadKey();
        }
    }
}