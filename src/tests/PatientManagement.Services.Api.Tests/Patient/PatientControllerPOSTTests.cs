using System;
using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;

namespace PatientManagement.Services.Api.Tests.Patient
{
    public class PatientControllerPOSTTests : PatientTestsBase
    {
        public PatientControllerPOSTTests(Setup.ApiWebApplicationFactory application) :base(application)
        {
        }

        [Fact]
        public async Task it_returns_bad_request_for_missing_required_fields()
        {
            var response = await _client.PostAsJsonAsync("/patient", new Controllers.Patient.Transport.Patient
            {
            });
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task it_creates_patient_and_returns_created_patient()
        {
            var patient = new Controllers.Patient.Transport.Patient
            {
                Active = true,
                BirthDate = DateTimeOffset.UtcNow.AddDays(-5),
                Gender = 1,
                Name = new Controllers.Patient.Transport.Patient.PatientName
                {
                    GivenNames = new string[] {"John"},
                    LastName = "Smith",
                    Title = "Mr"
                }
            };
            var response = await _client.PostAsJsonAsync("/patient", patient);
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var createdPatient = await response.Content.ReadFromJsonAsync<Controllers.Patient.Transport.Patient>();
            createdPatient.Should().NotBeNull();
            AssertPatientsEqual(patient, createdPatient);
        }
    }
}
