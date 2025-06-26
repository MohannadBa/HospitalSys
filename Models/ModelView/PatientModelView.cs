namespace Hospital_Project.Models.ModelView
{
    public class PatientModelView
    {
        public string DoctorFullName { get; set; } = string.Empty;
        public Spec Speclization { get; set; } = Spec.Neurologist;
        //result
        public IEnumerable<Doctor> Doctors { get; set; }= new List<Doctor>();
    }
}
