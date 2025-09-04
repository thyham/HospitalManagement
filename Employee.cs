using System;
using System.Runtime.CompilerServices;

namespace HospitalManagement
{
    class Employee : IComparable<Employee>
    {
        // Write code for the Properties of an Employee
        // Name (First and last name), ID#, hourly rate and total working hours per week
        private string employeeFName, employeeLName, employeePW;
        private double hourlyRate, workHours;
        private int employeeID;

        // Write code for the Empolyee Constructor
        public Employee()
        {
            employeeFName = "";
            employeeLName = "";
            hourlyRate = 0;
            workHours = 0;
            employeeID = 0;
            employeePW = "";

        }

        // Write code for the Method to recieve a single line from the emp.txt file as string
        // and extract individual fields from the line by spltting the line using ','
        public void LoadEmployee(string line)
        {
            // Split the comma seperated string into fields 
            string[] contents = line.Split(',');

            // Assign values to respective properties/ members
            employeeFName = contents[0];
            employeeLName = contents[1];
            hourlyRate = Convert.ToDouble(contents[2]);
            workHours = Convert.ToDouble(contents[3]);
            employeeID = Convert.ToInt32(contents[4]);
            employeePW = contents[5];
        }

        // Method to calculate the weekly salary of an Employee
        public double GetWeeklySal()
        {
            return hourlyRate * workHours;
            //workhours & hourly rate multiply
        }

        // Overide the ToString() method to display the employee details
        public override string ToString()
        {

            //use output statement to output every attribute for each entity 
            return $"{employeeFName}{employeeLName}, ID#: {employeeID}, Weekly Income: {GetWeeklySal()}, {employeePW}";
        }

        // Write code for the Implemention of the CompareTo method 
        public int CompareTo(Employee other) //compare employee id smaller/larger, == return 0
        {
            if (employeeID < other.employeeID)
            {
                return -1;
            }
            else if (employeeID == other.employeeID)
            {
                return 1;
            }
            else
            {
                return -0;
            }
            ;
        }

        public int EmployeeID
        {
            get { return employeeID; }
        }

        public string EmployeePW
        {
            get { return employeePW; }
        }

    }
}
