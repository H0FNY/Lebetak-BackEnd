using AutoMapper;
using CloudinaryDotNet;
using Lebetak.Hubs;
using Lebetak.Models;
using Lebetak.Models.ChatModel;
using Lebetak.Services;
using Lebetak.settings;
using Lebetak.UnitOfWork;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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

            // ================= Controllers =================
            builder.Services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.ReferenceHandler =
                        System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
                });
            builder.Services.Configure<OpenAIOptions>(
            builder.Configuration.GetSection("OpenAI"));
            builder.Services.AddHttpClient();
            builder.Services.AddOpenApi();

            // ================= DbContext =================
            builder.Services.AddDbContext<LebetakContext>(opt =>
                opt.UseLazyLoadingProxies()
                   .UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddScoped<UnitOFWork>();

            // ================= Identity =================
            builder.Services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<LebetakContext>()
                .AddDefaultTokenProviders();

            // ================= Cloudinary =================
            builder.Services.Configure<CloudinarySettings>(
                builder.Configuration.GetSection("Cloudinary"));

            builder.Services.AddSingleton(provider =>
            {
                var config = provider.GetRequiredService<IOptions<CloudinarySettings>>().Value;
                return new Cloudinary(new Account(
                    config.CloudName,
                    config.ApiKey,
                    config.ApiSecret));
            });

            // ================= AutoMapper =================
            builder.Services.AddAutoMapper(op =>
                op.AddProfile(typeof(Lebetak.Profiles.Mapper)));

            // ================= Authentication (JWT + SignalR) =================
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                var securityKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes("THIS_IS_MY_SUPER_SECRET_KEY_12345"));

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    IssuerSigningKey = securityKey,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };

                // ⭐ مهم جدًا لـ SignalR
                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        var accessToken = context.Request.Query["access_token"];
                        var path = context.HttpContext.Request.Path;

                        if (!string.IsNullOrEmpty(accessToken) &&
                            path.StartsWithSegments("/chatHub"))
                        {
                            context.Token = accessToken;
                        }

                        return Task.CompletedTask;
                    }
                };
            });

            // ================= Google Authentication =================
            builder.Services.AddAuthentication()
                .AddGoogle(options =>
                {
                    options.ClientId = "<Google-Client-Id>";
                    options.ClientSecret = "<Google-Client-Secret>";
                });

            // ================= SignalR =================
            builder.Services.AddSignalR();

            // ================= CORS (SignalR Friendly) =================
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", policy =>
                {
                    policy
                        .SetIsOriginAllowed(_ => true)
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials();
                });
            });

            var app = builder.Build();

            // ================= Swagger =================
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.UseSwaggerUI(c =>
                    c.SwaggerEndpoint("/openapi/v1.json", "Lebetak API V1"));
            }

            app.UseStaticFiles();

            // ================= Middleware Order =================
            app.UseCors("AllowAll");

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            // ================= Endpoints =================
            app.MapControllers();
            app.MapHub<Newshub>("/newsHub").RequireCors("AllowAll");
            app.MapHub<ChatHub>("/chatHub").RequireCors("AllowAll");

            app.Run();
        }
    }
}
