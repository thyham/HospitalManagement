//Timothy Pham 24842813 
//31927/32998 Assignment 1: C# Console Application

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