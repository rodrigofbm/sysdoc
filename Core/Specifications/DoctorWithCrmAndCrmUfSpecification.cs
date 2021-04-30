using Core.Entities;

namespace Core.Specifications
{
  public class DoctorWithCrmAndCrmUfSpecification : BaseSpecification<Doctor>
  {
    public DoctorWithCrmAndCrmUfSpecification(string crm, string crmUf) : base(d => d.Crm == crm && d.CrmUf == crmUf)
    {
    }
  }
}