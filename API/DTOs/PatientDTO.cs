using System;
using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class PatientDTO
    {
        [Required(ErrorMessage = "O Id é obrigatório")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório")]
        public string Name { get; set; }

        [Required(ErrorMessage = "A data de nascimento é obrigatória")]
        public DateTime BirthDate { get; set; }

        [Required(ErrorMessage = "O CPF é obrigatório")]
        [MaxLength(11, ErrorMessage = "O CPF precisa ter 11 dígitos")]
        [MinLength(11, ErrorMessage = "O CPF precisa ter 11 dígitos")]
        public string Cpf { get; set; }

        [Required(ErrorMessage = "Um médico precisa ser atribuído ao paciente")]
        public Guid DoctorId { get; set; }
    }
}