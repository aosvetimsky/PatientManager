using System;
using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;

namespace PatientManagement.Services.Api.Tests.Patient
{
    public class PatientControllerGETTests : PatientTestsBase
    {
        public PatientControllerGETTests(Setup.ApiWebApplicationFactory application) : base(application)
        {
        }

        [Fact]
        public async Task GET_not_existing_patient_returns_not_found()
        {
            var response = await _client.GetAsync($"/patient/{Guid.NewGuid()}");
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task GET_existing_patient_returns_patient()
        {
            var patient = await GivenPatient();

            var response = await _client.GetAsync($"/patient/{patient.Id}");
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var existingPatient = await response.Content.ReadFromJsonAsync<Controllers.Patient.Transport.Patient>();
            existingPatient.Should().NotBeNull();
            AssertPatientsEqual(patient, existingPatient);
        }
    }
}
