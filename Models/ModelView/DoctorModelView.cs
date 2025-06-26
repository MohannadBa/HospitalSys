namespace Hospital_Project.Models.ModelView
{
    public class DoctorModelView
    {
        public string PatientFullName { get; set; } = string.Empty;
        public int FileNo { get; set; }
        //result
        public IEnumerable<Patient> Patients { get; set; } = new List<Patient>();
        public IEnumerable<Appointment> appointments { get; set; } = new List<Appointment>();

    }
}
