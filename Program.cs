using CommandsApi.Data;
using CommandsApi.Models;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//Configure newtonsoft json
builder.Services.AddControllers().AddNewtonsoftJson(n =>
{
    n.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
});

//configure api versioning options
builder.Services.AddApiVersioning(o =>
{
    o.AssumeDefaultVersionWhenUnspecified = true;
    o.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
    o.ReportApiVersions = true;
    o.ApiVersionReader = ApiVersionReader.Combine(
        new QueryStringApiVersionReader("api-version"),
        new HeaderApiVersionReader("X-Version"),
        new MediaTypeApiVersionReader("ver"));

});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Automaper configuration
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

//inject command repository
builder.Services.AddScoped<ICommanderRepo, SqlCommanderRepo>();

builder.Services.AddDbContext<CommanderDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("CommanderConnectionString"));
});

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

app.Run();
