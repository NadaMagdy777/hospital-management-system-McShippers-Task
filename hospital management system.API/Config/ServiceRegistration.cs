using hospital__management_system.Core.Interfaces.Services;
using hospital__management_system.Core.Interfaces;
using hospital__management_system.EF.Services;
using hospital__management_system.EF;
using hospital__management_system.Core.MappingProfiles;

namespace hospital_management_system.API.Config
{
    public static class ServiceRegistration
    {
        public static IServiceCollection RegisterServices(this IServiceCollection provider)
        {
            provider.AddAutoMapper(typeof(ApplicationUserProfile).Assembly);

            provider.AddScoped<IUnitOfWork, UnitOfWork>();

            provider.AddScoped<ITokenGenerator, TokenGenerator>();

            provider.AddScoped<IAccountService, AccountService>();

            provider.AddScoped<IPatientService, PatientService>();

            provider.AddScoped<IPatientAppointmentManageService, PatientAppointmentManageService>();
            provider.AddScoped<IEmailService, EmailService>();

            return provider;

        }
    }
}
