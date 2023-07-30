using Microsoft.EntityFrameworkCore;
using PhoneBook.Report.API.Models.Domain;
using PhoneBook.Report.API.Repositories;
using PhoneBook.Report.API.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddScoped<IReportRepository, ReportRepository>();
builder.Services.AddScoped<IReportDetailRepository, ReportDetailRepository>();

builder.Services.AddScoped<IReportService, ReportService>();
builder.Services.AddScoped<IReportDetailService, ReportDetailService>();

builder.Services.AddDbContext<PhoneBookReportDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("PhoneBookReportConnectionString")));

var app = builder.Build();
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
