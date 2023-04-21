using BizCorp.Data;
using BizCorp.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NLog;
using System.Text;
using BizCorp.Logger;

namespace BizCorp
{
    public class Startup
    {
        private readonly string MyAllowSpecificOrigins = "BizCorpUIOrigin";

        public Startup(IWebHostEnvironment env)
        {
            string directory = string.Empty;
            if (env.EnvironmentName.Equals("Prod"))
            {
                directory = Directory.GetCurrentDirectory() + "/";
            }

            LogManager.LoadConfiguration(System.String.Concat(directory, "nlog.config"));
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.  
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.  

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
                    });

            var corsOrigins = Configuration.GetSection("BizCorpUI:origins").Get<string[]>();
            services.AddCors(options =>
            {
                options.AddPolicy(MyAllowSpecificOrigins,
                builder =>
                {
                    builder.AllowAnyOrigin()
                           .AllowAnyMethod()
                           .AllowAnyHeader()
                         ;
                });
            });

            services.AddScoped<DbContext, BizCorpContext>();
            services.AddControllers().AddNewtonsoftJson();

            services.AddDbContext<BizCorpContext>(options =>
            {
                options.UseMySql(Configuration.GetConnectionString("BizCorpDb"),
                    Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.29-mysql"));
            });

            //services.AddScoped<IRepository<User>, Repository<User>>();

            services.AddSingleton<ILog, LogNLog>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // app.UseAllElasticApm(Configuration);
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // This Need to be in order 
            app.UseRouting();  // First Use Routing
            app.UseCors(MyAllowSpecificOrigins);  // Then Enable Cors
            app.UseAuthentication(); // Then Use Authentication
            //app.UseAuthorization();  // Then Use Autherization
            // At the end Use Endpoints
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
