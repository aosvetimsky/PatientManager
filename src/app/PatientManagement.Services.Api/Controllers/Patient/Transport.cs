using Microsoft.AspNetCore.Mvc;
using PatientManagement.Services.Common;
using System;
using System.ComponentModel.DataAnnotations;

namespace PatientManagement.Services.Api.Controllers.Patient.Transport
{
    public class Patient
    {
        public class PatientName
        {
            public Guid Id { get; set; }
            [Required]
            [StringLength(100)]
            public string LastName { get; set; } = default!;
            public string[] GivenNames { get; set; } = default!;
            [StringLength(10)]
            public string? Title { get; set; } = string.Empty;
        }

        public Patient()
        {
            Name = new PatientName();
        }

        public PatientName Name { get; set; } = default!;

        [Range(0, 3)]
        public int Gender { get; set; }
        [Required]
        public DateTimeOffset BirthDate { get; set; }
        public bool Active { get; set; }
    }

    public class SearchPatientRequest
    {
        public int? Skip { get; set; }
        public int? Take { get; set; }
        [BindProperty, DataType(nameof(Hl7SearchDate))]
        public Hl7SearchDate? SearchDate { get; set; }
    }
}
