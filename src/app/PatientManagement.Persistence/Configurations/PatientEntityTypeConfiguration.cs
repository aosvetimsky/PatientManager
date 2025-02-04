using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PatientManagement.Domain.Patients;

namespace PatientManagement.Persistence.Configurations;

class PatientEntityTypeConfiguration : IEntityTypeConfiguration<Patient>
{
    public void Configure(EntityTypeBuilder<Patient> patientConfiguration)
    {
        patientConfiguration.ToTable("Patients");

        patientConfiguration.HasKey(x => x.Id);

        patientConfiguration.Property(x => x.LastName).IsRequired().HasMaxLength(100);
        patientConfiguration.Property(x => x.GivenNames);
        patientConfiguration.Property(x => x.Title).HasMaxLength(10);
        patientConfiguration.Property(x => x.BirthDate).IsRequired();
        patientConfiguration.Property(x => x.Gender);
        patientConfiguration.Property(x => x.Active).IsRequired();
    }
}
