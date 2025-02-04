using System;
using PatientManagement.Domain.Common;

namespace PatientManagement.Domain.Patients
{
    public interface IPatientReadOnlyRepository : IReadOnlyRepository<Patient, Guid>
    {
    }
}
