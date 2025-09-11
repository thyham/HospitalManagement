using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

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
                    ListAppointments();
                }


                else if (choice == "4")
                {
                    Console.Clear();
                    BookAppointment();
                }

                else if (choice == "5")
                {
                    Console.Clear();
                    running = false;
                    break;
                }

                else if (choice == "6")
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
        public void ListAppointments()
        {
            foreach (var line in File.ReadAllLines("appointments.txt"))
            {
                string[] contents = line.Split(',');
                int docID = int.Parse(contents[0]);
                int patID = int.Parse(contents[1]);
                if (patID == patientID)
                {
                    string description = contents[2];
                    string name = GetDoctorName(docID);
                    Console.WriteLine(name + patID + description);
                    //Book appointment with patientID or patient name?
                }
            }
        }

        public string GetDoctorName(int id)
        {
            foreach (var line in File.ReadAllLines("emp.txt"))
            {
                string[] contents = line.Split(',');
                int docID = int.Parse(contents[1]);
                if (docID == id)
                {
                    string name = contents[2];
                    return name;
                }
            }
            return "";
        }

        public void BookAppointment()
        {

            if (DoctorID != null)
            {
                Console.WriteLine("Booking with doctor: ");
                var ap = new Appointment(DoctorID.Value, patientID, "Working?");
                File.AppendAllText("appointments.txt", ap.ToString() + Environment.NewLine);
            }
            while (DoctorID == null)
            {
                Console.WriteLine("Choose a doctor");
                int chooseDoctor = int.Parse(Console.ReadLine());

                if (DoctorExists(chooseDoctor))
                {
                    DoctorID = chooseDoctor;
                    var ap = new Appointment(chooseDoctor, patientID, "TIMOTHY");
                    File.AppendAllText("appointments.txt", ap.ToString() + Environment.NewLine);
                    break;
                }

                else {
                    Console.WriteLine("No exist Try again");
                }
                
            }
        }


        public bool DoctorExists(int id)
        {
            foreach (var line in File.ReadAllLines("emp.txt"))
            {
                string[] contents = line.Split(',');
                string role = contents[0];
                int docID = int.Parse(contents[1]);
                if (role == "doctor" && docID == id)
                {
                    Console.WriteLine(docID + " " + id);
                    return true;
                }
            }
            return false;
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