using System;
using System.Linq.Expressions;
using Core.Entities;

namespace Core.Specifications
{
  public class DoctorWihPatientsSpecification : BaseSpecification<Entities.Doctor>
  {
    public DoctorWihPatientsSpecification()
    {
        AddIncludes(d => d.Patients);
    }

    public DoctorWihPatientsSpecification(Guid id) : base(d => d.Id == id)
    {
        AddIncludes(d => d.Patients);
    }
  }
}