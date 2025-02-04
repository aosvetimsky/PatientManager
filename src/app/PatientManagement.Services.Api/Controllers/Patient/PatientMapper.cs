using System;
using PatientManagement.Services.Contracts.Patient;

namespace PatientManagement.Services.Api.Controllers.Patient
{
    public class PatientMapper
    {
        public Transport.Patient ToTransport(PatientDto e) => new Transport.Patient
        {
            Active = e.Active,
            BirthDate = e.BirthDate,
            Gender = e.Gender,
            Name = new Transport.Patient.PatientName
            {
                GivenNames = e.GivenNames,
                Id = e.Id,
                LastName = e.LastName,
                Title = e.Title,
            }
        };

        public PatientDto ToDto(Guid? id, Transport.Patient e) => new PatientDto
        {
            Active = e.Active,
            BirthDate = e.BirthDate,
            Gender = e.Gender,
            GivenNames = e.Name.GivenNames,
            Id = id ?? e.Name.Id,
            LastName = e.Name.LastName,
            Title = e.Name.Title
        };
    }
}
