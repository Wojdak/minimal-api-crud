using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using System;
using MinimalAPI.Data;
using MinimalAPI.Models;
using MinimalAPI.Services;
using FluentValidation;
using MinimalAPI.Validators;
using MinimalAPI.Filters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DataContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("Data")));

builder.Services.AddScoped<IDriverService, DriverService>();
builder.Services.AddSingleton<IValidator<Driver>, DriverValidator>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var drivers = app.MapGroup("/drivers");

drivers.MapGet("/", (IDriverService _service) =>
{
    return Results.Ok(_service.GetDrivers());
});

drivers.MapGet("/{id}", (IDriverService _service, int id) =>
{
    var driver = _service.GetDriverById(id);

    if (driver is null)
        return Results.NotFound("Driver with the given ID doesn't exist");

    return Results.Ok(driver);
});

drivers.MapPost("/", (IDriverService _service, Driver driver) =>
{
    var result = _service.CreateDriver(driver);
    return Results.Ok(result);

}).AddEndpointFilter<DriverValidationFilter>();

drivers.MapPut("/{id}", (IDriverService _service, Driver driver, int id) =>
{
    var updatedDriver = _service.UpdateDriver(driver, id);

    if (updatedDriver is null)
        return Results.NotFound("Driver with the given ID doesn't exist");

    return Results.Ok(updatedDriver);

}).AddEndpointFilter<DriverValidationFilter>();

drivers.MapDelete("/{id}", (IDriverService _service, int id) =>
{
    bool deletedDriver = _service.DeleteDriver(id);

    if (!deletedDriver)
        return Results.NotFound("Driver with the given ID doesn't exist");

    return Results.Ok("Driver was successfully deleted");
});

app.Run();

