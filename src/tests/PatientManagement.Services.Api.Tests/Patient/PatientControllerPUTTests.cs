using System;
using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;

namespace PatientManagement.Services.Api.Tests.Patient
{
    public class PatientControllerPUTTests : PatientTestsBase
    {
        public PatientControllerPUTTests(Setup.ApiWebApplicationFactory application) :base(application)
        {
        }

        [Fact]
        public async Task it_returns_bad_request_for_missing_required_fields()
        {
            var response = await _client.PutAsJsonAsync($"/patient/{Guid.NewGuid()}", new Controllers.Patient.Transport.Patient
            {
            });
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task it_updates_existing_patient_and_returns_updated_patient()
        {
            var patient = await GivenPatient();

            var transport = new Controllers.Patient.Transport.Patient
            {
                Active = patient.Active,
                BirthDate = patient.BirthDate.AddDays(-2),
                Gender = 1,
                Name = new Controllers.Patient.Transport.Patient.PatientName
                {
                    GivenNames = patient.GivenNames,
                    LastName = "Lennon",
                    Title = patient.Title
                }
            };
            var response = await _client.PutAsJsonAsync($"/patient/{patient.Id}", transport);
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var updatedPatient = await response.Content.ReadFromJsonAsync<Controllers.Patient.Transport.Patient>();
            updatedPatient.Should().NotBeNull();
            AssertPatientsEqual(transport, updatedPatient);
        }
    }
}
