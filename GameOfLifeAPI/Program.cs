using Microsoft.Extensions.PlatformAbstractions;
using System.Reflection;
using GameOfLifeKata.Business;
using GameOfLifeKata.Infrastructure;
using Microsoft.Extensions.Options;
using Asp.Versioning;

namespace GameOfLifeKata.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();
            builder.Services.AddScoped<GameOfLife>(x => 
                new GameOfLife(new FileSystemBoardRepository(@"C:\dotNetKataGoL\GameOfLifeAPI")));
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddApiVersioning(setup =>
            {
                setup.ReportApiVersions = true;
                setup.DefaultApiVersion = new ApiVersion(1, 0);
                setup.ApiVersionReader = new UrlSegmentApiVersionReader();
                setup.AssumeDefaultVersionWhenUnspecified = true;

            }).AddMvc()
                .AddApiExplorer(options =>
                {
                    options.GroupNameFormat = "'v'VVV";
                    options.SubstituteApiVersionInUrl = true;
                });

            builder.Services.AddProblemDetails();
            builder.Services.AddSwaggerGen(c => 
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { 
                    Title="Game Of Life",
                    Version="v1",
                    Description= "A game of life API",
                });
                c.SwaggerDoc("v2", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "Game Of Life",
                    Version = "v2",
                    Description = "A game of life API",
                });

                var basePath = PlatformServices.Default.Application.ApplicationBasePath;
                var xmlPath = Path.Combine(basePath, Path.GetFileNameWithoutExtension(Assembly.GetEntryAssembly().Location)) + ".xml";
                if (File.Exists(xmlPath))
                {
                    c.IncludeXmlComments(xmlPath);
                }

            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    foreach (var description in app.DescribeApiVersions())
                    {
                        var url = $"/swagger/{description.GroupName}/swagger.json";
                        var name = description.GroupName.ToUpperInvariant();
                        c.SwaggerEndpoint(url, name);
                    }
                });
            }


            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}