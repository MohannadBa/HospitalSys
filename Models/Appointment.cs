using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Hospital_Project.Models
{
    public class Appointment
    {
        [Key]
        public int AppointmentId { get; set; }

        [Required(ErrorMessage = "Appointment date is required.")]
        [DataType(DataType.Date)]
        [Display(Name = "Appointment Date")]
        public DateTime AppointmentDate { get; set; }

        [StringLength(300, ErrorMessage = "Diagnosis cannot exceed 300 characters.")] 
        public string? Diagnosis { get; set; }


        [Range(10, 200, ErrorMessage = "Consultation fee must be between 10 and 200.")] 
        public double ConsultationFee { get; set; }

        [DataType(DataType.Time)]
        [Display(Name = "Appointment Time")]
        public DateTime AppointmentTime { get; set; }

        public int PatientId { get; set; }
        public int DoctorId { get; set; }
        //navigation
        public Doctor? Doctor { get; set; }
        public Patient? Patient { get; set; }

    }
}