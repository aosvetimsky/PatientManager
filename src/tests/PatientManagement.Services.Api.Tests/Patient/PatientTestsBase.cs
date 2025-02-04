using System;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using PatientManagement.Persistence;
using Xunit;

namespace PatientManagement.Services.Api.Tests.Patient
{
    public abstract class PatientTestsBase : IClassFixture<Setup.ApiWebApplicationFactory>
    {
        protected readonly HttpClient _client;
        protected readonly Setup.ApiWebApplicationFactory _application;
        protected readonly AppDbContext _db;

        public PatientTestsBase(Setup.ApiWebApplicationFactory application)
        {
            _application = application;
            _client = application.CreateClient();
            _db = application.Db;
        }

        protected async Task<Domain.Patients.Patient> GivenPatient(string? lastName = "Smith", DateTimeOffset? birthdate = default, Action<Domain.Patients.Patient> action = default)
        {
            var patient = new Domain.Patients.Patient(lastName, new string[] {"John" }, "Mr", Domain.Patients.Gender.Male, birthdate ?? DateTimeOffset.UtcNow.AddDays(-5), true);

            if (action != null)
            {
                action(patient);
            }

            await _db.Patients.AddAsync(patient);
            await _db.SaveChangesAsync();
            return patient;
        }

        protected void AssertPatientsEqual(Domain.Patients.Patient domain, Controllers.Patient.Transport.Patient transport)
        {
            domain.Active.Should().Be(transport.Active);
            domain.BirthDate.Should().Be(transport.BirthDate);
            domain.LastName.Should().Be(transport.Name.LastName);
            domain.Gender.Should().Be((Domain.Patients.Gender)transport.Gender);
            domain.Title.Should().Be(transport.Name?.Title);
        }

        protected void AssertPatientsEqual(Controllers.Patient.Transport.Patient first, Controllers.Patient.Transport.Patient second)
        {
            first.Active.Should().Be(second.Active);
            first.BirthDate.Should().Be(second.BirthDate);
            first.Gender.Should().Be(second.Gender);
            first.Name.LastName.Should().Be(second.Name.LastName);
            first.Name.Title.Should().Be(second.Name.Title);
            first.Name.GivenNames.Should().BeSubsetOf(second.Name.GivenNames);
        }
    }
}
