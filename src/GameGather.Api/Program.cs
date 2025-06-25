using GameGather.Api.Middleware;
using GameGather.Api.Modules;
using GameGather.Application;
using GameGather.Infrastructure;
using GameGather.Infrastructure.Authentication;
using GameGather.Infrastructure.Database.Seeders;
using GameGather.Infrastructure.Persistance;
using GameGather.Infrastructure.Utils.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .WriteTo.File(
        "Logs/log-.txt",
        rollOnFileSizeLimit: true,               // new file will be created when the current file reaches the size limit
        fileSizeLimitBytes: 20_000_000,          // ~20 MB
        retainedFileCountLimit: 7             // 7 files of logs only
    )
    .CreateLogger();


var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddCors(options =>
    {
        options.AddPolicy("AllowAllOrigins",
            builder =>
            {
                builder
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            });
    });

    // Add services to the container.
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    
    //CORS
    builder.Services.AddCors(options =>
    {
        options.AddPolicy("AllowAllOrigins",
            builder =>
            {
                builder
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            });
    });
    
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });

        c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            Description = "Wprowadź token JWT w polu 'Bearer {token}'",
            Name = "Authorization",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.Http,
            Scheme = "bearer",
            BearerFormat = "JWT"
        });

        c.AddSecurityRequirement(new OpenApiSecurityRequirement
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
                new string[] { }
            }
        });
    });
    
    builder.Services
        .AddApplication()
        .AddInfrastructure(builder.Configuration);
}


builder.Host.UseSerilog();


var app = builder.Build();
{

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        
    }



    app.UseSwagger(); 
    app.UseSwaggerUI();

    // ENDPOINTS
    app.AddTestEndpoints();
    app.AddAuthenticationEndpoints();
    app.AddPlayerManagerEndpoints();
    app.AddSessionGameModuleEndpoints();
    app.AddPostGameEndpoints();
    app.AddCommentEndpoints();
    // END ENDPOINTS

    // logging
    app.UseMiddleware<RequestLoggingMiddleware>();

    app.UseHttpsRedirection();
    app.UseAuthentication();
    app.UseAuthorization();

    app.UseCors("AllowAllOrigins");
    app.ApplyMigration();
    app.ApplySeeder();

    app.Run();
}

public partial class Program { }