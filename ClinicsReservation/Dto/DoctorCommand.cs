namespace ClinicsReservation.Dto
{
    public class DoctorCommand // for Post
    {
        //public int DoctorId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Email { get; set; }
        public string Phone { get; set; }
        public string Speciality { get; set; }

    }
}
