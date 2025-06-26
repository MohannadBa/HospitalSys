using System.ComponentModel.DataAnnotations;

namespace Hospital_Project.Models
{
    public class Doctor
    {
        [Key]
        public int DoctorId { get; set; }

        [Required(ErrorMessage = "Doctor name is required.")]
        [Display(Name ="Full Name")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Name can't be more than 100 characters.")]
        public string DoctorFullName { get; set; }=string.Empty;

        [Required]
        [Range(25,70,ErrorMessage ="Age should between 25 and 70")]
        public int Age { get; set; }
        
        [Required(ErrorMessage = "Speclization is required")]
        public Spec Speclization { get; set;}= Spec.Neurologist;

        [Display(Name = "Doctor Email")]

        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string? DoctorEmail { get; set; }

        [Display(Name = "Phone Number")]
        [Phone(ErrorMessage = "Invalid phone number.")]
        public string? DoctorPhoneNumber { get; set; }

        [Display(Name = "Years of Experience")]
        [Range(0, 50)]
        public int YearsOfExperience { get; set; }

        public IEnumerable<Appointment>? Appointments { get; set; }

    }
    public enum Spec
    {
        Cardiologist=0,
        Oncologist=1,
        Neurologist=2,
        Pediatrician=3,
        Surgeon=4
    }
}
