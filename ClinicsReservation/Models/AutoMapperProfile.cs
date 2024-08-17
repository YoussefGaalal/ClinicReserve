using AutoMapper;
using ClinicsReservation.Dto;

namespace ClinicsReservation.Models
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<DoctorCommand, Doctor>();
            CreateMap<Doctor, DoctorDto>();
            CreateMap<ClinicCommand, Clinic>();
            CreateMap<Clinic, ClinicDto>();
        }
    }
}
