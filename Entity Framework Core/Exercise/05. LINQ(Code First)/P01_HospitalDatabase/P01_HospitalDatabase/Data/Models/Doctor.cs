using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace P01_HospitalDatabase.Data.Models
{
    public class Doctor
    {
        public Doctor()
        {
            this.Visitations = new HashSet<Visitation>();
        }

        [Key]
        public int DoctorId { get; set; }

        [MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(80)]
        public string Specialty { get; set; }

        [MaxLength(80)]
        [EmailAddress]
        public string Email { get; set; }

        [MaxLength(30)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public ICollection<Visitation> Visitations { get; set; }

    }
}
