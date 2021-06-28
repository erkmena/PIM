using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using PIM.Filters;
using PIM.Helpers;
using PIM.Settings;
using System.IO.Compression;

namespace PIM
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            var appSettings = Configuration.GetSection("AppSettings").Get<AppSettings>();

            services.AddControllers(o =>
            {
                o.InputFormatters.RemoveType<XmlDataContractSerializerInputFormatter>();
                o.InputFormatters.RemoveType<XmlSerializerInputFormatter>();
                o.OutputFormatters.RemoveType<HttpNoContentOutputFormatter>();
                o.OutputFormatters.RemoveType<StreamOutputFormatter>();
                o.OutputFormatters.RemoveType<StringOutputFormatter>();
                o.OutputFormatters.RemoveType<XmlDataContractSerializerOutputFormatter>();
                o.OutputFormatters.RemoveType<XmlSerializerOutputFormatter>();

                o.Filters.Add<GlobalExceptionFilter>();
            });
            services.Configure<GzipCompressionProviderOptions>(options => options.Level = CompressionLevel.Optimal);
            services.AddResponseCompression();
            services.AddLogging(ctx => ctx.AddConsole());

            services.AddMemoryCache(o =>
            {
                //Added for In-Memory cache, so it doesn't grow forever
                o.SizeLimit = appSettings.CacheSizeLimit;
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "PIM.API", Version = "v1" });
            });

            (new ConfigureDI(Configuration)).ConfigureDependencyInjections(services);//Add DI

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "PIM.API v1"));
            }

            app.UseResponseCompression();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
