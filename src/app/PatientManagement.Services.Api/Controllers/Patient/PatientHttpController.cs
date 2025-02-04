using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PatientManagement.Services.Common;
using PatientManagement.Services.Contracts.Patient;
using PatientManager.Services.Api.Controllers;

namespace PatientManagement.Services.Api.Controllers.Patient
{
    [Route("patient")]
    public class PatientController: ApiControllerBase
    {
        private readonly IPatientAppService _patientAppService;
        private readonly PatientMapper _patientMapper;
        public PatientController(IPatientAppService patientAppService, PatientMapper patientMapper)
        {
            _patientAppService = patientAppService;
            _patientMapper = patientMapper;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IResult> Get(Guid id)
        {
            var patient = await _patientAppService.FindPatient(id);

            if (patient == null)
            {
                return Results.NotFound();
            }

            return Results.Ok(_patientMapper.ToTransport(patient));
        }

        [HttpPost]
        public async Task<IResult> Create(Transport.Patient patient)
        {
            var patientDto = _patientMapper.ToDto(null, patient);
            var result = await _patientAppService.CreatePatient(patientDto);

            if (!result.IsSucceeded)
            {
                return HandleOperationInvalidResult(result.Error);
            }

            patientDto = await _patientAppService.FindPatient(result.Result);
            return Results.Ok(_patientMapper.ToTransport(patientDto));
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IResult> Update(Guid id, Transport.Patient patient)
        {
            var existing = await _patientAppService.FindPatient(id);

            if (existing == null)
            {
                return Results.NotFound();
            }

            var result = await _patientAppService.UpdatePatient(_patientMapper.ToDto(id, patient));

            if (!result.IsSucceeded)
            {
                return HandleOperationInvalidResult(result.Error);
            }

            var updated = await _patientAppService.FindPatient(id);

            return Results.Ok(_patientMapper.ToTransport(updated));
        }

        [HttpDelete("{id}")]
        public async Task<IResult> Delete(Guid id)
        {
            var patient = await _patientAppService.FindPatient(id);

            if (patient == null)
            {
                return Results.NotFound();
            }

            var result = await _patientAppService.DeletePatient(id);

            if (!result.IsSucceeded)
            {
                return HandleOperationInvalidResult(result.Error);
            }
            
            return Results.Ok();
        }

        [HttpGet("search")]
        public async Task<IResult> Search(int? skip, int? take, string? hl7Date)
        {
            //maybe it should be moved to JsonTypeConverter and somehow handle wrong request
            Hl7SearchDate hl7SearchDate = default;
            if (!string.IsNullOrEmpty(hl7Date))
            {
                if (!Hl7SearchDate.TryCreateFromString(hl7Date, out hl7SearchDate))
                {
                    return Results.BadRequest("Invalid hl7SearchDate");
                }
            }

            var patients = await _patientAppService.SearchPatients(skip, take, hl7SearchDate);

            return Results.Ok(patients.Select(_patientMapper.ToTransport).ToArray());
        }
    }
}
