using System;

namespace PatientManagement.Services.Contracts.Patient
{
    public class PatientDto
    {
        public Guid Id { get; set; }
        public string LastName { get; set; } = default!;
        public string[] GivenNames { get; set; } = default!;
        public string? Title { get; set; }
        public int Gender { get; set; }
        public DateTimeOffset BirthDate { get; set; }
        public bool Active { get; set; }
    }
}
