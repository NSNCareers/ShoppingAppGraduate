using Microsoft.Extensions.DependencyInjection;
using WebAPI.DAL;
using WebAPI.Data;
using WebAPI.DataContext;

namespace WebAPI.Dependency
{
    public static class DependencyInjector
    {
        public static IServiceCollection ConfigureShoppingCartServices(this IServiceCollection services)
        {
            services.AddTransient<IShoppingManager, ShoppingManager>();
            services.AddTransient<IShoppingCartData, ShoppingCartData>();
            services.AddTransient<IDataBaseChanges, DataBaseChanges>();
            return services;
        }
    }
}
