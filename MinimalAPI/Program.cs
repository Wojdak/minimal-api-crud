using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Text;
using MinimalAPI.Data;
using MinimalAPI.Models;
using MinimalAPI.Services;
using FluentValidation;
using MinimalAPI.Validators;
using MinimalAPI.Filters;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Builder;
using System.Data;
using System.Net;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Description = "Bearer Authentication with JWT Token",
        Type = SecuritySchemeType.Http
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Id = "Bearer",
                    Type = ReferenceType.SecurityScheme
                }
            },
            new List<string>()
        }
    });
});

builder.Services.AddDbContext<DataContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("Data")));

builder.Services.AddScoped<IDriverService, DriverService>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddSingleton<IValidator<Driver>, DriverValidator>();
builder.Services.AddSingleton<IValidator<UserDto>, UserValidator>();

builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer( x =>
{
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateActor = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});

builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

var drivers = app.MapGroup("/drivers");

drivers.MapGet("/", [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Standard, Administrator")] (IDriverService _service) =>
{
    return Results.Ok(_service.GetDrivers());
});

drivers.MapGet("/{id}", [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Standard, Administrator")] (IDriverService _service, int id) =>
{
    var driver = _service.GetDriverById(id);

    if (driver is null)
        return Results.NotFound("Driver with the given ID doesn't exist");

    return Results.Ok(driver);
});

drivers.MapPost("/", [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator")] (IDriverService _service, Driver driver) =>
{
    var result = _service.CreateDriver(driver);
    return Results.Ok(result);

}).AddEndpointFilter<DriverValidationFilter>();

drivers.MapPut("/{id}", [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator")] (IDriverService _service, Driver driver, int id) =>
{
    var updatedDriver = _service.UpdateDriver(driver, id);

    if (updatedDriver is null)
        return Results.NotFound("Driver with the given ID doesn't exist");

    return Results.Ok(updatedDriver);

}).AddEndpointFilter<DriverValidationFilter>();

drivers.MapDelete("/{id}", [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator")] (IDriverService _service, int id) =>
{
    bool deletedDriver = _service.DeleteDriver(id);

    if (!deletedDriver)
        return Results.NotFound("Driver with the given ID doesn't exist");

    return Results.Ok("Driver was successfully deleted");
});


app.MapPost("/register", (UserDto _userDto, IUserService _service) =>
{
    var result = _service.RegisterUser(_userDto);

    return Results.Ok(result);

}).AddEndpointFilter<UserValidationFilter>();


app.MapPost("/login", (UserDto _userDto, IUserService _service) =>
{
    var loggedInUser = _service.LoginUser(_userDto);

    if (loggedInUser is null)
        return Results.NotFound("User not found");

    string jwtToken = CreateToken(loggedInUser);

    return Results.Ok(jwtToken);

}).AddEndpointFilter<UserValidationFilter>();

string CreateToken(User user)
{
    var claims = new List<Claim>
    {
        new Claim(ClaimTypes.Name, user.Username),
        new Claim(ClaimTypes.Role, user.Role),
    };

    var token = new JwtSecurityToken
    (
        issuer: builder.Configuration["Jwt:Issuer"],
        audience: builder.Configuration["Jwt:Audience"],
        claims: claims,
        expires: DateTime.UtcNow.AddDays(1),
        notBefore: DateTime.UtcNow,
        signingCredentials: new SigningCredentials(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
            SecurityAlgorithms.HmacSha256)
    );

    var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

    return tokenString;
}

app.Run();

