using api.Data;
using DbUp;
using DbUp.Engine;
using DbUp.SqlServer;
using Microsoft.Extensions.Configuration;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
IConfiguration configuration = builder.Configuration;

// Connection string to your SQL Server database
var connectionString = configuration.GetConnectionString("DefaultConnection");
var mySettings = new MySettings();

// Create a DbUp upgrader instance
var upgrader = DeployChanges.To
    .SqlDatabase(connectionString)
    .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly())
    .LogToConsole()
    .Build();

// Execute the upgrade process if needed
if (upgrader.IsUpgradeRequired()) {
    var result = upgrader.PerformUpgrade();
}

// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IDataRepository, DataRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();
app.MapControllers();

app.Run();
