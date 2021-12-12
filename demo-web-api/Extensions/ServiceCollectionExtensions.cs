//using AutoMapper;
//using FluentValidation.AspNetCore;
//using Invest.Core.Configuration;
//using Invest.Core.Entities;
//using Invest.Database;
//using Invest.Shared.Extensions;
//using Invest.Shared.Validation;
//using Microsoft.AspNetCore.Authentication.JwtBearer;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.Extensions.DependencyInjection;
//using Microsoft.IdentityModel.Tokens;
//using Microsoft.OpenApi.Models;
//using Swashbuckle.AspNetCore.Swagger;
//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Reflection;
//using System.Text;
//using Unchase.Swashbuckle.AspNetCore.Extensions.Extensions;

//namespace Invest.Admin.Api.Extensions
//{
//    public static class ServiceCollectionExtensions
//    {
//        public static IServiceCollection ConfigureSwagger(this IServiceCollection services)
//        {
//            services.AddSwaggerGen(c =>
//            {
//                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Invest.Admin.Api", Version = "v1" });
//                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
//                {
//                    Name = "Authorization",
//                    In = ParameterLocation.Header,
//                    Type = SecuritySchemeType.ApiKey,
//                    Scheme = "Bearer"
//                });

//                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
//                {
//                    {
//                        new OpenApiSecurityScheme
//                        {
//                            Reference = new OpenApiReference
//                            {
//                                Type = ReferenceType.SecurityScheme,
//                                Id = "Bearer"
//                            },
//                            Scheme = "Bearer",
//                            In = ParameterLocation.Header
//                        },
//                        new List<string>()
//                    }
//                });

//                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
//                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
//                c.IncludeXmlComments(Path.Combine($@"{AppDomain.CurrentDomain.BaseDirectory}", "Invest.Admin.Services.xml"));
//                c.IncludeXmlComments(xmlPath);
//                c.AddEnumsWithValuesFixFilters();
//                c.EnableAnnotations();
//                c.AddFluentValidationRules();
//            });

//            return services;
//        }

//        public static IServiceCollection ConfigureAuth(this IServiceCollection services, InvestConfiguration config)
//        {
//            var key = Encoding.ASCII.GetBytes(config.SecurityKey);
//            var tokenValidationParameters = new TokenValidationParameters
//            {
//                ValidateIssuerSigningKey = true,
//                IssuerSigningKey = new SymmetricSecurityKey(key),
//                ValidateIssuer = false,
//                ValidateAudience = false
//            };

//            services.AddSingleton(tokenValidationParameters);
//            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//                .AddJwtBearer(x =>
//                {
//                    x.RequireHttpsMetadata = false;
//                    x.SaveToken = true;
//                    x.TokenValidationParameters = tokenValidationParameters;
//                })
//                .AddApiKey(c =>
//                {
//                    c.ApiKey = config.ApiKey;
//                    c.SystemUserEmail = config.SystemUserEmail;
//                });

//            return services;
//        }

//        public static IServiceCollection ConfigureIdentity(this IServiceCollection services)
//        {
//            services.AddIdentity<UserEntity, IdentityRole<int>>(opts =>
//            {
//                opts.Password.RequireNonAlphanumeric = false;
//                opts.Password.RequireUppercase = false;
//                opts.Password.RequireLowercase = false;
//                opts.Password.RequireDigit = false;
//                opts.Password.RequiredLength = 6;
//            })
//            .AddEntityFrameworkStores<InvestDbContext>()
//            .AddDefaultTokenProviders();

//            return services;
//        }

//        public static IServiceCollection ConfigureAutomapping(this IServiceCollection services)
//        {
//            var mappingAssemblies = new[]
//            {
//                typeof(Mappings.ApiMappingProfile).Assembly,
//                typeof(DataAccess.Mappings.UsersMappingProfile).Assembly
//            };

//            services.AddAutoMapper(mappingAssemblies);

//            return services;
//        }

//        public static IServiceCollection ConfigureValidation(this IServiceCollection services)
//        {
//            services.AddControllers().AddFluentValidation(c =>
//            {
//                c.RegisterValidatorsFromAssemblyContaining<Startup>();
//            });

//            services.Configure<ApiBehaviorOptions>(options =>
//            {
//                options.InvalidModelStateResponseFactory = ctx => new ValidationProblemDetailsResult();
//            });

//            return services;
//        }
//    }
//}
