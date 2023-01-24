using Credit.Services.Abstract;
using Credit.Services.Concrete;
using Microsoft.Extensions.DependencyInjection;

namespace Credit.Services.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection LoadMyServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IEqualCreditService, EqualCreditManager>();
            serviceCollection.AddScoped<IInterimPaymentCreditService, InterimPaymentCreditManager>();
            serviceCollection.AddScoped<IBallonCreditService, BallonCreditManager>();
            serviceCollection.AddScoped<IDecreasingCreditService, DecreasingCreditManager>();
            serviceCollection.AddScoped<IGrowingCreditService, GrowingCreditManager>();
            return serviceCollection;
        }
    }
}
