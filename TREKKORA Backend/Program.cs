
using System.Security.Claims;
using System.Text;
using Application.Helpers;
using Application.Interfaces;
using Application.Mapper;
using Application.Services;
using Infrastructure;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using TREKKORA_Backend.Hubs;

namespace TREKKORA_Backend
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter());
                });
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Enter 'Bearer' followed by space and JWT token"
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] {}
                    }
                });

            });
            builder.Services.AddSwaggerGen();

            builder.Services.AddInfrastructure(builder.Configuration);



            builder.Services.AddScoped<ICountryRepository, CountryRepository>();
            builder.Services.AddScoped<IUserRepository, UserRepositories>();
            builder.Services.AddScoped<IStatesRepository, StateRepositories>();
            builder.Services.AddScoped<IGuidProfileRepositories, GuidProfileRepository>();
            builder.Services.AddScoped<IGuidRepository, GuideRepository>();
            builder.Services.AddScoped<IPlaceRepository, PlaceRepository>();
            builder.Services.AddScoped<IWishListRepository, WishListRepository>();
            builder.Services.AddScoped<IRatingRepository, RatingRepository>();
            builder.Services.AddScoped<IBookingRepository, BookingRepository>();
            builder.Services.AddScoped<ISearchRepository, SearchReposiotry>();
            builder.Services.AddScoped<IMessageRepository, MessageRepository>();


            builder.Services.AddScoped<IJwtServices, JwtServices>();
            builder.Services.AddScoped<IStateServices, StateServices>();
            builder.Services.AddScoped<IAuthServices, AuthServices>();
            builder.Services.AddScoped<IUserServices, UserServices>();
            builder.Services.AddScoped<ICountryServices, CountryServices>();
            builder.Services.AddScoped<IGuideProfileService, GuidProfileService>();
            builder.Services.AddScoped<ICLoudinaryServices, CloudinaryServices>();
            builder.Services.AddHttpClient<IWeatherServices, WeatherServices>();
            builder.Services.AddScoped<IWeatherServices, WeatherServices>();
            builder.Services.AddScoped<IPlaceServices, PlaceServices>();
            builder.Services.AddScoped<IWishListServices, WishListService>();
            builder.Services.AddScoped<IRatinServices,RatingService>();
            builder.Services.AddScoped<IBookingServices,BookinServices>();
            builder.Services.AddScoped<IsearchServices, SearchServices>();  
            builder.Services.AddScoped<IMessageServices, MessageServices>();
           

            builder.Services.AddAutoMapper(typeof(MappingProfile));
            builder.Services.AddSignalR();

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
              .AddJwtBearer(options =>
              {
                  options.TokenValidationParameters = new TokenValidationParameters
                  {
                      ValidIssuer = builder.Configuration["jwt:issuer"],
                      ValidAudience = builder.Configuration["jwt:audience"],
                      IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["jwt:key"])),
                      ValidateIssuer = true,
                      ValidateAudience = true,
                      ValidateLifetime = true,
                      ValidateIssuerSigningKey = true,
                      RoleClaimType = ClaimTypes.Role
                  };
              });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthentication();

            app.UseAuthorization();


            app.MapControllers();
            app.MapHub<ChatHub>("/chathub");

            app.Run();
        }
    }
}
