using System;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Xunit;

namespace PatientManagement.Services.Api.Tests.Patient
{
    public class PatientControllerSEARCHTests : PatientTestsBase
    {
        public PatientControllerSEARCHTests(Setup.ApiWebApplicationFactory application) :base(application)
        {
        }

        [Fact]
        public async Task it_searches_given_patients()
        {
            foreach(var index in Enumerable.Range(0, 20))
            {
                await GivenPatient(birthdate: DateTimeOffset.UtcNow.AddDays(-index));
            }

            var response = await _client.GetAsync($"/patient/search?skip=0&take=10");
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var patients = await response.Content.ReadFromJsonAsync<Controllers.Patient.Transport.Patient[]>();
            patients.Should().NotBeNull();
            patients.Length.Should().Be(10);
        }

        [Fact]
        public async Task it_searches_patients_by_eq_date()
        {
            var patient = await GivenPatient(birthdate: DateTimeOffset.UtcNow.AddDays(-1));
            await GivenPatient(birthdate: DateTimeOffset.UtcNow.AddDays(-2));
            await GivenPatient(birthdate: DateTimeOffset.UtcNow.AddDays(-3));

            var response = await _client.GetAsync($"/patient/search?skip=0&take=10&hl7Date=eq{patient.BirthDate.ToString("s")}");
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var patients = await response.Content.ReadFromJsonAsync<Controllers.Patient.Transport.Patient[]>();
            patients.Should().NotBeNull();
            patients.Length.Should().Be(1);
            patients[0].Name.Id.Should().Be(patient.Id);
        }
    }
}
