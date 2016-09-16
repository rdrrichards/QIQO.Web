using Core;
using Identity;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Serialization;
using QIQO.Business.Client.Contracts;
using QIQO.Business.Client.Entities;
using QIQO.Business.Client.Proxies;
using Core.Services;
using Microsoft.AspNet.Routing;
using Microsoft.AspNet.Http;

namespace QIQO.Web.Api
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            // Set up configuration sources.
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddCaching();
            services.AddCors();
            services.AddMvc().AddJsonOptions
                (
                    opt => { opt.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver(); }
                );

            services.AddIdentity<User, Role>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonLetterOrDigit = false;
                options.Password.RequireUppercase = true;
                options.User.AllowedUserNameCharacters = null;
                options.User.RequireUniqueEmail = true;
                //options.Lockout.AllowedForNewUsers = false;
                options.Lockout.MaxFailedAccessAttempts = 10;
                options.SignIn.RequireConfirmedEmail = false;
            })
                .AddUserStore<QIQOUserStore<User>>()
                .AddRoleStore<QIQORoleStore<Role>>()
                .AddUserManager<QIQOUserManager>()
                .AddRoleManager<QIQORoleManager>()
                .AddDefaultTokenProviders();

            services.AddInstance<IServiceFactory>(new ServiceFactory(services));
            services.AddTransient<IIdentityUserService, IdentityUserClient>();
            services.AddTransient<IIdentityRoleService, IdentityRoleClient>();
            services.AddTransient<IAccountService, AccountClient>();
            services.AddTransient<IAddressService, AddressClient>();
            services.AddTransient<ICompanyService, CompanyClient>();
            services.AddTransient<IEmployeeService, EmployeeClient>();
            services.AddTransient<IEntityProductService, EntityProductClient>();
            services.AddTransient<IFeeScheduleService, FeeScheduleClient>();
            services.AddTransient<IOrderService, OrderClient>();
            services.AddTransient<IInvoiceService, InvoiceClient>();
            services.AddTransient<IProductService, ProductClient>();
            services.AddTransient<ITypeService, TypeClient>();
            services.AddTransient<IEntityService, EntityService>();

            services.AddLogging();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseIISPlatformHandler();

            app.UseStaticFiles();

            app.UseIdentity();
            app.UseCookieAuthentication(options =>
            {
                //options.LoginPath = new PathString("/Account/Login");
                options.AutomaticAuthenticate = true;
                options.AutomaticChallenge = true;
                options.AuthenticationScheme = "Cookies";
                //options.AccessDeniedPath = new PathString("/Account/Forbidden");
            });

            app.UseMvc(ConfigureRoutes); //ConfigureRoutes
            app.UseCors(builder =>
                builder.WithOrigins("http://localhost")
           .AllowAnyHeader()
    );
        }

        private void ConfigureRoutes(IRouteBuilder routeBuilder)
        {
            routeBuilder.MapRoute("Default", "{controller=Home}/{action=Index}/{id?}");
            //routeBuilder.MapRoute("Product", "api/{controller}/{product_key?}", new { product_key = RouteP);
        }

        // Entry point for the application.
        public static void Main(string[] args) => WebApplication.Run<Startup>(args);
    }
}
