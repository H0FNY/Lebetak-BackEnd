//using Lebetak.Hubs;
//using Lebetak.Models;
//using Microsoft.AspNetCore.Authentication.JwtBearer;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.IdentityModel.Tokens;
//using System.Text;

//var builder = WebApplication.CreateBuilder(args);

//// Controllers
//builder.Services.AddControllers()
//    .AddJsonOptions(options =>
//    {
//        options.JsonSerializerOptions.ReferenceHandler =
//            System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
//    });

//// Swagger
//builder.Services.AddOpenApi();

//// DbContext
//builder.Services.AddDbContext<LebetakContext>(options =>
//    options.UseLazyLoadingProxies()
//           .UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//// Identity
//builder.Services.AddIdentity<User, IdentityRole>()
//    .AddEntityFrameworkStores<LebetakContext>()
//    .AddDefaultTokenProviders();

//// JWT + SignalR support
////builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
////.AddJwtBearer(options =>
////{
////    var secretKey = new SymmetricSecurityKey(
////        Encoding.UTF8.GetBytes("THIS_IS_MY_SUPER_SECRET_KEY_12345"));

////    options.TokenValidationParameters = new TokenValidationParameters
////    {
////        IssuerSigningKey = secretKey,
////        ValidateIssuer = false,
////        ValidateAudience = false
////    };

////    //  SignalR
////    options.Events = new JwtBearerEvents
////    {
////        OnMessageReceived = context =>
////        {
////            var accessToken = context.Request.Query["access_token"];
////            var path = context.HttpContext.Request.Path;

////            if (!string.IsNullOrEmpty(accessToken) &&
////                path.StartsWithSegments("/chatHub"))
////            {
////                context.Token = accessToken;
////            }
////            return Task.CompletedTask;
////        }
////    };
////});

//// SignalR
//builder.Services.AddSignalR();

//// CORS 
//builder.Services.AddCors(options =>
//{
//    options.AddPolicy("AllowAll", policy =>
//    {
//        policy
//            .AllowAnyHeader()
//            .AllowAnyMethod()
//            .AllowCredentials()
//            .SetIsOriginAllowed(_ => true);
//    });
//});

//var app = builder.Build();

//if (app.Environment.IsDevelopment())
//{
//    app.MapOpenApi();
//    app.UseSwaggerUI(op =>
//        op.SwaggerEndpoint("/openapi/v1.json", "Lebetak API"));
//}

//app.UseHttpsRedirection();
//app.UseStaticFiles();

//app.UseCors("AllowAll");

//app.UseAuthentication();
//app.UseAuthorization();

//app.MapControllers();

//// Hub 
//app.MapHub<Newshub>("/newsHub");
//app.MapHub<ChatHub>("/chatHub");

//app.Run();



using AutoMapper;
using CloudinaryDotNet;
using Lebetak.Hubs;
using Lebetak.Models;
using Lebetak.Services;
using Lebetak.settings;
using Lebetak.UnitOfWork;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Lebetak
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            //builder.Services.AddControllers();
            builder.Services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler =
                    System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
            });

            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            builder.Services.AddDbContext<LebetakContext>(bui => bui.UseLazyLoadingProxies().UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
            builder.Services.AddScoped<UnitOFWork>();


            builder.Services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<LebetakContext>()
                .AddDefaultTokenProviders();

            // Cloudinary
            builder.Services.Configure<CloudinarySettings>(
                builder.Configuration.GetSection("Cloudinary"));
            builder.Services.AddSingleton(provider =>
            {
                var config = provider.GetRequiredService<IOptions<CloudinarySettings>>().Value;
                return new Cloudinary(new Account(config.CloudName, config.ApiKey, config.ApiSecret));
            });


            // Auto mapper
            builder.Services.AddAutoMapper(op => op.AddProfile(typeof(Lebetak.Profiles.Mapper)));

            builder.Services.AddAuthentication(op =>
            {
                op.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                op.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                op.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(option => {
                    var secrityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("THIS_IS_MY_SUPER_SECRET_KEY_12345"));
                    option.TokenValidationParameters = new TokenValidationParameters()
                    {
                        IssuerSigningKey = secrityKey,
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });


            // Google Authentication
            builder.Services.AddAuthentication()
                .AddGoogle(options =>
                {
                    options.ClientId = "<Google-Client-Id>";
                    options.ClientSecret = "<Google-Client-Secret>";
                });


            builder.Services.AddSignalR();
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                    policy =>
                    {
                        policy.AllowAnyOrigin()
                              .AllowAnyHeader()
                              .AllowAnyMethod();
                    });
            });
            //// SignalR
            

            var app = builder.Build();
            
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.UseSwaggerUI(op => op.SwaggerEndpoint("/openapi/v1.json", "V1"));
            }

            app.UseStaticFiles();
            app.UseCors("AllowAll");

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();
            app.MapHub<Newshub>("/newsHub");
            app.MapHub<ChatHub>("/chatHub");
            app.Run();
        }
    }
}