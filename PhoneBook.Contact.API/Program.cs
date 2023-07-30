using Microsoft.EntityFrameworkCore;
using PhoneBook.Common.Interfaces;
using PhoneBook.Contact.API.Models.Domain;
using PhoneBook.Contact.API.RabbitMQ;
using PhoneBook.Contact.API.Repositories;
using PhoneBook.Contact.API.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<PhoneBookContactDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("PhoneBookContactConnectionString")));

builder.Services.AddScoped<IContactRepository, ContactRepository>();
builder.Services.AddScoped<IContactInfoRepository, ContactInfoRepository>();

builder.Services.AddScoped<IContactService, ContactService>();
builder.Services.AddScoped<IContactInfoService, ContactInfoService>();

builder.Services.AddScoped<IRabbitMQProducer, PhoneBookRabbitMQProducer>();

builder.Services.AddHostedService<PhoneBookRabbitMQConsumer>();


var app = builder.Build();
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

// Configure the HTTP request pipeline.

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    options.RoutePrefix = string.Empty;
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
