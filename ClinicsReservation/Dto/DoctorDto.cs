namespace ClinicsReservation.Dto
{
    public class DoctorDto //for get
    {
        public int DoctorId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Email { get; set; }
        public string Phone { get; set; }
        public string Speciality { get; set; }
    }
}
