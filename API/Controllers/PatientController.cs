using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTOs;
using API.Errors;
using API.Helpers;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class PatientController : BaseController
    {
        private readonly IUnityOfWork _unityOfWork;
        private readonly IMapper _mapper;
        public PatientController(IUnityOfWork unityOfWork, IMapper mapper)
        {
            _unityOfWork = unityOfWork;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<PatientDTO>>> GetAll() {
            var patients = await _unityOfWork.GetRepository<Patient>().GetAllAsync();
            
            return Ok(_mapper.Map<IReadOnlyList<Patient>, IReadOnlyList<PatientDTO>>(patients));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PatientDTO>> GetById(Guid id) {
            var patient = await _unityOfWork.GetRepository<Patient>().GetByIdAsync(id);
            
            return Ok(_mapper.Map<Patient, PatientDTO>(patient));
        }

        [HttpPost]
        public async Task<ActionResult<PatientDTO>> Create([FromBody] PatientDTO patient) {
            var spec = new PatientByCpfSpecification(patient.Cpf);
            var hasPatient = await _unityOfWork.GetRepository<Patient>().GetEntityWithSpec(spec);
            var pat = _mapper.Map<PatientDTO, Patient>(patient);

            if(hasPatient != null) {
                return BadRequest(new ApiErrorResponse(400, "CPF já cadastrado"));
            }

            if(!CpfEvaluator.IsValidCPF(patient.Cpf)) {
                return BadRequest(new ApiErrorResponse(400, "CPF inválido"));
            }

            _unityOfWork.GetRepository<Patient>().Add(pat);
            await _unityOfWork.Complete();
            
            return Ok(pat);
        }

        [HttpPut]
        public async Task<ActionResult<PatientDTO>> Update([FromBody] PatientDTO patient) {
            var spec = new PatientByCpfSpecification(patient.Cpf);
            var patientEntity = await _unityOfWork.GetRepository<Patient>().GetByIdAsync(patient.Id);
            var hasPatient = await _unityOfWork.GetRepository<Patient>().GetEntityWithSpec(spec);

            if(patientEntity == null) {
                return NotFound(new ApiErrorResponse(404, "Paciente não cadastrado"));
            }

            if(hasPatient != null && hasPatient.Id != patient.Id) {
                return BadRequest(new ApiErrorResponse(400, "CPF pertence a outro paciente"));
            }

            if(!CpfEvaluator.IsValidCPF(patient.Cpf)) {
                return BadRequest(new ApiErrorResponse(400, "CPF inválido"));
            }

            var pat = _mapper.Map<PatientDTO, Patient>(patient);
            patientEntity.BirthDate = patient.BirthDate;
            patientEntity.Name = patient.Name;
            patientEntity.Cpf = patient.Cpf;
            patientEntity.DoctorId = patient.DoctorId;
            
            _unityOfWork.GetRepository<Patient>().Update(patientEntity);
            await _unityOfWork.Complete();
            
            return Ok(patient);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id) {
            var patient = await _unityOfWork.GetRepository<Patient>().GetByIdAsync(id);
            
            if(patient == null) return NotFound(new ApiErrorResponse(404, "Paciente não encontrado"));

            _unityOfWork.GetRepository<Patient>().Delete(patient);
            await _unityOfWork.Complete();
            
            return NoContent();
        }
    }
}