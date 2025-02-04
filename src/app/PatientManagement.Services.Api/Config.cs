using Microsoft.Extensions.DependencyInjection;
using PatientManagement.Services.Api.Controllers.Patient;

namespace PatientManagement.Services.Api
{
    public static class Config
    {
        public static void AddApiServices(this IServiceCollection services)
        {
            services.AddSingleton<PatientMapper>();
        }
    }
}
