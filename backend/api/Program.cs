using api.Authorization;
using api.Data;
using Auth0.AspNetCore.Authentication;
using DbUp;
using DbUp.Engine;
using DbUp.SqlServer;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Security.Claims;

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

// Add memory cache 
builder.Services.AddMemoryCache();
builder.Services.AddSingleton<IQuestionCache, QuestionCache>();

// Add Auth0
var domain = $"https://{builder.Configuration["Auth0:Domain"]}/";
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(options => {
    options.Authority = domain;
    options.Audience = builder.Configuration["Auth0:Audience"];
    options.TokenValidationParameters = new TokenValidationParameters {
        NameClaimType = ClaimTypes.NameIdentifier
    };
});

// Add HTTP Client

builder.Services.AddHttpClient();

// Add Authorization policy
builder.Services.AddAuthorization(options =>
  options.AddPolicy("MustBeQuestionAuthor", policy
   =>
    policy.Requirements
      .Add(new MustBeQuestionAuthorRequirement())));
builder.Services.AddScoped<IAuthorizationHandler, MustBeQuestionAuthorHandler>();

// Get access to the HTTP request information in a class 
builder.Services.AddHttpContextAccessor();


//builder.Services.AddAuth0WebAppAuthentication(options => {
//    options.Domain = builder.Configuration["Auth0:Domain"];
//    options.ClientId = builder.Configuration["Auth0:ClientId"];
//});

//builder.Services.AddAuthentication(options => {
//    options.DefaultAuthenticateScheme =
//      JwtBearerDefaults.AuthenticationScheme;
//    options.DefaultChallengeScheme =
//      JwtBearerDefaults.AuthenticationScheme;
//}).AddJwtBearer(options =>
//{
//    options.Authority =
//    builder.Configuration["Auth0:Authority"];
//    options.Audience =
//    builder.Configuration["Auth0:Audience"];
//});

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
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();


app.Run();
