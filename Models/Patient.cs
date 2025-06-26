using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Hospital_Project.Models
{
    public class Patient
    {
            [Key]
            public int PatientId { get; set; }

            [Range(0, 99999)]
            [Required]
            public int FileNo { get; set; }

            [Required(ErrorMessage = "Patient name is required.")]
            [StringLength(100, MinimumLength = 3,ErrorMessage = "Name can't be more than 100 characters.")]
            public string PatientFullName { get; set; }=string.Empty;

        [DataType(DataType.Date)]
        [Display(Name = "Date of Birth")]
        public DateTime? DateOfBirth { get; set; } = new DateTime(2004, 2, 20);

            public Gender Gender { get; set; } = Gender.female;

            [EmailAddress(ErrorMessage = "Invalid email address.")] 
            public string? PatientEmail { get; set; }

            [Phone(ErrorMessage = "Invalid phone number.")] 
            public string? PatientPhoneNumber { get; set; }

            [StringLength(200)]
            public string? Address { get; set; }

           public IEnumerable<Appointment>? Appointments { get; set; }
        }
    public enum Gender { 
        male=1,
        female=2,
    }
}
