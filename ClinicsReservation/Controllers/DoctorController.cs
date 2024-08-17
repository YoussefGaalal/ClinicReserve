using AutoMapper;
using ClinicsReservation.Dto;
using ClinicsReservation.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ClinicsReservation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]


    public class DoctorController : ControllerBase
    {
        private readonly IMapper _mapper;


        public ApplicationDbContext _context;
        public DoctorController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/<DoctorController>
        [HttpGet]
        public async Task<IActionResult> GetAllDoctors()
        {
            var doctors = await _context.Doctors.ToListAsync();
            var doctorsdto = doctors.Select(d => _mapper.Map<DoctorDto>(d));
            if (doctors.Count() == 0) //when array is 0 (empty) 
            {
                return NotFound("No Doctor was Found ");
            }
            return Ok(doctorsdto);
        }

        // GET api/<DoctorController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var doctor = await _context.Doctors.Where(D => D.DoctorId == id).FirstOrDefaultAsync();

            if (doctor == null)
            {
                return NotFound($"No Doctor was Found with ID : {id}");
            }
            return Ok(_mapper.Map<DoctorDto>(doctor));

        }

        [HttpGet]//get by name
        [Route("getbyname")]
        public async Task<IActionResult> GetName([FromQuery]string name)
        {
            
            var doctors = await _context.Doctors.Where(D => D.FirstName.Contains(name)||D.LastName.Contains(name)).ToListAsync();

            if (doctors.Count() == 0) //when array is 0 (empty) 
            {
                return NotFound($"No Doctor was Found with this Name : {name}");
            }
            var doctorsdto = doctors.Select(d => _mapper.Map<DoctorDto>(d));
            return Ok(doctorsdto);

        }

        // POST api/<DoctorController>
        [HttpPost] //CREATE
        public void Post([FromBody] DoctorCommand doctorCommand)
        {
            Doctor doctor = _mapper.Map<Doctor>(doctorCommand);
            _context.Doctors.Add(doctor);
            _context.SaveChanges();

        }

        // PUT api/<DoctorController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody]  DoctorCommand doctorCommand)
        {
            var doctor = await _context.Doctors.SingleOrDefaultAsync(d => d.DoctorId == id);
            if (doctor == null)
                return NotFound($"No Doctor was Found with ID : {id}");

            _context.Entry(doctor).CurrentValues.SetValues(doctorCommand); 
            _context.SaveChanges();

            return Ok(doctor);

        }

        // DELETE api/<DoctorController>/5
        [HttpDelete("{id}")]

        public async Task<IActionResult> DeleteAsync(int id)
        {
            
            var doctor = await _context.Doctors.SingleOrDefaultAsync(d => d.DoctorId == id);
            if (doctor == null)
                return NotFound($"No Doctor was Found with ID : {id}");

            _context.Doctors.Remove(doctor);
            _context.SaveChanges();
            return Ok(_mapper.Map<DoctorDto>(doctor));


        }
    }
}
