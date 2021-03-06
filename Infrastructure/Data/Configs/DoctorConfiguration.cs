using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configs
{
  public class DoctorConfiguration : IEntityTypeConfiguration<Doctor>
  {
    public void Configure(EntityTypeBuilder<Doctor> builder)
    {
      builder.Property(p => p.Id).IsRequired();
      builder.Property(p => p.Name).IsRequired();
      builder.Property(p => p.Crm).IsRequired();
      builder.Property(p => p.CrmUf).HasMaxLength(2).IsRequired();
    }
  }
}