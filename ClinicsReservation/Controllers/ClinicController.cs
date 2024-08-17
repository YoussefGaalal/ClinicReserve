using AutoMapper;
using ClinicsReservation.Dto;
using ClinicsReservation.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ClinicsReservation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClinicController : ControllerBase
    {
        private readonly IMapper _mapper;


        public ApplicationDbContext _context;
        public ClinicController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/<ClinicController>
        [HttpGet]
        public async Task<IActionResult> GetAllClinics()
        {
            var clinics = await _context.Clinics.ToListAsync();
            var clinicsdto = clinics.Select(c => _mapper.Map<ClinicDto>(c));
            if (clinics.Count() == 0) //when array is 0 (empty) 
            {
                return NotFound("No Clinic was Found ");
            }
            return Ok(clinicsdto);
        }

        // GET api/<ClinicController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByID(int id)
        {

            var clinic = await _context.Clinics.Where(C => C.ClinicId == id).FirstOrDefaultAsync();

            if (clinic == null)
            {
                return NotFound($"No Clinic was Found with ID : {id}");
            }
            return Ok(_mapper.Map<ClinicDto>(clinic));

        }

        [HttpGet]//get by name
        [Route("getbyname")]
        public async Task<IActionResult> GetName([FromQuery] string name)
        {

            var clinics = await _context.Clinics.Where(C => C.Name.Contains(name)).ToListAsync();

            if (clinics.Count() == 0) //when array is 0 (empty) 
            {
                return NotFound($"No Clinic was Found with this Name : {name}");
            }
            var clinicsdto = clinics.Select(c => _mapper.Map<ClinicDto>(c));
            return Ok(clinicsdto);

        }

        // POST api/<ClinicController>
        [HttpPost] //CREATE
        public void Post([FromBody] ClinicCommand clinicCommand)
        {
            Clinic clinic = _mapper.Map<Clinic>(clinicCommand);
            _context.Clinics.Add(clinic);
            _context.SaveChanges();

        }

        // PUT api/<ClinicController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] ClinicCommand clinicCommand)
        {
            var clinic = await _context.Clinics.SingleOrDefaultAsync(c => c.ClinicId == id);
            if (clinic == null)
                return NotFound($"No Clinic was Found with ID : {id}");

            _context.Entry(clinic).CurrentValues.SetValues(clinicCommand);
            _context.SaveChanges();

            return Ok(_mapper.Map<ClinicDto>(clinic));

        }

        // DELETE api/<ClinicController>/5
        [HttpDelete("{id}")]

        public async Task<IActionResult> DeleteAsync(int id)
        {

            var clinic = await _context.Clinics.SingleOrDefaultAsync(c => c.ClinicId == id);
            if (clinic == null)
                return NotFound($"No Clinic was Found with ID : {id}");

            _context.Clinics.Remove(clinic);
            _context.SaveChanges();
            return Ok(_mapper.Map<ClinicDto>(clinic));


        }
    }
}
