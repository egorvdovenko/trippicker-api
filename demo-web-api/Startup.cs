using demo_web_api.Configuration;
using demo_web_api.Entities;
using demo_web_api.Interfaces;
using demo_web_api.Repositories;
using demo_web_api.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace demo_web_api
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
            //services.AddDbContext<DemoDbContext>(c => c.UseSqlite("Data Source=demo-app.db"));
            services.AddDbContext<DemoDbContext>(c =>
                c.UseSqlServer(Configuration.GetConnectionString("DemoDbContext")));

            services.AddScoped<ITeacherService, TeacherService>();
            services.AddScoped<IAccountService, AccountService>();

            services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();

            services.Configure<DemoWebApiConfiguration>(Configuration);
            var config = Configuration.Get<DemoWebApiConfiguration>();

            var key = Encoding.ASCII.GetBytes(config.SecurityKey);
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false
            };

            services.AddIdentity<UserEntity, IdentityRole<int>>(opts =>
                {
                    opts.Password.RequireNonAlphanumeric = false;
                    opts.Password.RequireUppercase = false;
                    opts.Password.RequireLowercase = false;
                    opts.Password.RequireDigit = false;
                    opts.Password.RequiredLength = 6;
                })
                .AddEntityFrameworkStores<DemoDbContext>()
                .AddDefaultTokenProviders();

            //todo как быть на секции (ServiceCollectionExtensions)
            //public static IServiceCollection ConfigureIdentity(this IServiceCollection services)
            //{
            //    services.AddIdentity<UserEntity, IdentityRole<int>>(opts =>
            //        {
            //            opts.Password.RequireNonAlphanumeric = false;
            //            opts.Password.RequireUppercase = false;
            //            opts.Password.RequireLowercase = false;
            //            opts.Password.RequireDigit = false;
            //            opts.Password.RequiredLength = 6;
            //        })
            //        .AddEntityFrameworkStores<InvestDbContext>()
            //        .AddDefaultTokenProviders();

            //    return services;
            //}

            //public static IServiceCollection ConfigureAuth(this IServiceCollection services, InvestConfiguration config)
            //{
            //    var key = Encoding.ASCII.GetBytes(config.SecurityKey);
            //    var tokenValidationParameters = new TokenValidationParameters
            //    {
            //        ValidateIssuerSigningKey = true,
            //        IssuerSigningKey = new SymmetricSecurityKey(key),
            //        ValidateIssuer = false,
            //        ValidateAudience = false
            //    };

            //    services.AddSingleton(tokenValidationParameters);
            //    services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            //        .AddJwtBearer(x =>
            //        {
            //            x.RequireHttpsMetadata = false;
            //            x.SaveToken = true;
            //            x.TokenValidationParameters = tokenValidationParameters;
            //        })
            //        .AddApiKey(c =>
            //        {
            //            c.ApiKey = config.ApiKey;
            //            c.SystemUserEmail = config.SystemUserEmail;
            //        });

            //    return services;
            //}

            services.AddSingleton(tokenValidationParameters);
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(x =>
                {
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = tokenValidationParameters;
                });

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "demo_web_api", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "demo_web_api v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
