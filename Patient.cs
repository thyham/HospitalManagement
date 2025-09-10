using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagement
{
    class Patient : User
    {
        private string role;
        private int patientID;
        private string patientPW;
        private int? DoctorID;
        public Patient(int id, string password, int? doctorID = null)
            : base(id, password)
        {
            role = "patient";
            patientID = id;
            patientPW = password;
            DoctorID = doctorID;
        }

        public override void ShowMenu(List<User> allUsers)
        {
            bool running = true;
            while (running)
            {
                Console.WriteLine("Patient Menu");
                string choice = Console.ReadLine();

                if (choice == "1")
                {
                    Console.Clear();
                    ListDetails();
                }

                else if (choice == "2")
                {
                    Console.Clear();
                    ListDoctor();
                }

                else if (choice == "3")
                {
                    Console.Clear();
                    var ap = new Appointment(100, patientID, "cc" + "\n");
                    Console.WriteLine(ap);
                }
            }
        }
        public void ListDetails()
        {
            Console.WriteLine(patientID + patientPW + DoctorID);
        }

        public void ListDoctor()
        {

            if (DoctorID == null)
            {
                Console.WriteLine("No doctor");
            }

            else
            {
                foreach (var line in File.ReadAllLines("emp.txt"))
                {
                    string[] contents = line.Split(',');
                    string role = contents[0];
                    int id = int.Parse(contents[1]);
                    string password = contents[2];

                    if (role == "doctor" && id == DoctorID)
                    {
                        Console.WriteLine("WORK");
                        Console.WriteLine(id);
                        return;
                    }
                    }
                Console.WriteLine("NO WORK");
            }

            }



        public int PatientID
        {
            get { return patientID; }
        }

        public string PatientPW
        {
            get { return patientPW; }
        }

    }
}