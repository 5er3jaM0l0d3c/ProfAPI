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
    builder.Services.AddAutoMapper(cfg => { }, typeof(ProfServer.Application.Mappings.UserProfile));
    builder.Services.AddAutoMapper(cfg => { }, typeof(ProfServer.Application.Mappings.ProductProfile));
    builder.Services.AddAutoMapper(cfg => { }, typeof(ProfServer.Application.Mappings.SaleProfile));
    builder.Services.AddAutoMapper(cfg => { }, typeof(ProfServer.Application.Mappings.Machine_ProductProfile));
    builder.Services.AddAutoMapper(cfg => { }, typeof(ProfServer.Application.Mappings.MaintenanceProfile));
    builder.Services.AddAutoMapper(cfg => { }, typeof(ProfServer.Application.Mappings.Maintanence_WorkDescriptionProfile));
    builder.Services.AddAutoMapper(cfg => { }, typeof(ProfServer.Application.Mappings.Maintenance_ProblemProfile));

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
    builder.Services.AddScoped<IMachineStatusRepository, MachineStatusRepository>();
    builder.Services.AddScoped<IManufacturerRepository, ManufacturerRepository>();
    builder.Services.AddScoped<IManufactureCountryRepository, ManufactureCountryRepository>();
    builder.Services.AddScoped<IUserRepository, UserRepository>();
    builder.Services.AddScoped<IRoleRepository, RoleRepository>();
    builder.Services.AddScoped<IProductRepository, ProductRepository>();
    builder.Services.AddScoped<ISaleRepository, SaleRepository>();
    builder.Services.AddScoped<IMachine_ProductRepository, Machine_ProductRepository>();
    builder.Services.AddScoped<IProblemRepository, ProblemRepository>();
    builder.Services.AddScoped<IWorkDescriptionRepository, WorkDescriptionRepository>();
    builder.Services.AddScoped<IMaintenanceRepository, MaintenanceRepository>();
    builder.Services.AddScoped<IMaintenance_WorkDescriptionRepository, Maintenance_WorkDescriptionRepository>();
    builder.Services.AddScoped<IMaintenance_ProblemRepository, Maintenance_ProblemRepository>();

    builder.Services.AddScoped<IMachineService, MachineService>();
    builder.Services.AddScoped<IPaymentTypeService, PaymentTypeService>();
    builder.Services.AddScoped<IMachineStatusService, MachineStatusService>();
    builder.Services.AddScoped<IManufacturerService, ManufacturerService>();
    builder.Services.AddScoped<IManufactureCountryService, ManufactureCountryService>();
    builder.Services.AddScoped<IUserService, UserService>();
    builder.Services.AddScoped<IRoleService, RoleService>();
    builder.Services.AddScoped<IProductService, ProductService>();
    builder.Services.AddScoped<ISaleService, SaleService>();
    builder.Services.AddScoped<IMachine_ProductService, Machine_ProductService>();
    builder.Services.AddScoped<IProblemService, ProblemService>();
    builder.Services.AddScoped<IWorkDescriptionService, WorkDescriptionService>();
    builder.Services.AddScoped<IMaintenanceService, MaintenanceService>();
    builder.Services.AddScoped<IMaintenance_WorkDescriptionService, Maintenance_WorkDescriptionService>();
    builder.Services.AddScoped<IMaintenance_ProblemService, Maintenance_ProblemService>();
    builder.Services.AddScoped<ITokenService, TokenService>();

    builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    builder.WebHost.UseUrls("https://localhost:7014", "http://localhost:5178");


    var app = builder.Build();
    var logger = app.Services.GetRequiredService<ILogger<Program>>();
    logger.LogInformation("Application starting up");
    
    logger.LogInformation("Now listening on: https://localhost:7014");
    logger.LogInformation("Now listening on: http://localhost:5179");


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
