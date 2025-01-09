using RealEstate.Business.Contracts;
using RealEstate.Business.Implement;
using RealEstate.Repository.Contracts;
using RealEstate.Repository.Implement;

namespace RealEstate.API
{
    public static class DependencyInjection
    {
        public static void ConfigureServices(this IServiceCollection services)
        {
            ConfigureRepositories(services);
            services.AddScoped<IOwnerService, OwnerService>();
            services.AddScoped<IPropertyService, PropertyService>();
            services.AddScoped<IPropertyFinanceService, PropertyFinanceService>();
        }


        private static void ConfigureRepositories(this IServiceCollection services)
        {
            services.AddScoped<IOwnerRepository, OwnerRepository>();
            services.AddScoped<IPropertyRepository, PropertyRepository>();
            services.AddScoped<IPropertyFinanceRepository, PropertyFinanceRepository>();
        }
    }
}
