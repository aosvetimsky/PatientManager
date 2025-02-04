using System;
using PatientManagement.Domain.Common;

namespace PatientManagement.Domain.Patients
{
    public interface IPatientRepository : IRepository<Patient, Guid>
    {
    }
}
