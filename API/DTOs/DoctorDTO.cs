using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class DoctorDTO
    {
        [Required(ErrorMessage = "O Id é obrigatório")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório")]
        public string Name { get; set; }

        [Required(ErrorMessage = "O CRM é obrigatório")]
        public string Crm { get; set; }

        [Required(ErrorMessage = "O CRM UF é obrigatório")]
        [MaxLength(2)]
        public string CrmUf { get; set; }
        
        public ICollection<PatientDTO> Patients { get; set; }
    }
}