using Microsoft.EntityFrameworkCore;
using PhoneBook.Common.Interfaces;
using PhoneBook.Report.API.Models.Domain;
using PhoneBook.Report.API.RabbitMQ;
using PhoneBook.Report.API.Repositories;
using PhoneBook.Report.API.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<PhoneBookReportDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("PhoneBookReportConnectionString")));

builder.Services.AddScoped<IReportRepository, ReportRepository>();
builder.Services.AddScoped<IReportDetailRepository, ReportDetailRepository>();

builder.Services.AddScoped<IReportService, ReportService>();
builder.Services.AddScoped<IReportDetailService, ReportDetailService>();

builder.Services.AddScoped<IRabbitMQProducer, ReportRabbitMQProducer>();

builder.Services.AddHostedService<ReportRabbitMQConsumer>();


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
