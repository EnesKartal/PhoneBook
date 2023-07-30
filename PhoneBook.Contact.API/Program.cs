using Microsoft.EntityFrameworkCore;
using PhoneBook.Contact.API.Models.Domain;
using PhoneBook.Contact.API.Repositories;
using PhoneBook.Contact.API.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddScoped<IContactRepository, ContactRepository>();
builder.Services.AddScoped<IContactInfoRepository, ContactInfoRepository>();

builder.Services.AddScoped<IContactService, ContactService>();
builder.Services.AddScoped<IContactInfoService, ContactInfoService>();

builder.Services.AddDbContext<PhoneBookContactDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("PhoneBookContactConnectionString")));

var app = builder.Build();
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
