using Microsoft.Extensions.DependencyInjection;
using PatientManagement.Domain.Patients;
using PatientManagement.Persistence.Repositories;

namespace PatientManagement.Persistence
{
    public static class Config
    {
        public static void AddPersistenceServices(this IServiceCollection services)
        {
            services.AddTransient<IPatientRepository, PatientRepository>();
            services.AddTransient<IPatientReadOnlyRepository, PatientRepository>();
        }
    }
}
