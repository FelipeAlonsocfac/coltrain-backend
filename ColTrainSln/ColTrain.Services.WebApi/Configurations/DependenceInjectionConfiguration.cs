using Microsoft.Extensions.DependencyInjection;

using ColTrain.Services.Contracts.Interfaces.Services;
using ColTrain.Services.Core.Services;
using ColTrain.Shared.Contracts.Interfaces.Repositories;
using ColTrain.Shared.Infrastructure.Repositories;

namespace ColTrain.Services.WebApi.Configurations
{
    public static class DependenceInjectionConfiguration
    {
        public static IServiceCollection AddDependenceInjectionConfiguration(this IServiceCollection services)
        {
            #region Repositories
            services.AddScoped<ICityRepository, CityRepository>();
            services.AddScoped<IStateRepository, StateRepository>();
            #endregion End Repositories

            #region Services
            services.AddScoped<ICityService, CityService>();
            services.AddScoped<IStateService, StateService>();
            #endregion End Services

            #region Validators

            #endregion Validators


            return services;
        }
    }
}
