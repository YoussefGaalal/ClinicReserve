namespace ClinicsReservation.Models
{
    public class Clinic
    {
        public int ClinicId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Address {  get; set; }
        public string ContactInfo { get; set; }

        public ICollection<ClinicDoctor> ClinicDoctors { get; set; }
    }
}
