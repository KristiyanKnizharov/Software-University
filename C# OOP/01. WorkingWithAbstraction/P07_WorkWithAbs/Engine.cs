using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hospital
{
    public class Engine
    {
        private const int MAX_CAPACITY = 3;
        private readonly List<Department> departments;
        private readonly List<Doctor> doctors;

        public Engine()
        {
            this.departments = new List<Department>();
            this.doctors = new List<Doctor>();
        }
        public void Run()
        {
            string command = Console.ReadLine();
            while (command != "Output")
            {
                string departmentName = command.Split()[0];
                string fNameDoctor = command.Split()[1];
                string sNameDoctor = command.Split()[2];
                string patientName = command.Split()[3];
                string fullName = fNameDoctor + sNameDoctor;

                if (!this.DoctorExists(fullName))
                {
                    doctors.Add(new Doctor
                        (fNameDoctor, sNameDoctor));
                    
                }
                if (!this.DepartmentExists(departmentName))
                {
                    this.departments.Add(new Department
                        (departmentName));
                }

                Department department = this.departments
                    .FirstOrDefault
                    (d => d.Name == departmentName);
                Doctor doctor = this.doctors
                    .FirstOrDefault(d => d.FullName ==
                    fullName);


                bool isFree = department
                    .Rooms
                    .Any(r => r.Count < MAX_CAPACITY);
                if (isFree)
                {
                    Room firstFreeRoom = department.GetFirstRooms();
                    Patient patient = new Patient(patientName);
                    
                    firstFreeRoom.AddPatient(new Patient(fullName));
                    doctor.AddPatientToDoctor(patient);
                }



                command = Console.ReadLine();
            }

            command = Console.ReadLine();

            while (command != "End")
            {
                string[] args = command.Split();

                if (args.Length == 1)
                {
                    Room[] rooms = (Room[])this.departments
                        .FirstOrDefault(d => d.Name == command)
                        .Rooms;
                    foreach (var room in rooms)
                    {
                        Console.WriteLine(room);
                    }
                }
                else if (args.Length == 2 && int.TryParse(args[1], out int roomNum))
                {
                    Room room = this.departments
                        .FirstOrDefault(d => d.Name == command)
                        .Rooms
                        .FirstOrDefault
                        (r => r.Number == roomNum);
                    string[] output = room
                        .ToString()
                        .Split(Environment.NewLine)
                        .OrderBy(r => r)
                        .ToArray();

                    foreach (var pat in output)
                    {
                        Console.WriteLine(pat);
                    }
                }
                else
                {
                    string doctorfullname = args[0] + args[1];
                    Doctor doctor = this.doctors
                        .FirstOrDefault(d => d.FullName == doctorfullname);
                   
                    foreach (var pat in doctor.Patients)
                    {
                        Console.WriteLine(pat);
                    }
                }
                command = Console.ReadLine();
            }

        }

        private bool DoctorExists(string fullName)
        {
            bool exists = doctors.Any(d => d.FullName == fullName);
            return exists;
        }
        private bool DepartmentExists(string dep)
        {
            bool exists = this.departments
                .Any(d => d.Name == dep);
            return exists;
        }

    }
}
