using Dapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using ProfServer.API.Middlewares;
using ProfServer.Application.Interfaces;
using ProfServer.Application.Services;
using ProfServer.Infrastructure.Dapper;
using ProfServer.Infrastructure.DbContext;
using ProfServer.Infrastructure.Repositories;
using ProfServer.Models.Official;
using Serilog;
using System.Text;

try
{
    var builder = WebApplication.CreateBuilder(args);

    // Register Dapper handlers for DateOnly before any DB/Dapper usage
    SqlMapper.AddTypeHandler(new DateOnlyHandler());
    SqlMapper.AddTypeHandler(new NullableDateOnlyHandler());

    Log.Logger = new LoggerConfiguration()
        .ReadFrom.Configuration(builder.Configuration)
        .Enrich.FromLogContext()
        .CreateLogger();

    builder.Host.UseSerilog();

    builder.Services.AddAutoMapper(cfg => { }, typeof(ProfServer.Application.Mappings.MachineProfile));

    // JWT settings
    builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("Jwt"));
    var jwtSettings = builder.Configuration.GetSection("Jwt").Get<JwtSettings>()
                      ?? throw new InvalidOperationException("JWT configuration section is missing.");

    // Validate key presence (encourage env var in prod)
    if (string.IsNullOrWhiteSpace(jwtSettings.Key))
    {
        throw new InvalidOperationException("Jwt:Key is not configured. Use appsettings or environment variable.");
    }

    var keyBytes = Encoding.UTF8.GetBytes(jwtSettings.Key);

    builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = true;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(keyBytes),
            ValidateIssuer = true,
            ValidIssuer = jwtSettings.Issuer,
            ValidateAudience = true,
            ValidAudience = jwtSettings.Audience,
            ValidateLifetime = true,
            ClockSkew = TimeSpan.FromMinutes(1)
        };
    });

    // Register token service
    builder.Services.AddSingleton<ITokenService, TokenService>();

    // Add services to the container.
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    if (string.IsNullOrEmpty(connectionString))
    {
        throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
    }
    builder.Services.AddSingleton<IDbConnectionFactory>(new NpgsqlConnectionFactory(connectionString));

    builder.Services.AddScoped<IMachineRepository, MachineRepository>();
    builder.Services.AddScoped<IPaymentTypeRepository, PaymentTypeRepository>();

    builder.Services.AddScoped<IMachineService, MachineService>();
    builder.Services.AddScoped<IPaymentTypeService, PaymentTypeService>();
    builder.Services.AddScoped<ITokenService, TokenService>();

    builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    var app = builder.Build();
    var logger = app.Services.GetRequiredService<ILogger<Program>>();
    logger.LogInformation("Application starting up");

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
        logger.LogInformation("Swagger enabled in Development environment");
    }

    app.UseExceptionMiddleware();

    app.UseHttpsRedirection();

    app.UseAuthentication();
    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Unhandled exception during host startup");
    throw;
}
finally
{
    Log.CloseAndFlush();
}
