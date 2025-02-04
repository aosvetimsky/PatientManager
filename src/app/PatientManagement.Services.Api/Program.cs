using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using PatientManagement.Persistence;
using PatientManagement.Services;
using PatientManagement.Services.Api;
using PatientManagement.Services.Api.Infrastructure.TypeConverters;
using PatientManagement.Services.Common;

public partial class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var configuration = builder.Configuration;

        // Add services to the container.
        builder.Services.AddControllers().AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.Converters.Add(new Hl7SearchDateJsonConverter());
        });
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(options =>
        {
            options.MapType(typeof(Hl7SearchDate), () => new OpenApiSchema
            {
                Type = "string"
            });
        });

        builder.Services.AddDbContext<AppDbContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString(nameof(AppDbContext)));
        });

        builder.Services.AddPersistenceServices();
        builder.Services.AddApplicationServices();
        builder.Services.AddApiServices();

        var app = builder.Build();

        InitDb(app.Services).GetAwaiter().GetResult();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }

    private static async Task InitDb(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        using var ctx = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        await ctx.Database.EnsureCreatedAsync();
        await SeedData.SeedPatients(ctx);
    }
}



