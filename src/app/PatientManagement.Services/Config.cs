using Microsoft.Extensions.DependencyInjection;
using PatientManagement.Services.Application.Patient;
using PatientManagement.Services.Contracts.Patient;

namespace PatientManagement.Services
{
    public static class Config
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            services.AddTransient<IPatientAppService, PatientAppService>();
            services.AddSingleton<PatientMapper>();
        }
    }
}
