using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PIM.Repositories;
using PIM.Repositories.Interfaces;
using PIM.Services;
using PIM.Services.Interfaces;
using PIM.Settings;

namespace PIM.Helpers
{
    public class ConfigureDI
    {
        public IConfiguration Configuration { get; }
        public ConfigureDI(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        internal void ConfigureDependencyInjections(IServiceCollection services)
        {
            var appSettings = Configuration.GetSection("AppSettings").Get<AppSettings>();
            appSettings.ConnectionString = Configuration.GetConnectionString("DefaultConnection");

            services.AddSingleton(appSettings);
            services.AddSingleton<IBrandService, BrandService>();
            services.AddSingleton<IProductService, ProductService>();
            services.AddSingleton<IUserService, UserService>();
            services.AddTransient<IBrandRepository, BrandRepository>();
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<IUserRepository, UserRepository>();

        }
    }
}
