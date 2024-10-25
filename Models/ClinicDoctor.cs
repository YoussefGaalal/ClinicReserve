namespace ClinicsReservation.Models
{
    public class ClinicDoctor
    {

        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; }

        public int ClinicId { get; set; }
        public Clinic Clinic { get; set; }

        public int ClinicDoctorId { get; set;}
        
    }
}
