using System.Collections.Generic;

namespace Core.Entities
{
    public class Doctor : BaseEntity
    {
        public string Name { get; set; }
        public string Crm { get; set; }
        public string CrmUf { get; set; }
        public ICollection<Patient> Patients { get; set; }
    }
}