using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Errors;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class DoctorController : BaseController
    {
        private readonly IUnityOfWork _unityOfWork;
        private readonly IMapper _mapper;
        public DoctorController(IUnityOfWork unityOfWork, IMapper mapper)
        {
            _unityOfWork = unityOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<DoctorDTO>>> GetAll() {
            var spec = new DoctorWihPatientsSpecification();
            var doctors = await _unityOfWork.GetRepository<Doctor>().GetAllWithSpecAsync(spec);
            
            return Ok(_mapper.Map<IReadOnlyList<Doctor>, IReadOnlyList<DoctorDTO>>(doctors));
        }

        [HttpPost]
        public async Task<ActionResult<DoctorDTO>> Create([FromBody] DoctorDTO doctor) {
            var spec = new DoctorWithCrmAndCrmUfSpecification(doctor.Crm, doctor.CrmUf);
            var doctors = await _unityOfWork.GetRepository<Doctor>().GetAllWithSpecAsync(spec);

            if(doctors.Any()) {
                return BadRequest(new ApiErrorResponse(400, "CRM já cadastrado"));
            }
            var doc = _mapper.Map<DoctorDTO, Doctor>(doctor);
            _unityOfWork.GetRepository<Doctor>().Add(doc);
            await _unityOfWork.Complete();
            
            return Ok(doctor);
        }

        [HttpPut]
        public async Task<ActionResult<DoctorDTO>> Update([FromBody] DoctorDTO doctor) {
            var docEntity = await _unityOfWork.GetRepository<Doctor>().GetByIdAsync(doctor.Id);
            var spec = new DoctorWithCrmAndCrmUfSpecification(doctor.Crm, doctor.CrmUf);
            var doctors = await _unityOfWork.GetRepository<Doctor>().GetEntityWithSpec(spec);

            if(docEntity == null) {
                return NotFound(new ApiErrorResponse(404, "Médico não cadastrado"));
            } 

            if(doctors != null && doctors.Id != docEntity.Id) {
                return BadRequest(new ApiErrorResponse(400, "CRM já cadastrado"));
            } 

            var doc = _mapper.Map<DoctorDTO, Doctor>(doctor);
            docEntity.Crm = doc.Crm;
            docEntity.Name = doc.Name;
            docEntity.Patients = doc.Patients;
            docEntity.CrmUf = doc.CrmUf;

            _unityOfWork.GetRepository<Doctor>().Update(docEntity);
            await _unityOfWork.Complete();
            
            return Ok(doctor);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id) {
            var spec = new DoctorWihPatientsSpecification(id);
            var doctor = await _unityOfWork.GetRepository<Doctor>().GetEntityWithSpec(spec);
            
            if(doctor == null) return NotFound(new ApiErrorResponse(404, "Médico não encontrado"));
            if(doctor.Patients.Any()) return BadRequest(new ApiErrorResponse(400, "Médico contém paciente"));

            _unityOfWork.GetRepository<Doctor>().Delete(doctor);
            await _unityOfWork.Complete();
            
            return Ok();
        }
    }
}