using FluentValidation.AspNetCore;
using Gacha_Game.Core;
using Gacha_Game.Infrastructure;
using Gacha_Game.Middlewares;
using Microsoft.OpenApi.Models;

namespace Gacha_Game
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddFluentValidationAutoValidation();
            builder.Services.AddInfrastructure(builder.Configuration);
            builder.Services.AddCore(builder.Configuration);
            builder.Services.AddHealthChecks();
            builder.Services.AddLogging();
            builder.Services.AddControllers();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigin",
                    builder => builder
                        .WithOrigins("http://localhost:3000")
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials());
            });



            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Gacha Game", Version = "v1" });
                c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "api.xml"));
            });

            var app = builder.Build();
            app.UseExceptionHandlingMiddleware();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            else
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors("AllowSpecificOrigin");
            app.UseStaticFiles();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();
            app.MapHealthChecks("/health");

            app.Run();
        }
    }
}
