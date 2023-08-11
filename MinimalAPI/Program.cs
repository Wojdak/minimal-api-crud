using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using System;
using MinimalAPI.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DataContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("Data")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.MapGet("/drivers", (DataContext context) =>
{
    return context.Drivers.ToList();
});

app.MapGet("/drivers/{id}", (DataContext context, int id) =>
{
    var driver = FindDriverById(context, id);

    if (driver is null)
        return Results.NotFound("Driver with the given ID doesn't exist");

    return Results.Ok(driver);
});

app.MapPost("/drivers", (DataContext context, Driver driver) =>
{
    context.Drivers.Add(driver);
    context.SaveChanges();
    return Results.Ok(driver);
});

app.MapPut("/drivers/{id}", (DataContext context, Driver driver, int id) =>
{
    var driver_to_edit = FindDriverById(context, id);

    if (driver_to_edit is null)
        return Results.NotFound("Driver with the given ID doesn't exist");

    driver_to_edit.Name= driver.Name;
    driver_to_edit.Nationality = driver.Nationality;
    driver_to_edit.RacingNumber = driver.RacingNumber;
    driver_to_edit.Team = driver.Team;

    context.SaveChanges();

    return Results.Ok(driver_to_edit);
});

app.MapDelete("/drivers/{id}", (DataContext context, int id) =>
{
    var driver_to_remove = FindDriverById(context, id);

    if (driver_to_remove is null)
        return Results.NotFound("Driver with the given ID doesn't exist");

    context.Drivers.Remove(driver_to_remove);
    context.SaveChanges();

    return Results.Ok();
});

Driver? FindDriverById(DataContext context, int id)
{
    return context.Drivers.FirstOrDefault(d => d.Id == id);
}

app.Run();

public class Driver
{
    public int Id { get; set; }
    public required string Name { get; set; } = string.Empty;
    public required string Nationality { get; set; } = string.Empty;
    public required int RacingNumber { get; set; }
    public string Team { get; set; } = string.Empty;
}