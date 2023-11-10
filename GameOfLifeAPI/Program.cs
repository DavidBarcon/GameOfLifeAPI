using Microsoft.Extensions.PlatformAbstractions;
using System.Reflection;
using GameOfLifeKata.Business;
using GameOfLifeKata.Infrastructure;
using Asp.Versioning;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Text.Json;
using System.Text;
using Microsoft.AspNetCore.Mvc.Formatters;

namespace GameOfLifeKata.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();
            builder.Services.AddScoped<GameOfLife>();
            builder.Services.AddScoped<BoardRepository, FileSystemBoardRepository>(x =>
                new FileSystemBoardRepository(GetPath()));
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

            builder.Services.AddHealthChecks().AddFolder(options =>
                {
                    options.AddFolder(GetPath());
                },
                "Folder exists",
                failureStatus: HealthStatus.Unhealthy,
                tags: new[] { "files" });
            builder.Services.AddHealthChecks()
                .AddTypeActivatedCheck<HealthChecks.FolderPermissionsHealthCheck>(
                    "Folder permissions check",
                    failureStatus: HealthStatus.Degraded,
                    args: new Object[]{ GetPath()},
                    tags: new[] { "files" });



            var app = builder.Build();

            // Configure the HTTP request pipeline.
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
            

            app.MapHealthChecks("/healthz", new HealthCheckOptions
            {
                //Predicate = healthCheck => healthCheck.Tags.Contains("sample"),
                ResultStatusCodes = null,
                ResponseWriter = WriteResponse,
            });

            app.UseHealthChecksPrometheusExporter("/healthmetrics");

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }

        private static string GetPath()
        {
            var path = @"C:\dotNetKataGoL\GameOfLifeAPI\Saves";
            if (OperatingSystem.IsLinux())
            {
                path =  @"/app/Saves";
            }
            Directory.CreateDirectory(path);
            return path;
        }

        private static Task WriteResponse(HttpContext context, HealthReport healthReport)
        {
            context.Response.ContentType = "application/json; charset=utf-8";

            var options = new JsonWriterOptions { Indented = true };

            using var memoryStream = new MemoryStream();
            using (var jsonWriter = new Utf8JsonWriter(memoryStream, options))
            {
                jsonWriter.WriteStartObject();
                jsonWriter.WriteString("status", healthReport.Status.ToString());
                jsonWriter.WriteStartObject("results");

                foreach (var healthReportEntry in healthReport.Entries)
                {
                    jsonWriter.WriteStartObject(healthReportEntry.Key);
                    jsonWriter.WriteString("status",
                        healthReportEntry.Value.Status.ToString());
                    jsonWriter.WriteString("description",
                        healthReportEntry.Value.Description);
                    jsonWriter.WriteStartObject("data");

                    foreach (var item in healthReportEntry.Value.Data)
                    {
                        jsonWriter.WritePropertyName(item.Key);

                        JsonSerializer.Serialize(jsonWriter, item.Value,
                            item.Value?.GetType() ?? typeof(object));
                    }

                    jsonWriter.WriteEndObject();
                    jsonWriter.WriteEndObject();
                }

                jsonWriter.WriteEndObject();
                jsonWriter.WriteEndObject();
            }

            return context.Response.WriteAsync(
                Encoding.UTF8.GetString(memoryStream.ToArray()));
        }
    }
}