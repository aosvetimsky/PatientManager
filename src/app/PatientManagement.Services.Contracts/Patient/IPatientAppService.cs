using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PatientManagement.Services.Common;

namespace PatientManagement.Services.Contracts.Patient
{
    public interface IPatientAppService
    {
        public Task<OperationResult<Guid>> CreatePatient(PatientDto patient);
        public Task<OperationResult> UpdatePatient(PatientDto patient);
        public Task<OperationResult> DeletePatient(Guid id);
        public Task<PatientDto?> FindPatient(Guid id);
        public Task<IReadOnlyList<PatientDto>> SearchPatients(int? skip, int? take, Hl7SearchDate? searchDate);
    }
}
