using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.PlatformAbstractions;
using Newtonsoft.Json.Serialization;
using TheWorldPracticeAppASPNET5.Models;
using TheWorldPracticeAppASPNET5.Services;
using TheWorldPracticeAppASPNET5.ViewModels;

namespace TheWorldPracticeAppASPNET5
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public static IConfigurationRoot Configuration;
        public Startup(IApplicationEnvironment appEnv)
        {
            var builder = new ConfigurationBuilder().SetBasePath(appEnv.ApplicationBasePath).AddEnvironmentVariables().AddJsonFile("config.json");
            Configuration = builder.Build();
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().AddJsonOptions(option => 
            {
                option.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            });
            services.AddLogging();
            services.AddEntityFramework().AddSqlServer().AddDbContext<WorldContext>();
            services.AddTransient<WorldContextSeedData>();
            services.AddScoped<CoordService>();
            services.AddScoped<IWorldRepository,WorldRepository>();
            services.AddScoped<ImailService, DebugMailService>();
            //if (enviroment.IsDevelopment())
            //{
            //   
            //}



        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, WorldContextSeedData seeder,ILoggerFactory loggerFactory)
        {
            // app.UseIISPlatformHandler();
            //  app.UseStaticFiles();
            //app.Run(async (context) =>
            //{
            //    await context.Response.WriteAsync("Hello World!");
            //});
            //app.UseDefaultFiles();
            loggerFactory.AddDebug(LogLevel.Warning);
            app.UseIISPlatformHandler();
            app.UseStaticFiles();

            AutoMapper.Mapper.Initialize(config =>
            {

                config.CreateMap<Trip, TripViewModel>().ReverseMap();
                config.CreateMap<Stop, StopViewModel>().ReverseMap();
            });


            app.UseMvc(config => {
                config.MapRoute(
                    name: "Default",
                    template: "{controller}/{action}/{id?}",
                    defaults: new {controller="App",action = "Index" }

                    );

            });
            seeder.EnsureSeedData();

        }

        // Entry point for the application.
        public static void Main(string[] args) => WebApplication.Run<Startup>(args);
    }
}
