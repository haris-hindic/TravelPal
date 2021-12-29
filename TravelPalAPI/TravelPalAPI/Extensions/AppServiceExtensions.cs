using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelPalAPI.Database;
using TravelPalAPI.Helpers;
using TravelPalAPI.Repositories;
using TravelPalAPI.Repositories.Implementation;

namespace TravelPalAPI.Extensions
{
    public static class AppServiceExtensions
    {
        public static IServiceCollection AddAppServices(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "TravelPalAPI", Version = "v1" });
            });

            services.AddDbContext<AppDbContext>(opts => opts.UseSqlServer(configuration.GetConnectionString("AppDb")));

            services.AddScoped<IFileStorageService, LocalImageStorage>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IAccommodationRepository, AccommodationRepository>();

            services.AddAutoMapper(typeof(AutoMapperProfiles).Assembly);

            return services;
        }
    }
}
