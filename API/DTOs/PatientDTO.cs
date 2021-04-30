using System;

namespace API.DTOs
{
    public class PatientDTO
    {
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public string Cpf { get; set; }
        public Guid DoctorId { get; set; }
    }
}