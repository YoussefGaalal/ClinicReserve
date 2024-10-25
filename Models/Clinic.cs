namespace ClinicsReservation.Models
{
    public class Clinic
    {
        public int Id { get; set; } 
        public string ClinicName { get; set; } //ClinicName
       // public string Description { get; set; } 
        public string Address {  get; set; }
        public string Contact { get; set; }

        public ICollection<ClinicDoctor> ClinicDoctors { get; set; }
    }
}
