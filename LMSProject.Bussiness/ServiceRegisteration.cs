using LMSProject.Bussiness.Bases;
using LMSProject.Data.Data;
using LMSProject.Data.Data.Enities.Identities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace LMSProject.Bussiness
{
    public static class ServiceRegisteration
    {
        public static IServiceCollection AddServiceRegisteration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddIdentity<User, Role>(option =>
            {
                // Password settings.
                option.Password.RequireDigit = true;
                option.Password.RequireLowercase = true;
                option.Password.RequireNonAlphanumeric = true;
                option.Password.RequireUppercase = true;
                option.Password.RequiredLength = 6;
                option.Password.RequiredUniqueChars = 1;

                // Lockout settings.
                option.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                option.Lockout.MaxFailedAccessAttempts = 5;
                option.Lockout.AllowedForNewUsers = true;

                // User settings.
                option.User.AllowedUserNameCharacters =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                option.User.RequireUniqueEmail = true;
                option.SignIn.RequireConfirmedEmail = true;
            }).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();

            var jwtSettings = new JwtSettings();
            configuration.GetSection(nameof(jwtSettings)).Bind(jwtSettings);
            services.AddSingleton(jwtSettings);

            var emailSettings = new EmailSettings();
            configuration.GetSection(nameof(emailSettings)).Bind(emailSettings);
            services.AddSingleton(emailSettings);


            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = jwtSettings.ValidateIssure,
                    ValidIssuers = new[] { jwtSettings.Issure },
                    ValidateIssuerSigningKey = jwtSettings.ValidateIssureSigningKey,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSettings.Secret)),
                    ValidateAudience = jwtSettings.ValidateAudience,
                    ValidAudience = jwtSettings.Audience,
                    ValidateLifetime = jwtSettings.ValidateLifetime
                };
            });


            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "LMS Project", Version = "v1" });
                c.EnableAnnotations();

                c.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
                {
                    Description = "JWT Authentication header using the Bearer scheme ()",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = JwtBearerDefaults.AuthenticationScheme
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                  {
                     new OpenApiSecurityScheme
                     {
                         Reference = new OpenApiReference
                         {
                         Type = ReferenceType.SecurityScheme,
                         Id = JwtBearerDefaults.AuthenticationScheme
                         }
                     },
                     Array.Empty<string>()
                  }
                });
            });

            services.AddAuthorization(opt =>
            {
                opt.AddPolicy("GetUsersList", policy =>
                {
                    policy.RequireClaim("Get Users List", "True");
                });

                opt.AddPolicy("GetUserById", policy =>
                {
                    policy.RequireClaim("Get User By Id", "True");
                });

            });
            return services;
        }
    }
}