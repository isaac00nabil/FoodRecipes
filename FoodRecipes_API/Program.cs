using FoodRecipes_Core.IRepositories;
using FoodRecipes_Core.IServices;
using FoodRecipes_Core.Models.Context;
using FoodRecipes_Infra.Repositories;
using FoodRecipes_Infra.Services;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<FoodRecipesDbContext>(cop =>
cop.UseSqlServer(builder.Configuration.GetValue<string>("sqlconnect")));

builder.Services.AddScoped<IAdminServiceInterface, AdminServiceImplementation>();
builder.Services.AddScoped<IAdminRepositoryInterface, AdminRepositoryImplementation>();

builder.Services.AddScoped<IClientServiceInterface, ClientServiceImplementation>();
builder.Services.AddScoped<IClientRepositoryInterface, ClientRepositoryImplementation>();

builder.Services.AddScoped<ISharedServiceInterface, SharedServiceImplementation>();
builder.Services.AddScoped<ISharedRepositoryInterface, SharedRepositoryImplementation>();


var warningLogger = new LoggerConfiguration()
    .WriteTo.File("D:\\Full Stack\\Project\\FoodRecipes\\FoodRecipes_Core\\Logger\\warningLogger.txt",
    rollingInterval: RollingInterval.Day,
    restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Warning)
    .CreateLogger();

var errorLogger = new LoggerConfiguration()
    .WriteTo.File("D:\\Full Stack\\Project\\FoodRecipes\\FoodRecipes_Core\\Logger\\errorLogger.txt",
    rollingInterval: RollingInterval.Month,
    restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Error)
    .CreateLogger();

var informationLogger = new LoggerConfiguration()
    .WriteTo.File("D:\\Full Stack\\Project\\FoodRecipes\\FoodRecipes_Core\\Logger\\informationLogger.txt",
    rollingInterval: RollingInterval.Hour,
    restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Information)
    .CreateLogger();

var debugLogger = new LoggerConfiguration()
    .WriteTo.File("D:\\Full Stack\\Project\\FoodRecipes\\FoodRecipes_Core\\Logger\\debugLogger.txt",
    rollingInterval: RollingInterval.Minute,
    restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Debug)
    .CreateLogger();

warningLogger.Warning("This is a warning log message");
errorLogger.Error("This is an error log message");
informationLogger.Information("This is an information log message");
debugLogger.Debug("This is a debug log message");

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

Log.Information("Starting web host");

app.Run();
