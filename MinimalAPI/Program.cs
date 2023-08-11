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
    return drivers;
});

app.MapGet("/drivers/{id}", (int id) =>
{
    return drivers.Find(driver => driver.Id == id);
});


app.Run();

class Driver
{
    public int Id { get; set; }
    public required string Name { get; set; } = string.Empty;
    public required string Nationality { get; set; } = string.Empty;
    public required int RacingNumber { get; set; }
    public string Team { get; set; } = string.Empty;
}