using MarsRovers.Interfaces;
using MarsRovers.Services;
using Microsoft.Extensions.DependencyInjection;

namespace MarsRovers.DependencyResolution
{
    public static class ServicesRegistry
    {
        public static IServiceCollection Register(this IServiceCollection services)
        {
            return services
                .AddScoped<INavigationService, NavigationService>()
                .AddScoped<IRoverService, RoverService>();
        }
    }
}
