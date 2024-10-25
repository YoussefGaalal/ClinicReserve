namespace ClinicsReservation.Dto
{
    public class DoctorDto //for get
    {
        public int Id { get; set; }
        public string DoctorName { get; set; }
        //public string LastName { get; set; }
        public string Specialization { get; set; }
        public string ClinicName { get; set; }
        public string Contact { get; set; }
    }
}
