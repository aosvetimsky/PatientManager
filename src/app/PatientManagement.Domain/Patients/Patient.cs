using System;
using PatientManagement.Domain.Common;

namespace PatientManagement.Domain.Patients
{
    public class Patient : Entity<Guid>
    {
        public string LastName { get; private set; }
        public string[] GivenNames { get; private set; }
        public string? Title { get; private set; }
        public Gender Gender { get; private set; } = Gender.Unknown;
        public DateTimeOffset BirthDate { get; private set; }
        public bool Active { get; private set; }

        public Patient(string lastName, string[] givenNames, string? title, Gender gender, DateTimeOffset birthDate, bool active)
        {
            LastName = lastName ?? throw new ArgumentNullException(nameof(lastName));
            GivenNames = givenNames;
            Title = title;
            Gender = gender;
            BirthDate = birthDate;
            Active = active;
        }

        public void UpdatePrimaryInfo(string lastName, string[] givenNames, string title, Gender gender, DateTimeOffset birthday)
        {
            LastName = lastName;
            GivenNames = givenNames;
            Title = title;
            Gender = gender;
            BirthDate = birthday;
        }
        public void SetActive(bool active)
        {
            Active = active;
        }
    }

    public enum Gender
    {
        Male,
        Female,
        Other,
        Unknown,
    }
}
