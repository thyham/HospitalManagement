using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagement
{
    class Appointment
    {
        public int DoctorID;
        public int PatientID;
        public string Description;

        public Appointment(int doctorID,  int patientID, string description)
        {
            DoctorID = doctorID;
            PatientID = patientID;
            Description = description;
        }

        public override string ToString()
        {
            return $"{DoctorID},{PatientID},{Description}";
        }
    }
}