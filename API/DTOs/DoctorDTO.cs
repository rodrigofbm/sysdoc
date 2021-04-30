using System.Collections.Generic;
using Core.Entities;

namespace API.DTOs
{
    public class DoctorDTO
    {
        public string Name { get; set; }
        public string Crm { get; set; }
        public string CrmUf { get; set; }
        public ICollection<Patient> Patients { get; set; }
    }
}