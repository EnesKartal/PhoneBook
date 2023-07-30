using Microsoft.EntityFrameworkCore;
using PhoneBook.Contact.API.Models.Domain;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<PhoneBookContactDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("PhoneBookContactConnectionString")));

var app = builder.Build();
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
