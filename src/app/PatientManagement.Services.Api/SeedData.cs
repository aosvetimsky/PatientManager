using System;
using System.Linq;
using System.Threading.Tasks;
using PatientManagement.Persistence;
using PatientManagement.Services.Extensions;

namespace PatientManagement.Services.Api
{
    internal class SeedData
    {
        private static readonly string[] FirstNames =
        {
            "Alex", "Tom", "Edvard", "Darius", "John", "Tomas", "Garry", "Sara", "Tanya", "Adam", "Vera", "Katty"
        };

        private static readonly string[] LastNames =
        {
            "Smith", "Adams", "Chaze", "Dorn", "Mizu", "Scoffield", "Barroos", "Bellick", "Tomos", "Gonsaliz", "Pilanio", "Yerch"
        };

        private static readonly string[] Titles =
        {
            "Mr", "Mrs", "Sir"
        };

        private static readonly Domain.Patients.Gender[] Genders =
        {
            Domain.Patients.Gender.Unknown, Domain.Patients.Gender.Male, Domain.Patients.Gender.Female, Domain.Patients.Gender.Other
        };

        public static async Task SeedPatients(AppDbContext ctx)
        {
            if (!ctx.Patients.Any())
            {
                for (int i = 0; i < 100; i++)
                {
                    await ctx.Patients.AddAsync(new Domain.Patients.Patient(
                        lastName: LastNames.Random(),
                        givenNames: new string[] { FirstNames.Random() },
                        title: Titles.Random(),
                        gender: Genders.Random(),
                        birthDate: DateTimeOffset.UtcNow.AddDays(new Random().Next(0, 20)),
                        active: true
                    ));
                }

                await ctx.SaveChangesAsync();
            }
        }
    }
}
