using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
using PatientManagement.Domain.Patients;
using PatientManagement.Services.Contracts.Patient;
using PatientManagement.Services.Common;

namespace PatientManagement.Services.Application.Patient
{
    internal class PatientAppService : IPatientAppService
    {
        private readonly IPatientRepository _patientRepository;
        private readonly IPatientReadOnlyRepository _patientReadOnlyRepository;
        private readonly PatientMapper _patientMapper;
        public PatientAppService(IPatientRepository patientRepository, IPatientReadOnlyRepository patientReadOnlyRepository, PatientMapper patientMapper)
        {
            _patientRepository = patientRepository;
            _patientReadOnlyRepository = patientReadOnlyRepository;
            _patientMapper = patientMapper;
        }

        public async Task<OperationResult<Guid>> CreatePatient(PatientDto patient)
        {
            var patientToCreate = new Domain.Patients.Patient(patient.LastName, patient.GivenNames, patient.Title, (Gender)patient.Gender, patient.BirthDate, patient.Active);
            await _patientRepository.AddAsync(patientToCreate);
            await _patientRepository.UnitOfWork.SaveEntitiesAsync();
            return OperationResult<Guid>.Succeeded(patientToCreate.Id);
        }
        public async Task<OperationResult> DeletePatient(Guid id)
        {
            var patient = await _patientRepository.FindAsync(id);

            if (patient == null)
            {
                return OperationResult.Failed(PatientErrors.PatientNotFoundError);
            }

            await _patientRepository.DeleteAsync(patient);
            await _patientRepository.UnitOfWork.SaveEntitiesAsync();
            return OperationResult.Succeeded();
        }
        public async Task<PatientDto?> FindPatient(Guid id)
        {
            var patient = await _patientRepository.FindAsync(id);

            if (patient == null)
            {
                return null;
            }

            return _patientMapper.ToDto(patient);
        }
        public async Task<OperationResult> UpdatePatient(PatientDto patient)
        {
            var patientToUpdate = await _patientRepository.FindAsync(patient.Id);

            if (patientToUpdate == null)
            {
                return OperationResult.Failed(PatientErrors.PatientNotFoundError);
            }

            patientToUpdate.UpdatePrimaryInfo(patient.LastName, patient.GivenNames, patient.Title, (Gender)patient.Gender, patient.BirthDate);

            await _patientRepository.UpdateAsync(patientToUpdate);
            await _patientRepository.UnitOfWork.SaveEntitiesAsync();
            return OperationResult.Succeeded();
        }
        public async Task<IReadOnlyList<PatientDto>> SearchPatients(int? skip, int? take, Hl7SearchDate? searchDate)
        {
            var query = _patientReadOnlyRepository.GetQueryable();

            if (skip.HasValue)
            {
                query = query.Skip(skip.Value);
            }

            if (take.HasValue)
            {
                query = query.Take(take.Value);
            }

            if (searchDate != null)
            {
                switch (searchDate.Prefix)
                {
                    case Hl7SearchDate.Hl7SearchDatePrefix.Eq:
                        query = query.Where(x => x.BirthDate == searchDate.Date);
                        break;
                    case Hl7SearchDate.Hl7SearchDatePrefix.Ne:
                        query = query.Where(x => x.BirthDate != searchDate.Date);
                        break;
                    case Hl7SearchDate.Hl7SearchDatePrefix.Lt:
                        query = query.Where(x => x.BirthDate <= searchDate.Date);
                        break;
                    case Hl7SearchDate.Hl7SearchDatePrefix.Gt:
                        query = query.Where(x => x.BirthDate >= searchDate.Date);
                        break;
                        // i'm too stupid to understand the other date behavior
                }
            }

            var patients = await query.ToListAsync();

            return patients.Select(_patientMapper.ToDto).ToImmutableList();
        }
    }
}
