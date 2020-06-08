using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital
{
    public class Doctor
    {
        private readonly List<Patient> patients;
        
        private Doctor()
        {
            this.patients = new List<Patient>();
        }
        public Doctor(string name, string secondname)
            : this()
        {
            this.FirstName = name;
            this.SecondName = secondname;
        }
        public string FirstName { get; }
        public string SecondName { get; }

        public string FullName => this.FirstName + this.SecondName;
        public void AddPatientToDoctor(Patient patient)
        {
            this.patients.Add(patient);
        }
        public IReadOnlyCollection<Patient> Patients => this.patients;
    }
}
