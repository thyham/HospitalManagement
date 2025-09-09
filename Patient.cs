using System;
using System.Collections.Generic;
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
            DoctorID = doctorID;
        }

        public override void ShowMenu()
        {
            bool running = true;
            while (running)
            {
                Console.WriteLine("Patient Menu");
                string choice = Console.ReadLine();
                if (choice == "1")
                {
                    var ap = new Appointment(100, patientID, "cc");
                    Console.WriteLine(ap);
                }
                break;
            }
        }
        public void ListDetails()
        {
            Console.WriteLine(patientID + patientPW);
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