using System.Linq;
using System.Text;
using ACIC.AMS.DataStore;
using ACIC.AMS.DataStore.Interfaces;
using ACIC.AMS.Repository;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace ACIC.AMS
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            Environment = env;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Environment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = Configuration["Jwt:Issuer"],
                        ValidAudience = Configuration["Jwt:Issuer"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
                    };
                })
                .AddCookie();

            services.AddAuthentication();
            services.AddAuthorization();

            services.AddMvc()
              .AddSessionStateTempDataProvider()
              .AddNewtonsoftJson(options =>
                  options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
              );

            services.AddAutoMapper(typeof(Startup));
            services.AddDbContextPool<ACICDBContext>(o =>
            {
                o.UseSqlServer(Configuration["ConnectionStrings:DefaultConnection"]);
            });

            services.AddTransient<IUserDataStore, UserDataStore>();
            services.AddTransient<IAccountDataStore, AccountDataStore>();
            services.AddTransient<IContactDataStore, ContactDataStore>();
            services.AddTransient<IAgentDataStore, AgentDataStore>();
            services.AddTransient<IStateDataStore, StateDataStore>();
            services.AddTransient<IDriverDataStore, DriverDataStore>();
            services.AddTransient<IEndorsementDataStore, EndorsementDataStore>();
            services.AddTransient<IPolicyDataStore, PolicyDataStore>();
            services.AddTransient<IVehicleDataStore, VehicleDataStore>();
            services.AddTransient<IBankDataStore, BankDataStore>();
            services.AddTransient<ICarrierDataStore, CarrierDataStore>();
            services.AddTransient<IMgaDataStore, MgaDataStore>();
            services.AddTransient<ICommissionsDataStore, CommissionsDataStore>();
            services.AddScoped<IMapper, Mapper>();

            services.AddSwaggerGen();
            services.AddSwaggerGen(c =>
            {
                c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First()); //This line
            });


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseCors("AllowAll");
            app.UseDeveloperExceptionPage();



            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Login}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                   name: "user",
                   pattern: "{controller=User}/{action=Profile}/{id?}");

            });


            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "ACIC API");
            });


        }
    }
}
