using API.DTOs;
using AutoMapper;
using Core.Entities;

namespace API.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles() {
            CreateMap<Doctor, DoctorDTO>().ReverseMap();
            CreateMap<Patient, PatientDTO>().ReverseMap();
        }
    }
}