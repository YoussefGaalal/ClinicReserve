using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
namespace ClinicsReservation.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) {
        }


        public DbSet<Clinic> Clinics { get; set; }
        public DbSet<Doctor> Doctors {  get; set; }
       public DbSet<ClinicDoctor> ClinicDoctors { get; set; }
        
        
    }
}
