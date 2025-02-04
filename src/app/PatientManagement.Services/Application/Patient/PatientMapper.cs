using PatientManagement.Services.Contracts.Patient;

namespace PatientManagement.Services.Application.Patient
{
    public class PatientMapper
    {
        public PatientDto ToDto(Domain.Patients.Patient entry) => new PatientDto
        {
            Id = entry.Id,
            LastName = entry.LastName,
            GivenNames = entry.GivenNames,
            Title = entry.Title,
            Gender = (int)entry.Gender,
            BirthDate = entry.BirthDate,
            Active = entry.Active
        };
    }
}
