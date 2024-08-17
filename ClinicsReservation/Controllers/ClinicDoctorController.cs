using AutoMapper;
using ClinicsReservation.Dto;
using ClinicsReservation.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace ClinicsReservation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClinicDoctorController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ClinicDoctorController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            
        }

        // GET: api/<ClinicDoctorController>
        [HttpGet]
        public async Task<IActionResult> GetAllClinicDoctors()
        {
            var clinicDoctors = await _context.ClinicDoctors.ToListAsync();

            if (!clinicDoctors.Any())
            {
                return NotFound("No Clinic-Doctor associations were found.");
            }

            return Ok(clinicDoctors);
        }

        // GET api/<ClinicDoctorController>/clinic/5/doctor/10
        [HttpGet("clinic/{clinicId}")]
        public async Task<IActionResult> Get(int clinicId) //doctors in clinic x
        {
            var ClinicCheck = await _context.Clinics.FirstOrDefaultAsync(c => c.ClinicId == clinicId);
            if (ClinicCheck == null)
            {
                return NotFound($"No Clinic Found with this ID : {clinicId}");
            }
            var clinicDoctor = await _context.ClinicDoctors
                .Where(cd => cd.ClinicId == clinicId )
                .Select(cd => cd.Doctor).ToListAsync(); // multiple dr per clinic

            if (clinicDoctor == null)
            {
                return NotFound($"No Doctors found in Clinic ID: {clinicId} ");
            }
            var doctorsdto = clinicDoctor.Select(d => _mapper.Map<DoctorDto>(d));
            return Ok(doctorsdto);
        }
        [HttpGet("doctor/{doctorId}")]
        public async Task<IActionResult> GetDR(int doctorId) //clinics w/ dr x
        {
            var DrCheck = await _context.Doctors.FirstOrDefaultAsync(d => d.DoctorId == doctorId);
            if (DrCheck == null)
            {
                return NotFound($"No Doctor Found with this ID : {doctorId}");
            }
            var clinicDoctor = await _context.ClinicDoctors
                .Where(dc => dc.DoctorId == doctorId)
                .Select(dc => dc.Clinic).ToListAsync(); // multiple clinic w/ dr

            if (clinicDoctor.Count()>= 0)
            {
                return NotFound($"No Clinics found for this Doctor {DrCheck.FirstName+" "+ DrCheck.LastName} ");
            }
            var clinicsdto = clinicDoctor.Select(dc => _mapper.Map<ClinicDto>(dc));
            return Ok(clinicsdto);
        }

        // POST api/<ClinicDoctorController>
        [HttpPost]
        public async Task<IActionResult> Create(int doctorid , int clinicid)
        {
            ClinicDoctor clinicDoctor = new ClinicDoctor() {DoctorId = doctorid , ClinicId = clinicid };
            _context.ClinicDoctors .Add(clinicDoctor);
            await _context.SaveChangesAsync();
            return Ok();
        
        }

      
       

        // DELETE api/<ClinicDoctorController>/clinic/5/doctor/10
        [HttpDelete("clinic/{clinicId}/doctor/{doctorId}")]
        public async Task<IActionResult> Delete(int clinicId, int doctorId)
        {
            var clinicDoctor = await _context.ClinicDoctors
                .Where(cd => cd.ClinicId == clinicId && cd.DoctorId == doctorId)
                .FirstOrDefaultAsync();

            if (clinicDoctor == null)
            {
                return NotFound($"No association found between Clinic ID: {clinicId} and Doctor ID: {doctorId}");
            }

            _context.ClinicDoctors.Remove(clinicDoctor);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
