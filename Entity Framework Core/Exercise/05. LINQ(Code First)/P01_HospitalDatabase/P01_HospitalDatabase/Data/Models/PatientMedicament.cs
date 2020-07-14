using System.ComponentModel.DataAnnotations;

namespace P01_HospitalDatabase.Data.Models
{
    public class PatientMedicament
    {
        [Required]
        public int PatientId { get; set; }
        public virtual Patient Patient { get; set; }

        [Required]
        public int MedicamentId { get; set; }
        public virtual Medicament Medicament { get; set; }
    }
}
