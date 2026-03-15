using GranaFlow.Application.Auth;
using GranaFlow.Application.Services;
using GranaFlow.Application.Services.Interfaces;
using GranaFlow.Application.Utils;
using GranaFlow.Domain.Interfaces.Repositories;
using GranaFlow.Infrastructure.Data;
using GranaFlow.Infrastructure.Repositories;
using GranaFlow.Infrastructure.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using System.Text;

namespace GranaFlow.API.Extensions
{
    public static class ServiceCollectionExtensions
    {

        public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("GranaFlowDB");

            services.AddDbContext<GranaFlowContext>(options =>
                options.UseSqlServer(connectionString)
                       .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
                       .EnableSensitiveDataLogging()
                       .EnableDetailedErrors()
                       .ConfigureWarnings(warnings =>
                            warnings.Ignore(RelationalEventId.PendingModelChangesWarning)));

            return services;
        }

        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<UsuarioService>();
            services.AddScoped<CategoriaService>();
            services.AddScoped<PessoaService>();

            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            services.AddScoped<ICategoriaRepository, CategoriaRepository>();
            services.AddScoped<IPessoaRepository, PessoaRepository>();

            services.AddScoped<ITokenService, TokenService>();

            services.AddScoped<CryptoUtils>();
            services.AddScoped<InfoToken>();

            return services;
        }

        public static IServiceCollection AddApiDocumentation(this IServiceCollection services)
        {
            services.AddSwaggerGen(static options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Grana Flow API",
                    Version = "v1",
                    Description = "API para gerenciamento de gastos e finanças"
                });

                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Insira o token JWT no formato: Bearer {seu_token}"
                });

                options.AddSecurityRequirement(static document =>
                {
                    var requirement = new OpenApiSecurityRequirement
                    {
                        [new OpenApiSecuritySchemeReference("Bearer", document)] = new List<string>()
                    };
                    return requirement;
                });
            });

            return services;
        }

        public static IServiceCollection AddApiCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins", policy =>
                {
                    policy.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader();
                });
            });

            return services;
        }

        public static IServiceCollection AddJWTConfig(this IServiceCollection services, IConfiguration configuration)
        {
            var secretKey = configuration["JwtSettings:SecretKey"];

            var key = Encoding.UTF8.GetBytes(secretKey);

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    RequireExpirationTime = true,
                    ValidateLifetime = true,

                    ValidateIssuer = false,
                    ValidateAudience = false,

                    ClockSkew = TimeSpan.Zero
                };
            });

            return services;
        }
    }
}
