using Microsoft.AspNet.Authentication.Cookies;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Identity.EntityFramework;
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

            services.AddIdentity<WorldUser, IdentityRole>(config => {
                config.User.RequireUniqueEmail = true;
                config.Password.RequiredLength = 8;

            }).AddEntityFrameworkStores<WorldContext>();

            services.Configure<CookieAuthenticationOptions>(config =>
            {
                config.LoginPath = "/Auth/Login";

            });
            //if (enviroment.IsDevelopment())
            //{
            //   
            //}



        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public async void Configure(IApplicationBuilder app, WorldContextSeedData seeder,ILoggerFactory loggerFactory)
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
            app.UseIdentity();


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
           await seeder.EnsureSeedDataAsync();

        }

        // Entry point for the application.
        public static void Main(string[] args) => WebApplication.Run<Startup>(args);
    }
}
