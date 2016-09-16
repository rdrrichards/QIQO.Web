using Core;
using Identity;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Routing;
using Microsoft.Extensions.DependencyInjection;
using Core.Services;
using Newtonsoft.Json.Serialization;
using QIQO.Business.Client.Contracts;
using QIQO.Business.Client.Entities;
using QIQO.Business.Client.Proxies;
using Microsoft.Extensions.Logging;

namespace QIQO.Web.Mvc
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
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
            services.AddTransient<IProductService, ProductClient>();
            services.AddTransient<ITypeService, TypeClient>();
            services.AddTransient<IEntityService, EntityService>();

            services.AddLogging();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment environment, ILoggerFactory log_factory)
        {
            app.UseIISPlatformHandler();

            if (environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                log_factory.AddDebug(LogLevel.Debug);
            }
            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseIdentity();
            app.UseCookieAuthentication(options =>
            {
                options.LoginPath = new PathString("/Account/Login");
                options.AutomaticAuthenticate = true;
                options.AutomaticChallenge = true;
                options.AuthenticationScheme = "Cookies";
                options.AccessDeniedPath = new PathString("/Account/Forbidden");
            });

            app.UseMvc(ConfigureRoutes);
            app.UseCors("AllowFromAll");

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Hello World!");
            });
        }

        private void ConfigureRoutes(IRouteBuilder routeBuilder)
        {
            routeBuilder.MapRoute("Default", "{controller=Home}/{action=Index}/{id?}");
        }

        // Entry point for the application.
        public static void Main(string[] args) => WebApplication.Run<Startup>(args);
    }
}
