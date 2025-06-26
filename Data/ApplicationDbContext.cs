using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Hospital_Project.Models;

namespace Hospital_Project.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Doctor> tblDoctors { get; set; } = default!;
        public DbSet<Patient> tblPatients { get; set; } = default!;
        public DbSet<Appointment> tblAppointments { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Doctor>().HasData(
                new Doctor { DoctorId = 1, Age = 29, DoctorFullName = "Mohnnad Ahmed", Speclization = Spec.Surgeon, YearsOfExperience = 4, DoctorPhoneNumber = "-", DoctorEmail = "-" },
                new Doctor { DoctorId = 2, Age = 30, DoctorFullName = "Mohnnad Ahmed1", Speclization = Spec.Cardiologist, YearsOfExperience = 10, DoctorPhoneNumber = "-", DoctorEmail = "-" },
                new Doctor { DoctorId = 3, Age = 35, DoctorFullName = "Mohnnad Ahmed3", Speclization = Spec.Cardiologist, YearsOfExperience = 15, DoctorPhoneNumber = "-", DoctorEmail = "-" },
                new Doctor { DoctorId = 4, Age = 40, DoctorFullName = "Mohnnad Ahmed4", Speclization = Spec.Oncologist, YearsOfExperience = 2, DoctorPhoneNumber = "-", DoctorEmail = "-" },
                new Doctor { DoctorId = 5, Age = 38, DoctorFullName = "Mohnnad Ahmed5", Speclization = Spec.Cardiologist, YearsOfExperience = 8, DoctorPhoneNumber = "-", DoctorEmail = "-" },
                new Doctor { DoctorId = 6, Age = 60, DoctorFullName = "Yazeed amjad", Speclization = Spec.Neurologist, YearsOfExperience = 5, DoctorPhoneNumber = "-", DoctorEmail = "-" }
                );
            modelBuilder.Entity<Patient>().HasData(
                new Patient { PatientId = 1, FileNo = 44444, PatientFullName = "Mohannad bb", DateOfBirth = new DateTime(2004, 4, 5),Address="-",Gender=Gender.male,PatientEmail="-",PatientPhoneNumber="-" },
                new Patient { PatientId = 2, FileNo = 11111, PatientFullName = "Mohannad cc", DateOfBirth = new DateTime(2002, 6, 5),Address="-",Gender=Gender.male,PatientEmail="-",PatientPhoneNumber="-" },
                new Patient { PatientId = 3, FileNo = 22222, PatientFullName = "Mohannad dd", DateOfBirth = new DateTime(2000, 8, 3), Address = "-", Gender = Gender.male, PatientEmail = "-", PatientPhoneNumber = "-" },
                new Patient { PatientId = 4, FileNo = 55555, PatientFullName = "Mohannad vv", DateOfBirth = new DateTime(2002, 6, 5), Address = "-", Gender = Gender.male, PatientEmail = "-", PatientPhoneNumber = "-" },
                new Patient { PatientId = 6, FileNo = 66666, PatientFullName = "Mohannad ll", DateOfBirth = new DateTime(2004, 4, 5), Address = "-", Gender = Gender.male, PatientEmail = "-", PatientPhoneNumber = "-" },
                new Patient { PatientId = 8, FileNo = 99999, PatientFullName = "Mohannad mm", DateOfBirth = new DateTime(2002, 6, 5), Address = "-", Gender = Gender.male, PatientEmail = "-", PatientPhoneNumber = "-" },
                new Patient { PatientId = 9, FileNo = 33333, PatientFullName = "Mohannad mm", DateOfBirth = new DateTime(2000, 8, 3), Address = "-", Gender = Gender.male, PatientEmail = "-", PatientPhoneNumber = "-" },
                new Patient { PatientId = 10, FileNo = 77777, PatientFullName = "Mohannad mm", DateOfBirth = new DateTime(2002, 6, 5), Address = "-", Gender = Gender.male, PatientEmail = "-", PatientPhoneNumber = "-" }
                );

        }
    }
}
