using Microsoft.OpenApi.Models;
using StatusStream.Application.Interfaces;
using StatusStream.Application.Services;
using StatusStream.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
//generate api documentation and interactive UI for testing endpoints.
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "StatusStream API",
        Description = "A lightweight RESTful API for posting and retrieving short status updates. Designed for internal use or prototyping social feed features without authentication."
    });
});

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddSingleton<IRepository, Repository>(); // ? Keeps data in memory across requests
builder.Services.AddScoped<IStatusService, StatusService>();
var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage(); // enable exception display and error display for developer 
    app.UseSwagger(); //register to swagger 
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "TechXpress E-commerce v1"); //Specifies the Swagger JSON file (/swagger/v1/swagger.json) containing API details.
        c.RoutePrefix = string.Empty; // This means you can access Swagger directly at http://localhost:{port}/ instead of http://localhost:{port}/swagger.

    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
