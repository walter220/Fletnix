using System.Net;
using System.Threading.Tasks;
using cloudscribe.Web.Pagination;
using Microsoft.AspNet.Authentication.Cookies;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.PlatformAbstractions;
using Newtonsoft.Json.Serialization;
using TheWorld.Models;
using TheWorld.Models.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace TheWorld
{
    public class Startup
    {
        public static IConfigurationRoot Configuration;

        public Startup(IApplicationEnvironment appEnv)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(appEnv.ApplicationBasePath)
                .AddJsonFile("config.json")
                .AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // Hierin gebeurd allerlei dependency injection. 
        // Wat veel voorgekomen is, is dat NuGet packages hier aan de service toegevoegd worden.
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(config =>
            {
#if !DEBUG
            // Zorgt ervoor dat alles wat we versturen over https gaat
              config.Filters.Add(new RequireHttpsAttribute());
#endif
            }).AddJsonOptions(opt =>
            {
                opt.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            });

            services.AddAntiforgery();

            // Adds Identity to the service
            // Setting up the user requirements
            services.AddIdentity<FletnixUser, IdentityRole>(config =>
            {
                config.User.RequireUniqueEmail = true;
                config.Password.RequiredLength = 8;
                config.Cookies.ApplicationCookie.LoginPath = "/Auth/Login"; //Redirect path
                // Mogelijk maken om het standaard gedrag aan te passen
                config.Cookies.ApplicationCookie.Events = new CookieAuthenticationEvents()
                {
                    // ctx staat voor context
                    OnRedirectToLogin = ctx =>
                    {
                        // Voorkomt dat het beveiligde api gedeelte de login page teruggeeft
                        // We geven daarom een unauthorized error terug
                        if (ctx.Request.Path.StartsWithSegments("/api") &&
                            ctx.Response.StatusCode == (int)HttpStatusCode.OK)
                        {
                            ctx.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                        }
                        else
                        {
                            ctx.Response.Redirect(ctx.RedirectUri);
                        }
                        return Task.FromResult(0);
                    }
                };
            })
            // Verteld waar de Identity in wordt opgeslagen
            .AddEntityFrameworkStores<FletnixContext>(); //why?

            services.AddLogging();

            services.AddEntityFramework()
                .AddSqlServer()
                .AddDbContext<FletnixContext>();

            // Add Transient zorgt ervoor dat je altijd een nieuwe instantie krijgt
            services.AddTransient<FletnixContextSeedData>();

            // Add Scoped zorgt ervoor dat de instantie de gehele tijd van een request wordt hergebruikt elke keer dat iedereen dit nodig heeft
            services.AddScoped<IMovieRepository, MovieRepository>();
            services.AddScoped<IGenreRepository, GenreRepository>();

            services.AddCaching();

            services.AddTransient<IBuildPaginationLinks, PaginationLinkBuilder>();
        }

        // Order matters!
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        // Seeder is het object dat wordt geinject. Op het einde wordt dit gebruikt
        public async void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory, FletnixContextSeedData seeder)
        {
            //Without this, htmls will fail
            app.UseIISPlatformHandler();
#if DEBUG
            app.UseDeveloperExceptionPage();
            loggerFactory.AddDebug(LogLevel.Information);
#endif
            // Kijk of het een static file is
            app.UseStaticFiles();

            // Kijk of we Identity moeten gebruiken
            app.UseIdentity();



            // Gebruik MVC, meestal als laatst gebruikt
            app.UseMvc(config =>
            {
                config.MapRoute(
                    name: "Default",
                    template: "{controller}/{action}/{id?}",
                    defaults: new {controller = "App", Action = "Index"}
                    );
            });

//            await seeder.EnsureSeedDataAsync();
        }

        // Entry point for the application.
        public static void Main(string[] args) => WebApplication.Run<Startup>(args);
    }
}
