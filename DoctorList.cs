//using System;
//using System.Collections.Generic;
//using System.Globalization;
//using System.IO;

//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace HospitalManagement
//{
//    class DoctorList
//    {
//        // Write code for to Create a List to store the information from emp.txt file
//        List<Doctor> doctors;

//        // EmployeeList constructor
//        public DoctorList()
//        {
//            // Create an instance of List<>
//            doctors = new List<Doctor>();
//        }

//        // Method to process a string which contain a single line for the emp.txt
//        // The lfile name is passed as a parameter to the Method
//        public void LoadDoctors(string filename)
//        {
//            // Read the file content using the StreamReader
//            StreamReader fileContent = new StreamReader(filename);
//            // Create an object of Employee class
//            Doctor newDoctor;

//            // Read the StremReader till the last line
//            while (!fileContent.EndOfStream)
//            {
//                // At this point there are still employees to be loaded
//                newDoctor = new Doctor();
//                // Write code to Read each line and from the StreamReader
//                string line = fileContent.ReadLine();
//                // Write code to Load the employee detail from file to respective fields
//                newDoctor.LoadDoctor(line);
//                // Write code to Add the details to the list collection
//                doctors.Add(newDoctor);
//            }
//            // Write code to Close the StreamReader
//            fileContent.Close();
//        }

//        // Method to display the details for all the Employee
//        public void PrintDoctors()
//        {
//            // prints each employee details in the employees list on a seperate line
//            foreach (Doctor doctor in this.doctors)
//            {
//                Console.WriteLine("Doctor Details: " + doctor.ToString());
//            }

//        }

//        public bool ReturnDoctors(int id, string password)
//        {
//            // prints each employee details in the employees list on a seperate line
//            foreach (Doctor doctor in this.doctors)
//            {
//                if (doctor.DoctorID == id && doctor.DoctorPW == password)
//                {
//                    return true;
//                }
//            }
//            return false;
//        }

//        //}
//        //  Write code to Sort the employee details based on the Employee ID
//        //public void SortEmployees()
//        //{
//        //    employees.Sort();
//        //}
//    }
//}