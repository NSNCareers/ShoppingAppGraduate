using Microsoft.Extensions.DependencyInjection;
using WebAPI.DAL;
using WebAPI.Data;
using WebAPI.DataContext;
using WebAPI.Token;

namespace WebAPI.Dependency
{
    public static class DependencyInjector
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection ConfigureShoppingCartServices(this IServiceCollection services)
        {
            services.AddTransient<IShoppingManager, ShoppingManager>();
            services.AddTransient<IShoppingCartData, ShoppingCartData>();
            services.AddTransient<IDataBaseChanges, DataBaseChanges>();
            services.AddTransient<IUserTokenGenerator, UserTokenGenerator>();
            return services;
        }
    }
}
