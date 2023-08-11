using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var drivers = new List<Driver>
{
    new Driver { Id=1, Name="Max Verstappen", Nationality="Dutch", RacingNumber=1, Team="Red Bull Racing"},
    new Driver { Id=2, Name="Sergio Perez", Nationality="Mexican", RacingNumber=11, Team="Red Bull Racing"},
    new Driver { Id=3, Name="Lewis Hamilton", Nationality="British", RacingNumber=44, Team="Mercedes"},
    new Driver { Id=4, Name="Carlos Sainz", Nationality="Spanish", RacingNumber=55, Team="Ferrari"}
};


app.MapGet("/drivers", () =>
{
    return Results.Ok(drivers);
});

app.MapGet("/drivers/{id}", (int id) =>
{
    var driver = FindDriverById(id);

    if (driver is null)
        return Results.NotFound("Driver with the given ID doesn't exist");

    return Results.Ok(driver);
});

app.MapPost("/drivers", (Driver driver) =>
{
    drivers.Add(driver);
    return Results.Ok(driver);
});

app.MapPut("/drivers/{id}", (Driver driver, int id) =>
{
    var driver_to_edit = FindDriverById(id);

    if (driver_to_edit is null)
        return Results.NotFound("Driver with the given ID doesn't exist");

    driver_to_edit.Name= driver.Name;
    driver_to_edit.Nationality = driver.Nationality;
    driver_to_edit.RacingNumber = driver.RacingNumber;
    driver_to_edit.Team = driver.Team;

    return Results.Ok(driver_to_edit);
});

app.MapDelete("/drivers/{id}", (int id) =>
{
    var driver_to_edit = FindDriverById(id);

    if (driver_to_edit is null)
        return Results.NotFound("Driver with the given ID doesn't exist");

    drivers.Remove(driver_to_edit);

    return Results.Ok();
});

Driver? FindDriverById(int id)
{
    return drivers.Find(driver => driver.Id == id);
}

app.Run();

class Driver
{
    public int Id { get; set; }
    public required string Name { get; set; } = string.Empty;
    public required string Nationality { get; set; } = string.Empty;
    public required int RacingNumber { get; set; }
    public string Team { get; set; } = string.Empty;
}