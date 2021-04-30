using System;
using System.Linq.Expressions;
using Core.Entities;

namespace Core.Specifications
{
  public class PatientByCpfSpecification : BaseSpecification<Patient>
  {
    public PatientByCpfSpecification(string cpf) : base(p => p.Cpf == cpf)
    {
    }
  }
}